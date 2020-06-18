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
    private bool m_isPlayerInRange = false;

    protected bool m_isEnemyBurning;

    private float m_detonationTime;
    public float DetonationTimer;

    private NavMeshAgent m_enemyNavMesh;


    //Enemy properties    
    //Float properties
    public float CurrentHealth; //This keeps track of how much health does the enemy have
    public float MaxHealth; //This sets the max health of an enemy
    public float EnemySpeed; //This sets the speed of our enemy

    public int BurnDamage;
    public float BurningTime;

    private float m_enemyBurningTimer;

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
        EnemyDetonation();
        EnemyBurning();
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
    public virtual void EnemyDamaged(float damage, int projectileType)
    {
        if(projectileType == 0)
        {
            CurrentHealth -= damage;
        }
    }

    protected virtual void EnemyMovement()
    {
        m_enemyNavMesh.speed = EnemySpeed;
        Vector3 targetPosition = Target.position;

        if(m_isPlayerInRange == false)
        {
            m_enemyNavMesh.SetDestination(targetPosition);
        }

    }

    private void EnemyDetonation()
    {
        if (m_isPlayerInRange == true)
        {
            m_detonationTime += Time.deltaTime;

            if (m_detonationTime > DetonationTimer)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isPlayerInRange = true;
        }
    }

    protected void EnemyBurning()
    {
        if(m_isEnemyBurning == true)
        {
            CurrentHealth -= BurnDamage * Time.deltaTime;
            m_enemyBurningTimer += Time.deltaTime;

            if(m_enemyBurningTimer > BurningTime)
            {
                m_isEnemyBurning = false;
                m_enemyBurningTimer = 0;
            }
        }
        Debug.Log(m_enemyBurningTimer);
    }
}
