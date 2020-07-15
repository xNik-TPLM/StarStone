using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Worked By: Nikodem Hamrol
/// </summary>

public class WindElementalEnemy : EnemyBase
{
    private bool m_isPlayerInDetonationRange;
    //private PlayerController m_player;

    private float m_detonationTime;

    [Header("Wind Elemental Properties")]
    public float DetonationTimer;
    public float DetonationDamage;
    public float DetonationRadius;

    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);
        switch (projectileType)
        {
            case 1:
                CurrentHealth -= damage;
                m_isEnemyBurning = true;
                break;

            case 2:
                CurrentHealth -= damage * 2;
                m_isEnemyBurning = true;
                break;

            case 3:
                CurrentHealth -= damage;
                break;
        }
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        EnemyDetonation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isPlayerInDetonationRange = true;

            if (m_detonationTime > (DetonationTimer - 0.1))
            {
                other.gameObject.GetComponent<PlayerController>().PlayerDamage(DetonationDamage);
                CurrentHealth = 0;
            }
        }
    }

    private void EnemyDetonation()
    {
        if (m_isPlayerInDetonationRange == true)
        {
            isPlayerInRange = true;
            m_detonationTime += Time.deltaTime;

            if (m_detonationTime > DetonationTimer)
            {
                CurrentHealth = 0;
            }
        }
    }
}
