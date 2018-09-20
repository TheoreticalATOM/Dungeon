using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
	private Animator animator;
	public List<BaseStat> Stats { get; set; }
	public int CurrentDamage { get; set; }

    public Transform ProjectileSpawn{ get; set; }
    FireBall fireball;

	void Start()
	{
		fireball = Resources.Load<FireBall>("Weapons/Projectiles/FireBall");
		ProjectileSpawn = GameObject.FindGameObjectWithTag("Projectile Spawn").transform;
		animator = GetComponent<Animator>();
	}
	public void PerformAttack(int damage)
	{
		animator.SetTrigger("Base_Attack");
	}
	public void PerformSpecial()
	{
		animator.SetTrigger("Special_Attack");
	}

	public void CastProjectile()
    {
        FireBall fireballInstance = (FireBall)Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.forward;
    }


}
