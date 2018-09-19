using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLog : MonoBehaviour, IConsumable
{
	public void Consume()
	{
		Debug.Log("You drank the potion, top");
		Destroy(gameObject);
	}

	public void Consume(ChatacterStats stats)
	{
		Debug.Log("You drank the potion, bot");
	}

}
