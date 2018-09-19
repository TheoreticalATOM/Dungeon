using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour 
{
	[SerializeField] private Text health, level;
	[SerializeField] private Image healthFill, levelFill;
	[SerializeField] private Player player;

	//Stats
	private List<Text> playerStatsTexts = new List<Text>();
	[SerializeField] private Text playerStatPrefab;
	[SerializeField] private Transform playerStatPanel;

	//Equipped Weapon Stuff
	[SerializeField] private Sprite defaultWeaponSprite;
	private PlayerWeaponController playerWeaponController;
	[SerializeField] private Text weaponStatPrefab;
	[SerializeField] private Transform weaponStatPanel;
	[SerializeField] private Text weaponNameText;
	[SerializeField] private Image weaponIcon;
	private List<Text> weaponStatTexts = new List<Text>();



	void Start () 
	{
		playerWeaponController = player.GetComponent<PlayerWeaponController>();
		UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
		UIEventHandler.OnStatsChanged += UpdateStats;
		UIEventHandler.OnItemEquipped += UpdateEquipWeapon;
		UIEventHandler.OnLevelUP += UpdateLevel;
		InitalizeStats();
	}

	void UpdateHealth(int currentHealth, int maxHealth)
	{
		this.health.text = currentHealth.ToString();
		this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
	}
	
	void UpdateLevel()
	{
		this.level.text = player.PlayerLevel.Level.ToString();
		this.levelFill.fillAmount = (float)player.PlayerLevel.CurrentExperiance / (float)player.PlayerLevel.RequiredExperaince;
	}


	void InitalizeStats()
	{
		for (int i = 0; i < player.chatacterStats.stats.Count; i++)
		{
			playerStatsTexts.Add(Instantiate(playerStatPrefab));
			playerStatsTexts[i].transform.SetParent(playerStatPanel);
		}
		UpdateStats();
	}

		void UpdateStats()
	{
		for (int i = 0; i < player.chatacterStats.stats.Count; i++)
		{
			playerStatsTexts[i].text = player.chatacterStats.stats[i].StatName + ": " + player.chatacterStats.stats[i].GetCalculatedStatValue().ToString();
			
		}
	}
	void UpdateEquipWeapon(Item item)
	{
		weaponIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);
		weaponNameText.text = item.ItemName;

		for (int i = 0; i < item.Stats.Count; i++)
		{
			weaponStatTexts.Add(Instantiate(weaponStatPrefab));
			weaponStatTexts[i].transform.SetParent(weaponStatPanel);
			weaponStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
		}
		//UpdateStats();
	}

 public void UnequipWeapon()
    {
        weaponNameText.text = "-";
        weaponIcon.sprite = defaultWeaponSprite;
        for (int i = 0; i < weaponStatTexts.Count; i++)
        {
           // Destroy(weaponStatTexts[i].gameObject);
        }
        playerWeaponController.UnequipWeapon();
    }
	
}
