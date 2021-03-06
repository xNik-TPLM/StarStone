﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the volume slider in the options menu
///// Worked By: Ben Smith
///// </summary>
public class GunSFXController : MonoBehaviour
{
    [Header("Gun Fire Properties")]
    public AudioSource Fire; // This sets the reference to the audio
    private float fireVolume = 0.8f; // This sets a starting volume

    // Start is called before the first frame update
    void Start()
    {
        Fire = GetComponent<AudioSource>(); // This allows the theme to be access in the game
    }

    // Update is called once per frame
    void Update()
    {
        Fire.volume = fireVolume;
    }

    public void SetVolume(float firevol)
    {
        fireVolume = firevol; // This links the slider value to the music volume
    }
}
