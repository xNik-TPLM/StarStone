﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script handles the player's offensive ability
/// Worked By: Ben Smith
/// </summary>
public class OffensiveAbility : MonoBehaviour
{
    // Public float variables
    public float radius = 5f; //  This sets the radius at which the nuke affects
    public float force = 700f; // This sets the amount of force that the objects within the radius will experience
    public float nukeDamage; // This sets the damage that the nuke has on enemies
    private EnemyBase m_enemy;

    private bool m_timerEnabled;
    private float m_timer;
    public float Timer;

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = GetComponent<EnemyBase>();
        m_timer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        StopwatchCooldown();
        TimerCooldown();
        //if (timer > 0)
        //{
        //    timer -= Time.deltaTime;
        //}
        // This checks whether the player has pressed the offensive ability key
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_timerEnabled = true;
            
        }
        
    }
    private void StopwatchCooldown()
    {
        //If something you want is enabled
        if (m_timerEnabled == true)
        {
            m_timer += Time.deltaTime;

            if (m_timer > Timer)
            {
                m_timerEnabled = false;
                m_timer = 0;
                //Do whatever
                Explode(); // This runs the nuke function
            }
        }
    }
    private void TimerCooldown()
    {
        //If something you want is enabled
        if (m_timerEnabled == true)
        {
            m_timer -= Time.deltaTime;

            if (m_timer <= 0)
            {
                m_timerEnabled = false;
                m_timer = Timer;
                //Do whatever
                Explode(); // This runs the nuke function
            }
        }
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // This puts all of the objects affected by the nuke into an array
        
        // For each object in the array, the nuke will apply a force to
        foreach (Collider nearbyObject in colliders)
        {
            
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>(); // This gets all of the rigidbodies that the nuke affects
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius); // This applies the force to the objects in the array
                m_enemy.EnemyDamaged(nukeDamage, 0);
            }
        }
    }
}