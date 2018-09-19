using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sword : MonoBehaviour, IWeapon 
{
	private Animator animator;
	public List<BaseStat> Stats { get; set; }
	public ChatacterStats chatacterStats { get; set; }
	public int CurrentDamage { get; set; }

	void Start()
	{
		animator = GetComponent<Animator>();
	}
	public void PerformAttack(int damage)
	{
		CurrentDamage = damage;
		animator.SetTrigger("Base_Attack");
	}
	public void PerformSpecial()
	{
		animator.SetTrigger("Special_Attack");
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Enemy")
		{
			col.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
		}
	}
}
