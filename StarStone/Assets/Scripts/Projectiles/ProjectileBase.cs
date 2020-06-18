using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a base script for projectiles in the game. As there will
/// It will mainly check collision against objects, such as enemies. It also holds properties, such as projectile speed and damage.
/// Worked By: Nikodem Hamrol
/// References: Gabriel Aguiar Prod. (2018). Unity 2018 - Game VFX - Projectile/Bullet Raycast Tutorial [online]. Available: https://www.youtube.com/watch?v=xenW67bXTgM [Last Accessed 9th June June 2020].
/// </summary>

public class ProjectileBase : MonoBehaviour
{
    //Projectile feilds
    //This folat will get the property value of projectile duration and use it to count down the life of a projectile
    private float m_projectileLifeTimer;

    public float ProjectileDuration;
    public float ProjectileSpeed;
    public float ProjectileDamage;

    public static int ProjectileType = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_projectileLifeTimer = ProjectileDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (ProjectileSpeed * Time.deltaTime);

        m_projectileLifeTimer -= Time.deltaTime;

        if(m_projectileLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Enemy.EnemyDamaged(ProjectileDamage);
            collision.collider.gameObject.GetComponent<EnemyBase>().EnemyDamaged(ProjectileDamage, InteractStarStone.StarStoneID);


            //Debug.Log("Hit");
        }

        Destroy(gameObject);
    }
}
