using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour 
{
	public int Level { get; set; }
	public int CurrentExperiance { get; set; }
	public int RequiredExperaince { get { return Level * 25; } } //Change for level curve

	void Start () 
	{
		CombatEvents.OnEnemyDeath += EnemyToExperiance;
		Level = 1;
	}

	public void EnemyToExperiance(IEnemy enemy)
	{
		GrantExperiance(enemy.Experiance);
	}

	public void GrantExperiance(int amount)
	{
		CurrentExperiance += amount;
		while(CurrentExperiance >= RequiredExperaince)
		{
			CurrentExperiance -= RequiredExperaince;
			Level++;
		}
		UIEventHandler.PlayerLevelUp();
	}
	

}
