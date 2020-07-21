﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the waves throughout the game.
/// It cotnrols beginning state, where enemies start to spawn. The completion of the wave to initate the next wave or fail the wave, and if the intermission phase of the game
/// Worked by: Nikodem Hamrol
/// References: 
/// </summary>

public class WaveSystem : MonoBehaviour
{   
    //Wave system fields
    //This boolean feild will be used to check if a wave has begun
    private bool m_hasWaveBegun;
    private bool m_waveInProgress;
    
    //Float fields
    private float m_timeToSpawn; //This will time the spawining of the next enemy
    private float m_nextwaveTimer; //This will time when the next wave will begin
    private float m_generatorOverheatTimer;

    //This integer field counts how many enemies have been spawned
    private int EnemiesSpawned;

    //Static fields
    public static bool IsWaveSystemInitiated;
    public static bool InIntermission; //This static boolean field will be used to check if the player is still in the intermission phase
    public static bool IsGeneratorOverheating;
    public static float WaveTimer; //This static float field is to count down the time of the wave and to be used to show in the player's HUD
    public static float GeneratorTemperature;
    public static int WaveNumber;
    public static int EnemiesOnMap; //This counts the amount of enemies currently on the map, which will be used to limit the amount of enemies on the map
    public static int WaveNumberIndex; //This is the index for the waves array, which were all the data is stored

    //Wave system properties
    [Header("Waves")]
    public WavesData[] waves; //This array takes all of the data that a wave has, which will be used to define what each wave is
    [Space(10)]

    //Float properties
    public float SpawnRate; //This is the spawn rate at which the enmies will spawn in seconds
    public float WaveCooldown; //This is the time between waves
    public float GeneratorOverheatTime;

    //This integer property is the max enemies that can be on the map
    public int MaxEnemiesOnMap; 
    

    [Header("Spawn Points")]
    public GameObject[] SpawnPoints; //This an array of all spawnn points on the map
    [Space(10)]

    [Header("Enemy Types")]
    public GameObject EnemyToSpawn; //This game object is the enemy that will be spawned on the map

    //This is a list of all elemental enemy prefabs that will be used to spawn onto the map. It was done this way, so that there is a reference to each enemy
    public GameObject WindElementalEnemy;
    public GameObject FireElementalEnemy;
    public GameObject EarthElementalEnemy;


