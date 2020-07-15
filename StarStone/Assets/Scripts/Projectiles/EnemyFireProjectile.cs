using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireProjectile : ProjectileBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().PlayerDamage(ProjectileDamage, 1);
            Destroy(gameObject);
        }
    }

}
