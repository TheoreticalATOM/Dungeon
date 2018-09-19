using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Interactable, IEnemy 
{
	public LayerMask aggroLayerMask;
	public float currentHealth;
	public float maxHealth;
	public int ID { get; set; }
	public int Experiance { get; set; }
	public DropTable dropTable { get; set; }
	public Spawner Spawner { get; set; }
	public PickUpItem pickUpItem;

	private Player player;
	private NavMeshAgent navAgent;
	private ChatacterStats chatacterStats;
	private Collider[] withinAggroColliders;

	void Start()
	{
		dropTable = new DropTable();
		dropTable.loot = new List<LootDrop>
		{
			new LootDrop("sword", 25),
			new LootDrop("staff", 25),
			new LootDrop("potion_log", 25)
		};
		
		ID=0;
		Experiance = 20;
		navAgent = GetComponent<NavMeshAgent>();
		chatacterStats = new ChatacterStats(6, 10, 2);
		currentHealth = maxHealth;
	}

	void FixedUpdate() //Maybe Performance Problem
	{
		withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask);
		if(withinAggroColliders.Length > 0)
		{
			ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
		}
	}

	public void PerformAttack()
	{
		player.TakeDamage(5);
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if(currentHealth <=0)
		{
			Die();
		}
	}

	void ChasePlayer(Player player)
	{
			navAgent.SetDestination(player.transform.position);

		this.player = player;
		if(navAgent.remainingDistance <= navAgent.stoppingDistance)
		{
			if(!IsInvoking("PerformAttack"))
			{
				InvokeRepeating("PerformAttack", .5f, 2f);
			}

		}
		else
		{
			CancelInvoke("PerformAttack");
		}

	}

	public void Die()
	{
		DropLoot();
		CombatEvents.EnemyDied(this);
		this.Spawner.Respawn();
		Destroy(gameObject);
	}

	void DropLoot()
	{
		Item item = dropTable.GetDrop();
		if(item != null)
		{
			PickUpItem instance = Instantiate(pickUpItem, transform.position, Quaternion.identity);
			instance.ItemDrop = item;
		}
	}

}
