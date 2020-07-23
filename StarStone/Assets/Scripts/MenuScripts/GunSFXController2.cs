using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the volume slider in the options menu
///// Worked By: Ben Smith
///// </summary>
public class GunSFXController2 : MonoBehaviour
{
    public AudioSource Reload; // This sets the reference to the audio
    private float reloadVolume = 0.8f; // This sets a starting volume

    // Start is called before the first frame update
    void Start()
    {
        Reload = GetComponent<AudioSource>(); // This allows the theme to be access in the game
    }

    // Update is called once per frame
    void Update()
    {
        Reload.volume = reloadVolume;
    }

    public void SetVolume(float reloadvol)
    {
        reloadVolume = reloadvol; // This links the slider value to the music volume
    }
}
