using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public AudioSource Theme;
    private float musicVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Theme = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Theme.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
