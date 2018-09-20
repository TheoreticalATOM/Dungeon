using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour 
{
	[SerializeField] private TextMeshProUGUI health, level;
	[SerializeField] private Image healthFill, levelFill;
	[SerializeField] private Player player;

	bool menuIsActive { get; set; }
	public RectTransform characterPanel;

	//Stats
	private List<TextMeshProUGUI> playerStatsTexts = new List<TextMeshProUGUI>();
	[SerializeField] private TextMeshProUGUI playerStatPrefab;
	[SerializeField] private Transform playerStatPanel;

	//Equipped Weapon Stuff
	[SerializeField] private Sprite defaultWeaponSprite;
	private PlayerWeaponController playerWeaponController;
	[SerializeField] private TextMeshProUGUI weaponStatPrefab;
	[SerializeField] private Transform weaponStatPanel;
	[SerializeField] private TextMeshProUGUI weaponNameText;
	[SerializeField] private Image weaponIcon;
	private List<TextMeshProUGUI> weaponStatTexts = new List<TextMeshProUGUI>();



	void Start () 
	{
		playerWeaponController = player.GetComponent<PlayerWeaponController>();
		UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
		UIEventHandler.OnStatsChanged += UpdateStats;
		UIEventHandler.OnItemEquipped += UpdateEquipWeapon;
		UIEventHandler.OnLevelUP += UpdateLevel;
		InitalizeStats();
		UpdateHealth(100, 100);
		UpdateLevel();
		characterPanel.gameObject.SetActive(false);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.U))
		{
			menuIsActive = !menuIsActive;
			characterPanel.gameObject.SetActive(menuIsActive);
		}
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
            //Destroy(weaponStatTexts[i].gameObject);
        }
        playerWeaponController.UnequipWeapon();
    }
	
}
