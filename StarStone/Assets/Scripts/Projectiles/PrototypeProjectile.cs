using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Worked By: Nikodem Hamrol
/// </summary>

public class PrototypeProjectile : ProjectileBase
{
    [Header("Prototype Projectile Properties")]
    public float BlastRadius;

    private void ProjectileCollision()
    {
        Collider[] collisionsDetected = Physics.OverlapSphere(transform.position, BlastRadius);

        foreach (Collider colliderDetected in collisionsDetected)
        {   
            EnemyBase enemyDetected = colliderDetected.GetComponent<EnemyBase>();

            if (enemyDetected != null && colliderDetected.isTrigger == false)
            {
                enemyDetected.EnemyDamaged(ProjectileDamage, InteractStarStone.StarStoneID);
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        ProjectileCollision();
    }
}
