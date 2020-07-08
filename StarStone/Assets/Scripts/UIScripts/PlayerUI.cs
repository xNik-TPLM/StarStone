using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This script handles the player's defensive ability activating a shield to give the player extra health
/// Worked By: Ben Smith & Nikodem Hamrol
/// </summary>
public class PlayerUI : MonoBehaviour
{
    //Ben Smith's fields and properties
    public Text ammoDisplay; // This displays the current ammo left in the clip
    public Text maxAmmo; // This displays the maximum ammo that is left that the player has
    private WeaponBase m_weapon;
    private PlayerController m_playerHealth;

    private bool m_timerEnabled;
    private float m_timer;
    public float Timer;

    public static bool shieldActive; // This checks if the shield is currently enabled
    public Slider healthSlider; // This sets a reference for the health bar
    public Slider shieldSlider; // This sets a reference for the shield bar
    [Space(20)]


    //Nikodem Hamrol's fields and properties
    private int m_sliderColourIndex;

    [Header("Slider Temperature Properties")]
    public float MaxGeneratorTemperature;
    public int TemperatureStatesAmount;
    public Vector3[] SliderColours;

    [Header("UI Components")]
    public Text WaveStateText;
    public Text TimerText;
    public Text OverheatingText;
    public Slider GeneratorTemperatureSlider;
    public Image TemperatureFill;


    // Start is called before the first frame update
    void Start()
    {
        //Ben Smith
        gameObject.SetActive(true); // This enables the Player UI
        m_weapon = FindObjectOfType<WeaponBase>();
        m_playerHealth = FindObjectOfType<PlayerController>();
        m_timer = Timer;


        //Nikodem Hamrol
        //At Start, set the values for the slider's and disable the overheating text
        GeneratorTemperatureSlider.maxValue = MaxGeneratorTemperature;
        //GeneratorTemperatureSlider.value = WaveSystem.GeneratorTemperature;
        OverheatingText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Ben Smith
        healthSlider.value = m_playerHealth.currentHealth/100;
        StopwatchCooldown();
        TimerCooldown();

        if (Input.GetKeyDown(KeyCode.L))
        {
            shieldActive = true;
        }
        if (shieldActive == true)
        {

            gameObject.SetActive(true);
        }

        // Once the player takes damage, they will lose health depending on whether the shield has been enabled or not
        if (Input.GetKeyDown(KeyCode.P) && shieldActive == true)
        {
                shieldSlider.value -= 0.1f; // This lowers the shield value once damage has been taken
            if (shieldSlider.value <= 0)
            {
                m_playerHealth.currentHealth -= 10;
            }
        }

        ammoDisplay.text = m_weapon.CurrentAmmo.ToString(); // This displays the current ammo left in the clip as a part of the player's HUD
        maxAmmo.text = m_weapon.MaxAmmo.ToString(); // This displays the maximum ammo left in the weapon as a part of the player's HUD

        //Nikodem Hamrol
        UpdateWaveNumber();
        UpdateTimer();
        UpdateGeneratorSlider();
    }
    private void StopwatchCooldown()
    {
        //If something you want is eneabled
        if (m_timerEnabled == true)
        {
            m_timer += Time.deltaTime;

            if (m_timer > Timer)
            {
                m_timerEnabled = false;
                m_timer = 0;
                //Do whatever
            }
        }
    }
    private void TimerCooldown()
    {
        //If something you want is eneabled
        if (m_timerEnabled == true)
        {
            
            m_timer -= Time.deltaTime;

            if (m_timer <= 0)
            {
                m_timerEnabled = false;
                m_timer = Timer;
                //Do whatever
            }
        }
    }

    private void UpdateWaveNumber()
    {
        WaveStateText.text = "Wave: " + WaveSystem.WaveNumber.ToString();
    }

    //This function will handle the time of the waves to display as text (Nikodem Hamrol)
    private void UpdateTimer()
    {
        //Convert the time from an integer to string
        string timerSeconds = ((int)WaveSystem.WaveTimer % 60).ToString("00");
        string timerMinutes = ((int)WaveSystem.WaveTimer / 60).ToString("00");

        //Set timer text
        TimerText.text = timerMinutes + ":" + timerSeconds;
    }

    //this function will handle the temperature of the generator display
    private void UpdateGeneratorSlider()
    {
        GeneratorTemperatureSlider.value = WaveSystem.GeneratorTemperature;

        //Switch colours using the colour index
        switch (m_sliderColourIndex)
        {
            case 0: //Green
                //Check if the temperature is between 100 and 200
                if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, (MaxGeneratorTemperature / TemperatureStatesAmount), ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2)))
                {
                    m_sliderColourIndex = 1; //Set colour to yellow
                }
                break;

            case 1: //Yellow
                //Check if the temperature is between 200 and 299, this avoids flashing of colours when it reaches max temperature
                if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2), MaxGeneratorTemperature - 1))
                {
                    m_sliderColourIndex = 2; //Set colour to Orange
                }
                //Check if the temperature is less than 100
                else if (WaveSystem.GeneratorTemperature < (MaxGeneratorTemperature / TemperatureStatesAmount))
                {
                    m_sliderColourIndex = 0; //Set colour to Green
                }
                break;

            case 2: //Orange
                //Check if the temperature is max, which is 300
                if (WaveSystem.GeneratorTemperature == MaxGeneratorTemperature)
                {
                    m_sliderColourIndex = 3; //Set colour to Red
                }
                //Check if the temperature is between 100 and 200
                else if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, (MaxGeneratorTemperature / TemperatureStatesAmount), ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2)))
                {
                    m_sliderColourIndex = 1; //Set colour to Yellow
                }
                break;

            case 3: //Red, generator is overheating
                //The generator will now start to overheat and show the overheating text
                WaveSystem.IsGeneratorOverheating = true;
                OverheatingText.enabled = true;

                //Check if the temperature is between 200 and 299
                if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2), MaxGeneratorTemperature - 1))
                {   
                    //Set colour to Orange and disable overheating and hide the text
                    m_sliderColourIndex = 2; 
                    WaveSystem.IsGeneratorOverheating = false;
                    OverheatingText.enabled = false;
                }
                break;
        }

        //Update the colour everytime using the colour index
        TemperatureFill.color = new Color(SliderColours[m_sliderColourIndex].x, SliderColours[m_sliderColourIndex].y, SliderColours[m_sliderColourIndex].z);
    }
}