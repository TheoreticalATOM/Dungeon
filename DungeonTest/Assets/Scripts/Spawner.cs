using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public GameObject monster;
    public bool respawn;
    public float spawnDelay;
    private float currentTime;
    private bool spawning;


	void Start () 
	{
        Spawn();
        currentTime = spawnDelay;
	}
	
	void Update () 
	{
		if (spawning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Spawn();
            }
        }
	}

    public void Respawn()
    {
        if(respawn == true)
        {
        spawning = true;
        currentTime = spawnDelay;
        }
        if(respawn == false)
        {
        spawning = false;
        Destroy(this);
        }
    }

    void Spawn()
    {
        IEnemy instance = Instantiate(monster, transform.position, Quaternion.identity).GetComponent<IEnemy>();
        instance.Spawner = this;
        spawning = false;
    }
}
