using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElementalEnemy : EnemyBase
{
    private bool m_isPlayerInDetonationRange;

    private float m_detonationTime;

    public float DetonationTimer;

    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);
        switch (projectileType)
        {
            case 2:
                CurrentHealth -= damage * 2;
                m_isEnemyBurning = true;
                break;
        }
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
        EnemyDetonation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isPlayerInDetonationRange = true;
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
                Destroy(gameObject);
                CurrentHealth = 0;
            }
        }
    }
}
