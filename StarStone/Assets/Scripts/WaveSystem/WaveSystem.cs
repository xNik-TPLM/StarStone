using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls the waves throughout the game.
/// It cotnrols beginning state, where enemies start to spawn. The completion of the wave to initate the next wave or fail the wave, and if the intermission phase of the game
/// Worked by: Nikodem Hamrol
/// References: Single Sapling Games. (2019). Wave System - FPS Game In Unity - Part 63 [online]. Available: https://www.youtube.com/watch?v=gtVQDqFdabs [Last Accessed 29th July 2020].
/// </summary>

public class WaveSystem : MonoBehaviour
{   
    //Wave system fields
    //This boolean feild will be used to check if a wave has begun
    private bool m_hasWaveBegun;
    
    //Float fields
    private float m_timeToSpawn; //This will time the spawining of the next enemy
    private float m_nextwaveTimer; //This will time when the next wave will begin
    private float m_generatorOverheatTimer; 

    //This integer field counts how many enemies have been spawned
    private int EnemiesSpawned;

    //Static fields
    public static bool IsWaveSystemInitiated; //This static boolean field will be used to check if the player has initated the wave system
    public static bool InIntermission; //This will be used to check if the player is still in the intermission phase
    public static bool IsGeneratorOverheating; //This will check if the generator is overheating, which will start the overheating timer and change the Ui elements to represent that
    public static bool GameCompleted; //Ths will chec if the payer has completed the game successfullyk
    public static float WaveTimer; //This static float field is to count down the time of the wave and to be used to show in the player's HUD
    public static float GeneratorTemperature; //This will be used to set the generator temperature on the slider, based on the amount of enemies on the map
    public static int WaveNumber; //This will use the wave number in from the wave data, so that it can be displayed
    public static int EnemiesOnMap; //This counts the amount of enemies currently on the map, which will be used to limit the amount of enemies on the map
    public static int WaveNumberIndex; //This is the index for the waves array, which were all the data is stored
    public static int GameStateIndex; //This is the state that the game is, which will dispaly the text to what the game state matches to
    

    //Wave system properties
    [Header("Waves")]
    [Tooltip("This array is the data for each wave, entering the size of the array, will add more waves")]
    public WavesData[] waves; //This array takes all of the data that a wave has, which will be used to define what each wave is

    [Header("Spawn Points")]
    [Tooltip("These are the spawn points on the map. Make sure you add the spawn points into this array, which must correspond to the amount of the points on the map")]
    public GameObject[] SpawnPoints; //This an array of all spawnn points on the map

    [Header("General Wave Properties")]
    [Tooltip("This is the rate that the enemies will spawn in")]
    public float SpawnRate; //This is the spawn rate at which the enmies will spawn in seconds
    [Tooltip("This is the time to initiate the next wave")]
    public float WaveCooldown; //This is the time between waves
    [Tooltip("This is the maximum time the generator can overheat, before ending the game in failure")]
    public float GeneratorOverheatTime; //Max time the generator can overheat for
    [Tooltip("This is the amount of enemies that is allowed on the map")]
    public int MaxEnemiesOnMap; //Max enemies that can be on the map
    [Tooltip("The game over screen object, when the player fails")]
    public GameObject GameOverScreen;
    
    [Header("Enemy Types")]
    [Tooltip("The enemy that will spawn. Only for debugging")]
    public GameObject EnemyToSpawn; //This game object is the enemy that will be spawned on the map

    //This is a list of all elemental enemy prefabs that will be used to spawn onto the map. It was done this way, so that there is a reference to each enemy
    [Tooltip("The wind enemy prefab")]
    public GameObject WindElementalEnemy;
    [Tooltip("The fire enemy prefab")]
    public GameObject FireElementalEnemy;
    [Tooltip("The earth enemy prefab")]
    public GameObject EarthElementalEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Reset the public static fields to false and 0
        IsWaveSystemInitiated = false;
        InIntermission = false;
        IsGeneratorOverheating = false;
        GameCompleted = false;
        GeneratorTemperature = 0;
        EnemiesOnMap = 0;
        WaveNumberIndex = 0;
        GameStateIndex = 0;
        