    // Start is called before the first frame update
    void Start()
    {
        IsWaveSystemInitiated = false;
        InIntermission = false;
        IsGeneratorOverheating = false;
        GeneratorTemperature = 0;
        EnemiesOnMap = 0;
        WaveNumberIndex = 0;


        m_hasWaveBegun = false;

        //Go through each child of the WaveSystem object to get all spawn points
        for(int i = 0; i< SpawnPoints.Length; i++)
        {
            SpawnPoints[i] = transform.GetChild(i).gameObject;
        }

        WaveNumberIndex = 0;
        WaveTimer = waves[0].WaveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWaveSystemInitiated == true || m_hasWaveBegun == true && InIntermission == false)
        {
            //Initiate the first wave by setting wave begun to true, the wave index to first wave, which is 0 on the array element and and timer to use the time of the first wave
            m_hasWaveBegun = true;
            WaveNumber = waves[WaveNumberIndex].WaveNumber;

            BeginWave();
            WaveFinished();
            GeneratorState();
        }
    }

    //This function handles the spawning of an enemy
    private void SpawnEnemy()
    {
        //This int is a random value of spawn points, that will be used to spawn the enemy at that point
        int SpawnPointID = Random.Range(0, SpawnPoints.Length);       

        //Check if the amount of enemies spawned is a remainder of rate to spawn a mini boss is equal to the value to spawn a mini boss. This will spawn a miniboss enemy, which will be earth elemental
        if((EnemiesSpawned % waves[WaveNumberIndex].RateToSpawnMiniBoss) + 1 == waves[WaveNumberIndex].RateToSpawnMiniBoss)
        {
            EnemyToSpawn = EarthElementalEnemy; 
        }
        //Check if the amount of enemies spawned is a remainder of rate to spawn a shooter is equal to the value to spawn a shooter. This will spawn a shooter enemy, which will be fire elemental
        else if ((EnemiesSpawned % waves[WaveNumberIndex].RateToSpwanShooter) + 1 == waves[WaveNumberIndex].RateToSpwanShooter)
        {
            EnemyToSpawn = FireElementalEnemy;
        }
        //Else spawn the runner enemy, which is the wind elemental
        else
        {
            EnemyToSpawn = WindElementalEnemy;
        }

        //Check if it time to spawn the next enemy 
        if (Time.time >= m_timeToSpawn)
        {   
            //First increment enemies on map by 1 and instantiate the enemy to the spawn point's position and rotation
            EnemiesOnMap++;
            Instantiate(EnemyToSpawn, SpawnPoints[SpawnPointID].transform.position, SpawnPoints[SpawnPointID].transform.rotation);

            //Set the spawn rate that the enemies will spawn in and increment enemies spawned
            m_timeToSpawn = Time.time + 1 / SpawnRate;
            EnemiesSpawned++;
            GeneratorTemperature += 20;
        }            
    }

    //This function handles the beginning of each wave
    private void BeginWave()
    {
        //if the wave has begun
        if (m_hasWaveBegun == true && InIntermission == false)
        {
            //Start the timer
            WaveTimer -= Time.deltaTime;

            //Check if all enemies have been spawnd and check if there's enough enemies on the map
            if (EnemiesSpawned < waves[WaveNumberIndex].MaxEnemiesInWave && EnemiesOnMap != MaxEnemiesOnMap)
            {
                //If all that is true then spawn enemies
                SpawnEnemy();
            }
        }
    }

    //This function handles the states of how the wave will finish
    private void WaveFinished()
    {
        //Wave completed state
        //if all enemies have been spawned and there are no enemies on the map, then 
        if(EnemiesSpawned == waves[WaveNumberIndex].MaxEnemiesInWave && EnemiesOnMap == 0)
        {
            //Wave has eneded and start the wave cooledown timer
            m_hasWaveBegun = false;
            m_nextwaveTimer += Time.deltaTime;

            if (waves[WaveNumberIndex].NextWaveIsIntermission == true && IsWaveSystemInitiated == true && m_hasWaveBegun == false)
            {
                InIntermission = true;
                Intermission();
            }

            //If the cooldown is finished if the player is not in an intermission phase
            if (m_nextwaveTimer > WaveCooldown && InIntermission == false)
            {
                //Set the cooldown and enemies spawned to 0
                m_nextwaveTimer = 0;
                EnemiesSpawned = 0;

                //Increment the wave index, set the timer to the next wave's time and start the wave
                WaveNumberIndex++;
                WaveTimer = waves[WaveNumberIndex].WaveTime;
                m_hasWaveBegun = true;
                InteractAlters.HasSigilInteracted = false;
            }
        }

        //Wave failed state (Ran out of time)
        //If time runs out
        if(WaveTimer <= 0)
        {
            //End the wave
            m_hasWaveBegun = false;
            Debug.Log("Failed");
        }
    }

    //This function handles if intermission is active
    private void Intermission()
    {
        Debug.Log(InIntermission + " " + m_hasWaveBegun);
        if (InteractAlters.HasSigilInteracted == true && InIntermission == true)
        {
            InIntermission = false;
            InteractAlters.HasSigilInteracted = false;
        }
    }

    //This function handles the generator state, which mainly checks if it's overheating
    private void GeneratorState()
    {
        if(IsGeneratorOverheating == true)
        {
            m_generatorOverheatTimer += Time.deltaTime;

            if(m_generatorOverheatTimer > GeneratorOverheatTime)
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            m_generatorOverheatTimer = 0;
        }
    }

    private void GameEndState()
    {
        if(InteractAlters.HasAllSigilsActivated == true)
        {

        }
    }
}