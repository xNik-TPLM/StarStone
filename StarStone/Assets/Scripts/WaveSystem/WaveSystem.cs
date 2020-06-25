using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    
    [System.Serializable]
    
    public class Wave
    {
        public int WaveNumber;
        public int MaxEnemiesInWave;
        public int RateToSpwanShooter;
        public float WaveTime;
    }

    [Header("Waves")]
    public Wave[] waves;
    [Space(10)]
    

    private int waveNumberIndex;
    private int EnemiesSpawned = 0;

    private bool hasWavesBegun;

    private float timeToSpawn = 0;

    public float SpawnRate;
    //public int WaveNumber;
    private int EnemiesKilled;
    private int CurrentEnemiesKilled;

    public static int EnemiesOnMap = 0;
    //public int MaxEnemiesInWave;
    public int MaxEnemiesOnMap;
    
    //public int RateToSpawnShooter;
    [Space(10)]

    public GameObject[] SpawnPoints;
    [Space(10)]

    public GameObject EnemyToSpawn;

    [Header("Enemy Types")]
    public GameObject WindElementalEnemy;
    public GameObject FireElementalEnemy;


    // Start is called before the first frame update
    void Start()
    {
        //SpawnPoints = new Transform[3];
        for(int i = 0; i< SpawnPoints.Length; i++)
        {
            SpawnPoints[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hasWavesBegun = true;
            waveNumberIndex = 0;
        }

        BeginWave();
    }

    private void SpawnEnemy()
    {
        int SpawnPointID = Random.Range(0, SpawnPoints.Length);

        EnemyToSpawn = WindElementalEnemy;

        if ((EnemiesKilled % waves[waveNumberIndex].RateToSpwanShooter) == 0 && EnemiesKilled != CurrentEnemiesKilled)
        {
            EnemyToSpawn = FireElementalEnemy;
        }

        if (Time.time >= timeToSpawn)
        {   
            EnemiesOnMap++;
            //Debug.Log("Spawn");
            Instantiate(EnemyToSpawn, SpawnPoints[SpawnPointID].transform.position, SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.rotation);
            timeToSpawn = Time.time + 1 / SpawnRate;
            EnemiesSpawned++;
        }            

    }

    private void BeginWave()
    {
        if (hasWavesBegun)
        {
            if (EnemiesSpawned < waves[waveNumberIndex].MaxEnemiesInWave && EnemiesOnMap != MaxEnemiesOnMap)
            {
                SpawnEnemy();
                Debug.Log(EnemiesOnMap + " " + EnemiesSpawned);
            }
        }
    }

    /*private void WaveFinished()
    {
        if(EnemiesSpawned == waves[waveNumberIndex].MaxEnemiesInWave)
        {
            CanSpawnEnemies = false;
        }

        if(EnemiesKilled == waves[waveNumberIndex].MaxEnemiesInWave)
        {
            waveNumberIndex++;
        }
    }*/

    /*private void NextWave()
    {

    }

    private void Intermission()
    {

    }*/
}
