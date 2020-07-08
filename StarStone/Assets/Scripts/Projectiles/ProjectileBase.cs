using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a base script for projectiles in the game.
/// It will mainly check collision against objects, such as enemies. It also holds properties, such as projectile speed and damage.
/// Worked By: Nikodem Hamrol
/// References: Gabriel Aguiar Prod. (2018). Unity 2018 - Game VFX - Projectile/Bullet Raycast Tutorial [online]. Available: https://www.youtube.com/watch?v=xenW67bXTgM [Last Accessed 9th June June 2020].
/// </summary>

public class ProjectileBase : MonoBehaviour
{
    //Projectile field
    //This float will get the property value of a projectile duration and uses it to count down the life of a projectile
    private float m_projectileLifeTimer;

    protected bool projectileDestroyed;

    //Projectile properties
    //Flaot properties
    public float ProjectileDuration; //This is the duration of each projectile that will get instantiated
    public float ProjectileSpeed; //The speed of the projectile
    public float ProjectileDamage; //The damage the that the projectile will deal to the target

    // Start is called before the first frame update
    void Start()
    {
        //Set the the timer using the duration property
        m_projectileLifeTimer = ProjectileDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the direction of the projectile which will be travelling
        transform.position += transform.forward * ProjectileSpeed * Time.deltaTime;

        //Count down the life of the projectile
        m_projectileLifeTimer -= Time.deltaTime;

        //if the time runs out then it will destroy the projectile
        if(m_projectileLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    //As the projectile base, this will check, if the projectile has collided with anyhting and it will destroy itself
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {   
            Destroy(gameObject);
            
        }
    }
}
