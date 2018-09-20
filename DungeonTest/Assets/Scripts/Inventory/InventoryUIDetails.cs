using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIDetails : MonoBehaviour 
{
	Item item;
	Button selectedItemButton, itemInteractButton;
	public TextMeshProUGUI itemNameText, itemDescriptionText, ItemInteractButtonText;

	public TextMeshProUGUI statText;


	void Start()
	{
		itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
		itemDescriptionText = transform.Find("Item_Description").GetComponent<TextMeshProUGUI>();
		itemInteractButton = transform.Find("InteractButton").GetComponent<Button>();
		ItemInteractButtonText = itemInteractButton.transform.GetComponentInChildren<TextMeshProUGUI>();
		gameObject.SetActive(false);
	}


	public void SetItem(Item item, Button selectedButton)
	{
		gameObject.SetActive(true);
		statText.text = "";
		if(item.Stats != null)
		{
			foreach(BaseStat stat in item.Stats)
			{
				statText.text += stat.StatName + ": " + stat.BaseValue + "\n";
			}
		}
		itemInteractButton.onClick.RemoveAllListeners();
		this.item = item;
		selectedItemButton = selectedButton;
		itemNameText.text = item.ItemName;
		itemDescriptionText.text = item.Description;
		ItemInteractButtonText.text = item.ActionName;
		itemInteractButton.onClick.AddListener(OnItemInteract);
	}

	public void OnItemInteract()
	{
		if(item.ItemType == Item.ItemTypes.Consumable)
		{
			InventoryController.Instance.ConsumeItem(item);
			Destroy(selectedItemButton.gameObject);
		}
		else if (item.ItemType == Item.ItemTypes.Weapon)
		{
			InventoryController.Instance.EquipItem(item);
			Destroy(selectedItemButton.gameObject);
		}
		item = null;
		gameObject.SetActive(false);
	}

}
