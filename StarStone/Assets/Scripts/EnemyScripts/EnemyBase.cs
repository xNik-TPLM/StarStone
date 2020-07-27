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
    //Enemy fields
    //Float fields
    private float m_enemyBurningTime; //This float will be counting up the time of burning
    private float m_enemyFreezeTime; //This float will be counting up the time of freezing
    private float m_enemySpeed; //This is the current speed the enemy will go.

    //Protected fields
    //protected booleans
    protected bool m_isPlayerInRange; //This will check if the enemy is in range with the player for detonation, or shooting
    protected bool m_isEnemyBurning; //This will be activated when the an enemy is hit with a fire projectile and will be used to set 
    protected bool m_isEnemyFrozen;

    //This protected float keeps track of how much health does the enemy have
    protected float m_enemyCurrentHealth;

    //This transform will get the location of the player so that the AI will know what their target is
    protected Transform m_playerTarget;

    //This navmesh agent will be used as reference to the component in our enemy object
    protected NavMeshAgent m_enemyNavMesh;

    //The player controller reference will be used to give health to the player when the enemy is hit by an earth projectile
    protected PlayerController m_playerReference;

    //Enemy properties    
    //Float properties
    [Header("Health Properties")]
    [Tooltip("The maximum health the enemy will have")]
    public float MaxHealth; //This sets the max health of an enemy

    [Header("Speed Properties")]
    [Tooltip("The maximum speed the enemy will move around the map")]
    public float MaxEnemySpeed; //This sets the max speed of our enemy

    [Header("UI")]
    public GameObject HealthBarUI; //This is the reference to the canvas of our health bar so that we can deactivate it when health is full
    public Slider HealthBarSlider; //this is the reference to the slider, which change the value of the slider

    [Header("Elemental Effects")]
    [Tooltip("This is the maximum time the enemy will burn for")]
    public float MaxBurningTime; //This sets the burning time of our enemy
    [Tooltip("This the amount of damage that burning will deal per second")]
    public int BurningDamage; //This sets the burning damage per second of our enemy
    [Tooltip("This is the maximum time for enemy being frozen")]
    public float MaxFreezeTime; //This sets the freeze time of our enemy
    [Tooltip("The amount of health given to the player when hit by the earth projectile")]
    public float HealthToPlayer; //This the amount of health the enemy will give to the player when hit by an earth projectile
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Get a reference to the navmesh agent component, player controller, and player's position
        m_enemyNavMesh = GetComponent<NavMeshAgent>(); 
        m_playerReference = FindObjectOfType<PlayerController>();
        m_playerTarget = GameObject.FindGameObjectWithTag("Player").transform; //Get the transform of our player to be used at destination for the AI

        //Set the enemy speed and health using the set properties
        m_enemySpeed = MaxEnemySpeed;
        m_enemyCurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHealth();
        EnemyBehaviour();
        CheckingElementalDamage();
        //EnemyBurning();
        //EnemyFreeze();
    }

    //This function returns the health to display it on the enemy's health bar
    private float ReturnHealth()
    {
        return m_enemyCurrentHealth / MaxHealth;
    }

    //This function will handle everything to do with health and display it
    protected virtual void EnemyHealth()
    {
        HealthBarSlider.value = ReturnHealth(); //Use the returned float to display it on the health bar

        //If enemy's current health is smaller than max health
        if (m_enemyCurrentHealth < MaxHealth)
        {
            //Display health bar
            HealthBarUI.SetActive(true);
        }

        //If enemy's current health is greater than max health
        if (m_enemyCurrentHealth >= MaxHealth)
        {
            //Hide the health bar and set current health as max health
            HealthBarUI.SetActive(false);
            m_enemyCurrentHealth = MaxHealth;
        }

        //If enemy's current health is below or equla to 0 then 
        if (m_enemyCurrentHealth <= 0)
        {
            //Decrement enemies on the map for the wave system, reduce the generator temperature and destroy the game object
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
            m_enemyCurrentHealth -= damage;
        }
    }

    //This function will handle the behaviour of each enemy, this will be overriden, to match elemental enemy's behaviour
    protected virtual void EnemyBehaviour()
    {
        m_enemyNavMesh.speed = m_enemySpeed; //Set speed for the Navmesh agent. Using the speed property to set the navmesh will be simpler, instead of scrolling through a load of navmesh agent properties
        Vector3 m_playerTargetPosition = m_playerTarget.position; //Set the target position to the position of the player

        //If the player is not in range, then set the destination of the player's location
        if(m_isPlayerInRange == false)
        {
            m_enemyNavMesh.SetDestination(m_playerTargetPosition);
        }
    }

    private void CheckingElementalDamage()
    {
        if (m_isEnemyBurning == true)
        {
            StartCoroutine(EnemyBurning());
        }

        if(m_isEnemyFrozen == true)
        {
            StartCoroutine(EnemyFreezing());
        }
    }

    private IEnumerator EnemyBurning()
    {
        Debug.Log("enemy burning");
        m_enemyCurrentHealth -= BurningDamage * Time.deltaTime;
        yield return new WaitForSeconds(MaxBurningTime);
        m_isEnemyBurning = false;
    }

    private IEnumerator EnemyFreezing()
    {
        m_enemySpeed = 0;
        yield return new WaitForSeconds(MaxFreezeTime);
        m_isEnemyFrozen = false;
    }

    //This function will handle the burning of the enemy. It's private as it will be used for all enemies as it is controlled by the boolean
    /*private void EnemyBurning()
    {
        //If the enemy is burning
        /*if(m_isEnemyBurning == true)
        {
            //Take away enemy's health and initiate burning timer
            m_enemyCurrentHealth -= BurningDamage * Time.deltaTime;
            m_enemyBurningTime += Time.deltaTime;

            //The timer reaches the time of burning
            if(m_enemyBurningTime > MaxBurningTime)
            {
                //stop the burning and set the timer back to 0
                m_isEnemyBurning = false;
                m_enemyBurningTime = 0;
            }
        }
    }*/

    //This function will handle the freezing of the enemy. This function will be used on all enemies
    /*private void EnemyFreeze()
    {
        //if the enemy is frozen
        if (m_isEnemyFrozen == true)
        {
            //Set the enemy speed to 0 and start the timer
            m_enemySpeed = 0;
            m_enemyFreezeTime += Time.deltaTime;

            //Check if the timer is up
            if(m_enemyFreezeTime > MaxFreezeTime)
            {
                //Turn off freezing and set speed back to normal
                m_isEnemyFrozen = false;
                m_enemyFreezeTime = 0;
                m_enemySpeed = MaxEnemySpeed;
            }
        }
    }*/
}
