using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is only to keep data about a wave, which will be in an array to define each wave
/// Worked By: Nikodem Hamrol
/// </summary>

[System.Serializable]
public class WavesData
{
    [Tooltip("Number indicator representing the wave")]
    public int WaveNumber; //The wave number, which will indicate the number to the player in the HUD
    [Tooltip("After the wave ends, will this wave activate the intermission")]
    public bool IsIntermissionWave; //This will be used to activate the intermission phase before the next wave begins
    [Tooltip("The maximum amount of enemies to spawn enemies in the wave")]
    public int MaxEnemiesInWave; //This will check if all enemies spawned matches the max amount enemies needed to be spawned in a wave
    [Tooltip("The rate which will spawn the shooter enemy")]
    public int RateToSpwanShooter; //This is the rate at which a shooter will be spawned
    [Tooltip("The rate which will spawn the mini boss")]
    public int RateToSpawnMiniBoss; //This is the rate at which a mini boss will be spawned
    [Tooltip("The maximum time that the wave must be completed")]
    public float WaveTime; //This is the time of a wave to be completed
}
