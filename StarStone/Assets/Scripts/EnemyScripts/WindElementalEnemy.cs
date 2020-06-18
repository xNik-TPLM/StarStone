using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElementalEnemy : EnemyBase
{
    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);
        switch (projectileType)
        {
            case 1:
                CurrentHealth -= damage * 2;
                m_isEnemyBurning = true;
                break;
        }
    }
}
