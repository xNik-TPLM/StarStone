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
    public int WaveNumber; //The wave number, which will indicate the number to the player in the HUD
    public bool NextWaveIsIntermission; //This will be used to activate the intermission phase before the next wave begins
    public int MaxEnemiesInWave; //This will check if all enemies spawned matches the max amount enemies needed to be spawned in a wave
    public int RateToSpwanShooter; //This is the rate at which a shooter will be spawned
    public float WaveTime; //This is the time of a wave to be completed
}