        //Go through each child of the WaveSystem object to get all spawn points
        for(int i = 0; i< SpawnPoints.Length; i++)
        {
            SpawnPoints[i] = transform.GetChild(i).gameObject;
        }

        //Set the wave index to 0 and use the first wave's timer at the start
        WaveNumberIndex = 0;
        WaveTimer = waves[0].WaveTime;
    }

    // Update is called once per frame
    void Update()
    {
        //These 3 function will only if the wave system is initiated or if has begun and if the not in intermission.
        //So if the player is in intermission, is will not run these functions
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
            //Set the game state index 1, which will display the wave number and timer
            GameStateIndex = 1;

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

            //Check if the wave has intermission and if the wave is initiated and the waves have not begun
            if (waves[WaveNumberIndex].IsIntermissionWave == true && IsWaveSystemInitiated == true && m_hasWaveBegun == false)
            {
                //Set that the player is in intermission, display the alter to activate as the game state is set to 2
                InIntermission = true;
                GameStateIndex = 2;
                Intermission();
            }

            //If the cooldown is finished if the player is not in an intermission phase
            if (m_nextwaveTimer > WaveCooldown && InIntermission == false && InteractAlters.HasAllSigilsActivated == false)
            {
                //Set the cooldown and enemies spawned to 0
                m_nextwaveTimer = 0;
                EnemiesSpawned = 0;
                
                //Increment the wave index, set the timer to the next wave's time and start the wave
                WaveNumberIndex++;
                WaveTimer = waves[WaveNumberIndex].WaveTime;
                m_hasWaveBegun = true;
                InteractAlters.HasSigilInteracted = false;
                HealthCrate.HealthKitUsed = false;
                AmmoCrate.HasAmmoRefilled = false;
            }
        }

        //Wave failed state (Ran out of time)
        //If time runs out in the wave
        if(WaveTimer <= 0)
        {
            //End the wave
            GameOver();
        }
    }

    //This function handles if intermission is active
    private void Intermission()
    {
        //Check if a sigil has been interacted and if the intermission is still active and if the all sigils haven't been activated yet, which will set the intermission to false and start the next wave
        if (InteractAlters.HasSigilInteracted == true && InIntermission == true && InteractAlters.HasAllSigilsActivated == false)
        {
            InIntermission = false;
        }
    }

    //This function handles the generator state, which mainly checks if it's overheating
    private void GeneratorState()
    {
        //If the generator is overheating, then start counting the time
        if (IsGeneratorOverheating == true)
        {
            m_generatorOverheatTimer += Time.deltaTime;

            //If the timer is above the overheat time, it will end the game
            if (m_generatorOverheatTimer > GeneratorOverheatTime)
            {
                GameOver();
            }
        }
        else //If the generator has stopped overheating, which iis was done by the player killing an enemy, it will set the timer back to 0 
        {
            m_generatorOverheatTimer = 0;
        }
    }

    //This function will run the game over state of the game
    public void GameOver()
    {   
        //Stop spawning enemies, show the game over screen, disable the player controls, destroy all enemies, and start the coroutine, which will load the main menu
        //The reason the freeze function isn't called, it's because it will not run the coroutine
        m_hasWaveBegun = false;
        GameOverScreen.SetActive(true);
        PlayerController.ControlsEnabled = false;
        DestroyAllEnemies();
        StartCoroutine(LoadMainMenu());
    }

    //This coroutine will run when the game over is active, which will load the main menu.
    private IEnumerator LoadMainMenu()
    {
        //Wait 5 seconds, then load the main menu
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameMenu");
    }

    //This function will destroy every enemy on the map
    public void DestroyAllEnemies()
    {
        //This loop will run up to the amount of enemies on the map
        for (int i = 0; i < EnemiesOnMap; i++)
        {
            //Get every enemy, by finding the enemy base component
            EnemyBase enemy = FindObjectOfType<EnemyBase>();

            //If the enemy still exists, then destroy it
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}