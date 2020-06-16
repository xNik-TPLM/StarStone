using System.Collections;
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
    //Enemy feilds
    private NavMeshAgent m_enemyNavMesh;


    //Enemy properties    
    //Float properties
    public float CurrentHealth; //This keeps track of how much health does the enemy have
    public float MaxHealth; //This sets the max health of an enemy
    public float EnemySpeed; //This sets the speed of our enemy

    public GameObject HealthBarUI;
    public Slider HealthBarSlider;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        m_enemyNavMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Constanlty display enemies health
        EnemyHealth();
        EnemyMovement();
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
    }

    //This function will damage the enemy, which will be called in the projectile script as projectile damage is needed
    public void EnemyDamaged(float projectileDamage)
    {
        CurrentHealth -= projectileDamage;
    }

    private void EnemyMovement()
    {
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, Target.position, m_enemyNavMesh.speed * Time.deltaTime);
        m_enemyNavMesh.SetDestination(targetPosition);
    }
}
