using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementalEnemy : EnemyBase
{
    //Fire elemental fields
    private float m_enemyFireRateTime;
    

    public float FiringDistance;
    public float EnemyFireRate;


    public GameObject FireProjectile;

    protected override void Start()
    {
        base.Start();
        m_enemyFireRateTime = EnemyFireRate;
        m_enemyNavMesh.stoppingDistance = FiringDistance;
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        if(Vector3.Distance(transform.position, Target.position) < FiringDistance)
        {
            isPlayerInRange = true;
            transform.position = transform.position;
        }
        else
        {
            isPlayerInRange = false;
        }

        if(m_enemyFireRateTime <= 0 && isPlayerInRange)
        {
            Instantiate(FireProjectile, transform.position, transform.rotation);
            m_enemyFireRateTime = EnemyFireRate;
        }
        else
        {
            m_enemyFireRateTime -= Time.deltaTime;
        }
    }

    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);
        switch (projectileType)
        {
            case 1:
                CurrentHealth -= damage;
                break;

            case 2:
                CurrentHealth -= damage;
                m_isEnemyFrozen = true;
                break;

            case 3:
                CurrentHealth -= damage;
                break;

            case 4:
                CurrentHealth -= damage * 2;
                break;
        }
    }
}
