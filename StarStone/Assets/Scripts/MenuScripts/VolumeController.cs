using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the volume slider in the options menu
///// Worked By: Ben Smith
///// </summary>
public class VolumeController : MonoBehaviour
{
    [Header("Volume Properties")]
    public AudioSource Theme; // This sets the reference to the audio
    private float musicVolume = 0.5f; // This sets a starting volume

    // Start is called before the first frame update
    void Start()
    {
        Theme = GetComponent<AudioSource>(); // This allows the theme to be access in the game
    }

    // Update is called once per frame
    void Update()
    {
        Theme.volume = musicVolume; 
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol; // This links the slider value to the music volume
    }
}
