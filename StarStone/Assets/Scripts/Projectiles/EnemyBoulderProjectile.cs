using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoulderProjectile : ProjectileBase
{
    [Header("Boulder Projectile Properties")]
    public float BlastRadius;

    private void BoulderCollision()
    {
        Collider[] m_collisionDetected = Physics.OverlapSphere(transform.position, BlastRadius);

        foreach(Collider colliderDetected in m_collisionDetected)
        {
            PlayerController playerDetected = colliderDetected.GetComponentInParent<PlayerController>();

            if(playerDetected != null)
            {
                playerDetected.PlayerDamage(ProjectileDamage);
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        BoulderCollision();
    }
}
