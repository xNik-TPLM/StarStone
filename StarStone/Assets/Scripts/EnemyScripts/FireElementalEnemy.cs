using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child to the enemy base.
/// This is a Fire Elemental enemy. It can shoot fire projectiles at the player which can cause burning damage to them
/// Worked By: Nikodem Hamrol
/// </summary>
public class FireElementalEnemy : EnemyBase
{
    //Fire elemental enemy fields
    //This float is the time that enemy will shot the projectile
    private float m_enemyTimeToFire;
    
    //Fire enemy properties
    [Header("Fire Elemental Enemy Properties")]
    [Tooltip("This is the distance the enemy can its projectile at the player")]
    public float FiringDistance; //This is the distance between the player and the enemy, which will allow the enemy to shoot at the player
    [Tooltip("This is the maximum time the projectile will spawn")]
    public float EnemyMaxTimeToFire; //This is the time of spawning the projectile
    [Tooltip("This is the object attached to this enemy, which will spawn the boulder")]
    public GameObject ProjectileSpawnPoint; //This is where the boulder will spawn
    [Tooltip("This is the projectile prefab that will be fired at the player")]
    public GameObject FireProjectile; //This is what the enemy throw/shoot at the player

    //The start function is overridden to set the time to fire and the stopping distance of the enemy
    protected override void Start()
    {
        base.Start();
        m_enemyTimeToFire = EnemyMaxTimeToFire;
        m_enemyNavMesh.stoppingDistance = FiringDistance;
    }

    //This function is overridden to add a behaviour relating to this enemy type which is firing the fire projectile
    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        //If the distance between the enemy and the player is less than the firing distance
        if (Vector3.Distance(transform.position, m_playerTarget.position) < FiringDistance)
        {
            m_isPlayerInRange = true; //Set the bool that the player is in range
            transform.position = transform.position; //Stop the enemy in its position
            transform.LookAt(new Vector3(m_playerTarget.position.x, gameObject.transform.position.y, m_playerTarget.position.z)); //Aim at the player
        }
        else
        {
            m_isPlayerInRange = false;  //Else, set the bool that the player is not in range, which will start moving towards the player again
            m_enemyTimeToFire = EnemyMaxTimeToFire; //Set the time to fire back to the max time to fire, to avoid the chance of the enemy shooting straight after when they stopped moving
        }

        //Check if the time to fire is less than or equal to 0 and that the player is in range
        if (m_enemyTimeToFire <= 0 && m_isPlayerInRange)
        {
            //Spawn the earth boulder and set the time to fire to enemy fire rate property
            Instantiate(FireProjectile, ProjectileSpawnPoint.transform.position, transform.rotation);
            m_enemyTimeToFire = EnemyMaxTimeToFire;
        }
        else //If the time to fire is bigger than 0, then count down the time to fire
        {
            m_enemyTimeToFire -= Time.deltaTime;
        }
    }

    //This function is overridden to deal different damage based on the elemental projectile
    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);

        //The projectile type will determine what sort of damage will it do this enemy
        switch (projectileType)
        {
            case 1: //Fire projectile will deal normal damage
                m_enemyCurrentHealth -= damage;
                break;

            case 2: //Ice projectile will deal normal damage, with freezing the enemy
                m_enemyCurrentHealth -= damage;
                m_isEnemyFrozen = true;
                break;

            case 3: //Wind projectile will deal normal damage
                m_enemyCurrentHealth -= damage;
                break;

            case 4: //Earth projectile will deal double damage, as fire is weak to earth and will give the player some health
                m_enemyCurrentHealth -= damage * 2;
                m_playerReference.currentHealth += HealthToPlayer;
                break;
        }
    }
}
