using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour 
{
	ChatacterStats stats;

	void Start()
	{
		stats = GetComponent<Player>().chatacterStats;
	}

	public void ConsumeItem(Item item)
	{
		GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));
		if(item.ItemModifier)
		{
			itemToSpawn.GetComponent<IConsumable>().Consume(stats);
		}
		else
		itemToSpawn.GetComponent<IConsumable>().Consume();
	}

}
