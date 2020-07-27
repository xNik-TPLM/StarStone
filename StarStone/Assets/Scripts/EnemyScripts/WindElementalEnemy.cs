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
    private bool m_isPlayerInDetonationRange; //This boolean will chekc if the player has entered the trigger sphere to enable countdown for detonation
    private bool m_detonationEnabled;
    private float m_detonationTime; //This float is the time of detonation which will count up until it reaches max detonation time

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
        EnemyDetonation();
    }

    //When a player enters and stays within the enemy's sphere collider
    public void OnTriggerStay(Collider other)
    {
        //Check if the object is tagged as player
        if (other.CompareTag("Player"))
        {
            //Set detonation to true, which will enable detonation timer
            m_isPlayerInDetonationRange = true;
            StartCoroutine(Detonation());

            if(m_detonationEnabled == true)
            {
                other.gameObject.GetComponent<PlayerController>().PlayerDamage(DetonationDamage, 0);
                m_enemyCurrentHealth = 0;
                Instantiate(ExplosionVFX, transform.position, transform.rotation);
                m_detonationEnabled = false;
            }

            //If detonation time is up. The "-0.1" is there so that it can deal damage to the player a frame before the enemy gets destroyed
            /*if (m_detonationTime > (MaxDetonationTime - 0.1))
            {
                //Deal damage to the player and set health to 0, which will destroy the enemy and spawn the visual effect
                other.gameObject.GetComponent<PlayerController>().PlayerDamage(DetonationDamage, 0);
                m_enemyCurrentHealth = 0;
                Instantiate(explosionVFX, transform.position, transform.rotation);
            }*/
        }
    }

    //This function will handle the detonation of the enemy
    private void EnemyDetonation()
    {
        //If the player is in detonation range
        /*if (m_isPlayerInDetonationRange == true)
        {
            //It will stop the enemy, by setting player range to true, and start counting detonation time
            m_isPlayerInRange = true;
            m_detonationTime += Time.deltaTime;

            //Check if deontation time is bigger than max detonation time
            if (m_detonationTime > MaxDetonationTime)
            {
                //Set enemy's health to 0 and spawn the visual effect
                m_enemyCurrentHealth = 0;
                Instantiate(explosionVFX, transform.position, transform.rotation);
            }
        }*/
    }

    private IEnumerator Detonation()
    {
        if (m_isPlayerInDetonationRange == true)
        {
            m_isPlayerInRange = true;
            StartCoroutine(DamagingDetonation());
            yield return new WaitForSeconds(MaxDetonationTime);

            m_enemyCurrentHealth = 0;
            Instantiate(ExplosionVFX, transform.position, transform.rotation);
        }
    }

    private IEnumerator DamagingDetonation()
    {
        yield return new WaitForSeconds(MaxDetonationTime - 0.1f);
        m_detonationEnabled = true;
    }
}
