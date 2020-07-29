using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child to the enemy base.
/// This is a Wind Elemental enemy. Its faster than any other enemy and it attacks by detonating on when the player is within the proximity
/// Worked By: Nikodem Hamrol
/// </summary>

public class WindElementalEnemy : EnemyBase
{
    //Wind elemental enemy fields
    //This boolean checks if detonation is enabled, which will detonatie enemy dealing damage to the player, if it's in the area.
    private bool m_detonationEnabled; 

    [Header("Wind Elemental Properties")]
    [Tooltip("This is the maximum time for detonation")]
    public float MaxDetonationTime; //This is the max timebefore the enemy will detonate
    [Tooltip("This is the damage it will deal to the player after detonation")]
    public float DetonationDamage; //This is the damage after detonation
    [Tooltip("This is the radius of detection for detonation")]
    public float DetonationRadius;
    [Tooltip("This is the visual special effect to be used fro detonation")]
    public GameObject ExplosionVFX; //This is the vfx that will activate after enemy's detonation

    //This function is overridden to deal different damage based on the elemental projectile
    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);

        //The projectile type will determine what sort of damage will it do this enemy
        switch (projectileType)
        {
            case 1: //Fire projectile will deal normal damage, with the addition of burning damage
                m_enemyCurrentHealth -= damage;
                m_isEnemyBurning = true;
                break;

            case 2: //Ice projectile will deal double damage, as wind is weak to ice, and it will freeze the enemy
                m_enemyCurrentHealth -= damage * 2;
                m_isEnemyFrozen = true;
                break;

            case 3: //Wind projectile will deal normal damage
                m_enemyCurrentHealth -= damage;
                break;

            case 4: //Earth projectile will deal normal damage and will give the player some health
                m_enemyCurrentHealth -= damage;
                m_playerReference.currentHealth += HealthToPlayer;
                break;
        }
    }

    //This function is overridden to add a behaviour relating to this enemy type which is the detonation
    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
    }

    //When a player enters and stays within the enemy's sphere collider
    public void OnTriggerStay(Collider other)
    {
        //Check if the object is tagged as player
        if (other.CompareTag("Player"))
        {
            //Start detonation coroutine
            StartCoroutine(Detonation());

            //If detonation is enable
            if(m_detonationEnabled == true)
            {
                //damage the player and disable detonation, to stop potential duplicated damage
                other.gameObject.GetComponent<PlayerController>().PlayerDamage(DetonationDamage, 0);
                m_detonationEnabled = false;
            }
        }
    }

    //This coroutine will handle the detonation of the enemy
    private IEnumerator Detonation()
    {
        //if the player has entered the enemy's trigger, it will stop the enemy and start the damaging coroutine
        m_isPlayerInRange = true;
        StartCoroutine(DamagingDetonation());

        //Wait until detonation tme runs out
        yield return new WaitForSeconds(MaxDetonationTime);

        //If the the enemy is not yet destroyed, kill the enemy and instantiate the explosion visual effect
        if (gameObject != null)
        {
            m_enemyCurrentHealth = 0;
            Instantiate(ExplosionVFX, transform.position, transform.rotation);
        }
    }

    //This coroutine will detonate before normal detonation, in case the player is still inside the trigger
    private IEnumerator DamagingDetonation()
    {
        //Wait the same amount of time, but a frame before
        yield return new WaitForSeconds(MaxDetonationTime - 0.1f);

        //Enable detonation, kill the enemy and instantiate the explosion visual effect
        m_detonationEnabled = true;
        m_enemyCurrentHealth = 0;
        Instantiate(ExplosionVFX, transform.position, transform.rotation);

    }
}
