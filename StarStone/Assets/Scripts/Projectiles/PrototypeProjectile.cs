using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child to the projectile base script
/// This script controls all prototype projectiles that's instantiated from the prototype weapon
/// Worked By: Nikodem Hamrol
/// </summary>

public class PrototypeProjectile : ProjectileBase
{
    [Header("Prototype Projectile Properties")]
    [Tooltip("This is the radius the projectile can affect enemies")]
    public float BlastRadius; //The radius of checking for each collider in the scene, it will be used to check if enemies are within that radius to deal damage

    //This function will check for all colliders and if enemies get detected then it will deal damage to the enemies detected
    private void ProjectileCollision()
    {
        //Create a collider array that will check for all collisions within the radius. This means each enemy can get hit when the projectile hits close to them
        Collider[] collisionsDetected = Physics.OverlapSphere(transform.position, BlastRadius);

        //Go through the whole collider array
        foreach (Collider colliderDetected in collisionsDetected)
        {
            //Set the enemy base so that the collider array can search for it
            EnemyBase enemyDetected = colliderDetected.GetComponent<EnemyBase>();

            //If an enemy exists within the collider array, and ignore trigger colliders, it will deal damage
            if (enemyDetected != null && colliderDetected.isTrigger == false)
            {
                enemyDetected.EnemyDamaged(ProjectileDamage, InteractStarStone.StarStoneID);
            }
        }
    }

    //Overriding this function, means that the prototype projectile can apply damage to the enemies
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        ProjectileCollision();
    }
}
