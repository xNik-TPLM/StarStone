using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child to the projectile base script
/// This script controls the Earth enemy's boulder when it's instantiated
/// Worked By: Nikodem Hamrol
/// </summary>

public class EnemyBoulderProjectile : ProjectileBase
{
    [Header("Boulder Projectile Properties")]
    [Tooltip("This is the radius od the collision of buolder when hits an object")]
    public float BlastRadius; //The radius of checking for each collider in the scene, it will be used to check if the player is within that radius to deal damage

    //This function will check for all colliders and if it's a player then it will deal damage to the player
    private void BoulderCollision()
    {
        //Create a collider array that will check for all collisions within the radius. This means the player can get hit when the boulder hits close to the player
        Collider[] m_collisionDetected = Physics.OverlapSphere(transform.position, BlastRadius);

        //Go through the whole collider array
        foreach(Collider colliderDetected in m_collisionDetected)
        {
            //Set the player controller so that the collider array can search for it
            PlayerController playerDetected = colliderDetected.GetComponentInParent<PlayerController>();

            //If it exists within the collider array, it will deal damage
            if(playerDetected != null)
            {
                playerDetected.PlayerDamage(ProjectileDamage, 0);
            }
        }
    }

    //Overriding this function, means that the boulder can apply damage to the player
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        BoulderCollision();
    }
}
