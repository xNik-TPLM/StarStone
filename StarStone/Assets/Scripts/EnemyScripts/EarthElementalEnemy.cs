using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is a child to the enemy base.
/// This is an Earth Elemental enemy. His health regenerates over time and is weak to wind attacks
/// Worked By: Nikodem Hamrol
/// </summary>

public class EarthElementalEnemy : EnemyBase
{
    private float m_enemyFireRateTime;


    public float FiringDistance;
    public float EnemyFireRate;

    //Earth Elemental Properties
    //Float Properties
    [Header("Earth Elemental Properties")]
    public float RegeneratedHealth; //This is the amount of health points the enemy will regerenate per second
    public GameObject ProjectileSpawnPoint;
    public GameObject EarthBoulder;

    protected override void Start()
    {
        base.Start();
        m_enemyFireRateTime = EnemyFireRate;
        m_enemyNavMesh.stoppingDistance = FiringDistance;
    }


    //Override this function to add the regenerate health function
    protected override void EnemyHealth()
    {
        base.EnemyHealth();
        RegenerateHealth();
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        EarthElementalAttack();
    }

    //This function is overridden to deal different damage based on the elemental projectile
    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);
        switch(projectileType)
        {
            case 1:
                CurrentHealth -= damage;
                m_isEnemyBurning = true;
                break;

            case 2:
                CurrentHealth -= damage;
                m_isEnemyFrozen = true;
                break;

            case 3: 
                CurrentHealth -= damage;
                break;

            case 4:
                CurrentHealth -= damage;
                break;
        }
    }    
    
    //This function will regenerate the enemy's health per second
    private void RegenerateHealth()
    {
        //Check if health is less than max health
        if(CurrentHealth < MaxHealth && m_isEnemyBurning == false)
        {
            //Regenerate health
            CurrentHealth += RegeneratedHealth * Time.deltaTime;
        }
    }

    private void EarthElementalAttack()
    {
        if(Vector3.Distance(transform.position, Target.position) < FiringDistance)
        {
            isPlayerInRange = true;
            transform.position = transform.position;
        }
        else
        {
            isPlayerInRange = false;
        }

        if(m_enemyFireRateTime <=0 && isPlayerInRange)
        {
            Instantiate(EarthBoulder, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
            m_enemyFireRateTime = EnemyFireRate;
        }
        else
        {
            m_enemyFireRateTime -= Time.deltaTime;
        }
    }
}
