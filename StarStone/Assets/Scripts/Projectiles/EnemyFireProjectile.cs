using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child to the projectile base script
/// This script controls the Fire enemy's projectile when it's instantiated
/// Worked By: Nikodem Hamrol
/// </summary>

public class EnemyFireProjectile : ProjectileBase
{
    //Fire projectile fields
    //This transform gets the player's current position
    private Transform m_playerTarget;

    //This Vector 3 gets the target position, which is the player's position before the projectile got instantiated, which stop the projectile following the player
    private Vector3 m_targetPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //Once the projectile is instantiated, set the player target, by finding the player controller, and use that transform to set the target position
        m_playerTarget = FindObjectOfType<PlayerController>().transform;
        m_targetPosition = new Vector3(m_playerTarget.position.x, m_playerTarget.position.y, m_playerTarget.position.z);
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Move the projectile towards the target position, using projectile speed
        transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, ProjectileSpeed * Time.deltaTime);

        //Count down the life of the projectile
        m_projectileLifeTimer -= Time.deltaTime;

        //if the time runs out then it will destroy the projectile
        if (m_projectileLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Overriding this function, means that the projectile can apply damage to the player
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        //If the projectile has hit the player. It will deal damage and enable burning
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerController>().PlayerDamage(ProjectileDamage, 1);
        }
    }
}
