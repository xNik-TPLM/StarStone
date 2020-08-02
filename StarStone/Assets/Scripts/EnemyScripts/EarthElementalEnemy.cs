using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is a child to the enemy base.
/// This is an Earth Elemental enemy. Its health regenerates over time, it throws boulder at the player and is weak to wind attacks
/// Worked By: Nikodem Hamrol
/// </summary>

public class EarthElementalEnemy : EnemyBase
{
    //This private float is to time the enemy can throw the boulder
    private float m_enemyTimeToFire;

    //Earth Elemental Properties
    [Header("Earth Elemental Enemy Properties")]
    [Tooltip("This is the amount health the enemy will be regenerated per second")]
    public float RegeneratedHealth; //This is the amount of health the enemy will regerenate per second
    [Tooltip("This is the distance the enemy can throw the boulder at the player")]
    public float FiringDistance; //This is the distance between the player and the enemy, which will allow the enemy to throw the boulder at the player
    [Tooltip("This is the rate that the enemy will spawn the boulder")]
    public float EnemyMaxTimeToFire; //This is the time of spawning the boulder 
    [Tooltip("This is the object attached to this enemy, which will spawn the boulder")]
    public GameObject BoulderSpawnPoint; //This is where the boulder will spawn
    [Tooltip("This is the boulder prefab that will be thrown at the player")]
    public GameObject EarthBoulder; //This is what the enemy throw/shoot at the player

    //The start function is overridden to set the tiem to fire and the stopping distance of the enemy
    protected override void Start()
    {
        base.Start();
        m_enemyTimeToFire = EnemyMaxTimeToFire;
        m_enemyNavMesh.stoppingDistance = FiringDistance;
    }

    //This function is overridden to add the behaviours relating to this enemy, which health regeneration and it's attack
    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        RegenerateHealth();
        EarthElementalAttack();
    }

    //This function is overridden to deal different damage based on the elemental projectile
    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);

        //The projectile type will determine what sort of damage will it do this enemy
        switch(projectileType)
        {
            case 1: //Fire projectile will deal normal damage, with the addition of burning damage
                m_enemyCurrentHealth -= damage;
                m_isEnemyBurning = true;
                break;

            case 2: //Ice projectile will deal normal damage, with freezing the enemy
                m_enemyCurrentHealth -= damage;
                m_isEnemyFrozen = true;
                break;

            case 3: //Wind projectile will deal double damage as the earth is weak to wind
                m_enemyCurrentHealth -= damage * 2;
                break;

            case 4: //Earth projectile will deal normal damage
                m_enemyCurrentHealth -= damage;
                break;
        }
    }    
    
    //This function will regenerate the enemy's health per second
    private void RegenerateHealth()
    {
        //Check if health is less than max health
        if(m_enemyCurrentHealth < MaxHealth && m_isEnemyBurning == false)
        {
            //Regenerate health
            m_enemyCurrentHealth += RegeneratedHealth * Time.deltaTime;
        }
    }

    //This function is how this enemy will attack
    private void EarthElementalAttack()
    {
        //If the distance between the enemy and the player is less than the firing distance
        if(Vector3.Distance(transform.position, m_playerTarget.position) < FiringDistance)
        {
            m_isPlayerInRange = true; //Set the bool that the player is in range
            transform.position = transform.position; //Stop the enemy in its position
            transform.LookAt(m_playerTarget.position); //Aim at the enemy
        }
        else
        {
            m_isPlayerInRange = false; //Else, set the bool that the player is not in range, which will start moving towards the player again
            m_enemyTimeToFire = EnemyMaxTimeToFire; //Set the time to fire back to the max time to fire, to avoid the chance of the enemy shooting straight after when they stopped moving
        }

        //Check if the time to fire is less than or equal to 0 and that the player is in range
        if(m_enemyTimeToFire <= 0 && m_isPlayerInRange)
        {
            //Spawn the earth boulder and set the time to fire to enemy fire rate property
            Instantiate(EarthBoulder, BoulderSpawnPoint.transform.position, BoulderSpawnPoint.transform.rotation);
            m_enemyTimeToFire = EnemyMaxTimeToFire;
        }
        else //If the time to fire is bigger than 0, then count down the time to fire
        {
            m_enemyTimeToFire -= Time.deltaTime;
        }
    }
}
