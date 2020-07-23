using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the volume slider in the options menu
///// Worked By: Ben Smith
///// </summary>
public class WalkSFXController : MonoBehaviour
{
    public AudioSource Walk; // This sets the reference to the audio
    private float sfxVolume = 0.8f; // This sets a starting volume

    // Start is called before the first frame update
    void Start()
    {
        Walk = GetComponent<AudioSource>(); // This allows the theme to be access in the game
    }

    // Update is called once per frame
    void Update()
    {
        Walk.volume = sfxVolume;
    }

    public void SetVolume(float vol)
    {
        sfxVolume = vol; // This links the slider value to the music volume
    }
}
