using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public ChatacterStats chatacterStats;
	public int currentHealth;
	public int maxHealth;
	public PlayerLevel PlayerLevel { get; set; }

	void Start()
	{
		PlayerLevel = GetComponent<PlayerLevel>();
		this.currentHealth = this.maxHealth;
		chatacterStats = new ChatacterStats(10,10,10);
	}


	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if(currentHealth <=0)
		{
			Die();
		}
		UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
	}

	private void Die()
	{
		Debug.Log("Player Dead, Health Reset");
		this.currentHealth = this.maxHealth;
		UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
	}


}
