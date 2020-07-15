﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// This script is a base script for each enemy in the game
/// It holds the properties, such as Health, and functionalities, such as displaying health and when enemy is damaged.
/// Worked By: Nikodem Hamrol
/// References: Dapper Dino. (2018). Unity Tutorial - How to make Enemy Healthbars [online]. Available: https://www.youtube.com/watch?v=ZYeXmze5gxg [Last Accessed 9th June 2020].
/// </summary>

public class EnemyBase : MonoBehaviour
{
    //Enemy fields
    //This float is the duration of bruning that will be checked if it reaches the "Burning Time" property
    private float m_enemyBurningTimer;
    private float m_enemyFreezeTimer;

    private float m_enemySpeed;

    protected PlayerController m_playerReference;
    
    //Protected fields
    //protected booleans
    protected bool isPlayerInRange; //This will check if the enemy is in range with the player for detonation, or shooting
    protected bool m_isEnemyBurning; //This will check if the enemy is burning   
    protected bool m_isEnemyFrozen; 
    
    //This transform will get the location of the player so that the AI will know what their target is
    protected Transform Target;

    //This navmesh agent will be used as reference to the component in our enemy object
    protected NavMeshAgent m_enemyNavMesh;

    //Enemy properties    
    //Float properties
    [Header("Health")]
    public float CurrentHealth; //This keeps track of how much health does the enemy have
    public float MaxHealth; //This sets the max health of an enemy

    [Header("Speed")]
    public float MaxEnemySpeed; //This sets the max speed of our enemy

    [Header("Elemental Effects")]
    public float BurningTime; //This sets the burning time of our enemy
    public int BurningDamage; //This sets the burning damage of our enemy
    public float FreezeTime; //This sets the freeze time of our enemy
    public float HealthToPlayer; //This the amount of health the enemy will give to the player when hit by an earth projectile

    [Header("UI")]
    //Unity properties
    public GameObject HealthBarUI; //This is the reference to the canvas of our health bar so that we can deactivate it when health is full
    public Slider HealthBarSlider; //this is the reference to the slider, which change the value of the slider

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_enemyNavMesh = GetComponent<NavMeshAgent>(); //Get a reference to the navmesh agent component
        m_playerReference = FindObjectOfType<PlayerController>();
        Target = GameObject.FindGameObjectWithTag("Player").transform; //Get the transform of our player to be used at destination for the AI

        //Set the enemy speed and health using the set properties
        m_enemySpeed = MaxEnemySpeed;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHealth();
        EnemyBehaviour();
        EnemyBurning();
        EnemyFreeze();
    }

    //This function returns the health to display it on the enemy's health bar
    private float ReturnHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    //This function will handle everything to do with health and display it
    protected virtual void EnemyHealth()
    {
        HealthBarSlider.value = ReturnHealth(); //Use the returned float to display it on the health bar

        //If enemy's current health is smaller than max health
        if (CurrentHealth < MaxHealth)
        {
            //Display health bar
            HealthBarUI.SetActive(true);
        }

        //If enemy's current health is greater than max health
        if (CurrentHealth >= MaxHealth)
        {
            //Hide the health bar and set current health as max health
            HealthBarUI.SetActive(false);
            CurrentHealth = MaxHealth;
        }

        //If enemy's current health is below or equla to 0 then 
        if(CurrentHealth <= 0)
        {
            //Decrement enemies on the map for the wave system and destroy the game object
            WaveSystem.EnemiesOnMap--;
            WaveSystem.GeneratorTemperature -= 20;
            Destroy(gameObject);
        }
    }

    //This function will damage the enemy, which uses damage for the damage that will deal to the enemy and the projectile type to get reference of what elemental projectile will do to the enemy
    public virtual void EnemyDamaged(float damage, int projectileType)
    {
        //if it's a normal projectile then deal normal damage
        if(projectileType == 0)
        {
            CurrentHealth -= damage;
        }
    }

    //This function will handle the behaviour of each enemy, this will be overriden, to match elemental enemy's behaviour
    protected virtual void EnemyBehaviour()
    {
        m_enemyNavMesh.speed = m_enemySpeed; //Set speed for the Navmesh agent. Using the speed property to set the navmesh will be simpler, instead of scrolling through a load of navmesh agent properties
        Vector3 targetPosition = Target.position; //Set the target position to the position of the player

        //If the player is not in range, then set the destination of the player's location
        if(isPlayerInRange == false)
        {
            m_enemyNavMesh.SetDestination(targetPosition);
        }
    }

    //This function will handle the burning of the enemy. It's private as it will be used for all enemies as it is controlled by the boolean
    private void EnemyBurning()
    {
        //If the enemy is burning
        if(m_isEnemyBurning == true)
        {
            //Take away enemy's health and initiate burning timer
            CurrentHealth -= BurningDamage * Time.deltaTime;
            m_enemyBurningTimer += Time.deltaTime;

            //The timer reaches the time of burning
            if(m_enemyBurningTimer > BurningTime)
            {
                //stop the burning and set the timer back to 0
                m_isEnemyBurning = false;
                m_enemyBurningTimer = 0;
            }
        }
    }

    //This function will handle the freezing of the enemy. This function will be used on all enemies
    private void EnemyFreeze()
    {
        //if the enemy is frozen
        if (m_isEnemyFrozen == true)
        {
            //Set the enemy speed to 0 and start the timer
            m_enemySpeed = 0;
            m_enemyFreezeTimer += Time.deltaTime;

            //Check if the timer is up
            if(m_enemyFreezeTimer > FreezeTime)
            {
                //Turn off freezing and set speed back to normal
                m_isEnemyFrozen = false;
                m_enemyFreezeTimer = 0;
                m_enemySpeed = MaxEnemySpeed;
            }
        }
    }
}
