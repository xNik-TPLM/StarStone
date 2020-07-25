﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireProjectile : ProjectileBase
{
    private Transform m_playerm_playerTarget;

    private Vector3 m_m_playerTargetPosition;

    protected override void Start()
    {
        base.Start();

        m_playerm_playerTarget = FindObjectOfType<PlayerController>().transform;

        m_m_playerTargetPosition = new Vector3(m_playerm_playerTarget.position.x, m_playerm_playerTarget.position.y, m_playerm_playerTarget.position.z);
    }

    protected override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_m_playerTargetPosition, ProjectileSpeed * Time.deltaTime);

        //Count down the life of the projectile
        m_projectileLifeTimer -= Time.deltaTime;

        //if the time runs out then it will destroy the projectile
        if (m_projectileLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerController>().PlayerDamage(ProjectileDamage, 1);
        }

        base.OnCollisionEnter(collision);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().PlayerDamage(ProjectileDamage, 1);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

}
