using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the volume slider in the options menu
///// Worked By: Ben Smith
///// </summary>
public class PrototypeSFXController : MonoBehaviour
{
    public AudioSource Explosion; // This sets the reference to the audio
    private float explosionVolume = 0.8f; // This sets a starting volume

    // Start is called before the first frame update
    void Start()
    {
        Explosion = GetComponent<AudioSource>(); // This allows the theme to be access in the game
    }

    // Update is called once per frame
    void Update()
    {
        Explosion.volume = explosionVolume;
    }

    public void SetVolume(float explosionvol)
    {
        explosionVolume = explosionvol; // This links the slider value to the music volume
    }
}
