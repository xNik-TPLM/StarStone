using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementalEnemy : EnemyBase
{
    public float FiringDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject FireProjectile;

    protected override void Start()
    {
        base.Start();
        timeBtwShots = startTimeBtwShots;
        m_enemyNavMesh.stoppingDistance = FiringDistance;
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        if(Vector3.Distance(transform.position, Target.position) < FiringDistance)
        {
            Debug.Log("In Range");
            isPlayerInRange = true;
            transform.position = transform.position;
        }
        else
        {
            isPlayerInRange = false;
        }

        if(timeBtwShots <= 0 && isPlayerInRange)
        {
            Instantiate(FireProjectile, transform.position, transform.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
