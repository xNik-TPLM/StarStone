using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if (enemyDetected != null)
            {
                //Debug.Log(colliderDetected.gameObject.name);
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
