﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour 
{
	public static InventoryController Instance { get; set; }
	public PlayerWeaponController playerWeaponController;
	public ConsumableController consumableController;
	public InventoryUIDetails inventoryDetailsPanel;
	public List<Item> playerItems = new List<Item>();

	void Start()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
		Instance = this;
		}

		playerWeaponController = GetComponent<PlayerWeaponController>();
		consumableController = GetComponent<ConsumableController>();
		//Placeholer that gives items at start
		GiveItem("sword");
		GiveItem("potion_log");
		GiveItem("staff");
	}

	public void GiveItem(string itemSlug)
	{
		Item item = ItemDatabase.Instance.GetItem(itemSlug);
		playerItems.Add(item);
		UIEventHandler.ItemAddedToInventory(item);
	}

		public void GiveItem(Item item)
	{
		playerItems.Add(item);
		UIEventHandler.ItemAddedToInventory(item);
	}

	public void SetItemDetails(Item item, Button selectedButton)
	{
		inventoryDetailsPanel.SetItem(item, selectedButton);
	}

	public void EquipItem(Item itemToEquip)
	{
		playerWeaponController.EquipWeapon(itemToEquip);
	}

	public void ConsumeItem(Item itemToConsume)
	{
		consumableController.ConsumeItem(itemToConsume);
	}

}