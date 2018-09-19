using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour 
{
	public GameObject playerHand;
	public GameObject EquippedWeapon { get; set; }

	public Transform spawnProjectile;
	Item currentlyEquippedItem;
	IWeapon equippedWeapon;
	ChatacterStats chatacterStats;

	void Start()
	{
		spawnProjectile = transform.Find("ProjectileSpawn");
		chatacterStats = GetComponent<Player>().chatacterStats;
	}

	public void EquipWeapon(Item itemToEquip)
	{
		if(EquippedWeapon != null)
		{
			UnequipWeapon();
		}
		EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug));
		EquippedWeapon.transform.SetParent (playerHand.transform, false);
		equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();

		if(EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
		{
			EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
		}
		EquippedWeapon.transform.SetParent(playerHand.transform);
		equippedWeapon.Stats = itemToEquip.Stats;
		currentlyEquippedItem = itemToEquip;
		chatacterStats.AddStatBonus(itemToEquip.Stats);

		UIEventHandler.ItemEquipped(itemToEquip);
		UIEventHandler.StatsChanged();

	}

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
        chatacterStats.RemoveStatsBonus(equippedWeapon.Stats);
        Destroy(EquippedWeapon.transform.gameObject);
        UIEventHandler.StatsChanged();
    }

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			PerformWeaponAttack();
		}
	}

	public void PerformWeaponAttack()
	{
		equippedWeapon.PerformAttack(CalculateDamage());
	}
	public void PerformWeaponSpecial()
	{
		equippedWeapon.PerformSpecial();
	}

	private int CalculateDamage()
	{
		int damageToDeal = (chatacterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue() *2) + Random.Range(2,8);
		damageToDeal += CalculateCrit(damageToDeal);
		Debug.Log("Damage dealt" + damageToDeal);

		return damageToDeal;
	}

	private int CalculateCrit(int damage)
	{
		if(Random.value <= .10f)
		{
			int critDamage = damage = (int)(damage * Random.Range(.5f, .75f));
			return critDamage;
		}
		return 0;
	}

}
