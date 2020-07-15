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
    public Text ammoDisplay; // This displays the current ammo and max ammo of a weapon

    //public WeaponBase m_weapon;
    private PlayerController m_playerHealth;

    public static bool shieldCooldownActive;

    private float m_shieldCooldownTimer;
    public float ShiedCooldownMaxTime;

    public static bool shieldActive; // This checks if the shield is currently enabled
    public Slider healthSlider; // This sets a reference for the health bar
    public Slider shieldSlider; // This sets a reference for the shield bar
    [Space(10)]

    //Nikodem Hamrol's fields and properties
    //These integers are used to change as indexes for the arrays of colours
    private int m_temperatureColourIndex;
    private int m_nukeCooldownColourIndex;
    private int m_shieldCooldownColourIndex;

    //Reference to the offesive ability script to access the nuke max time
    private OffensiveAbility m_offensiveAbilityReference;

    [Header("Slider Temperature Properties")]
    public float MaxGeneratorTemperature; //This is to set the max value for the slider and to check if the generator will overheat
    public int TemperatureStatesAmount;  //This is the amount of states the generatr has. This value makes it easier to calculate the middle states of the generator.
    public Vector3[] SliderColours; //This array keeps the rgb values for the states of the generator temperature.
    public Slider GeneratorTemperatureSlider; //This is used as reference to the generator slider, in order to update the value of it
    public Image TemperatureFill; //This is used as reference to the image used to fill the slider, in order to change its colour

    [Header("Ability Cooldown Properties")]
    //These the two images are used as reference to the cooldown image, in order to update the fill amount and the change the colour of the image
    public Image NukeCooldownImage;
    public Image ShieldCooldownImage;

    //This array keeps track the rgba values for the cooldown states.
    public Vector4[] AbilityReadyColour;

    [Header("UI Text Components")]
    public Text WaveStateText; //This text object will show the wave number and if they are in an intermission phase
    public Text WaveTimerText; //This text object will show the time left for a wave to be completed
    public Text OverheatingText; //This text object will indicate if the generator is overheating

    // Start is called before the first frame update
    void Start()
    {
        //Ben Smith
        gameObject.SetActive(true); // This enables the Player UI
        //m_weapon = FindObjectOfType<WeaponBase>();
        m_playerHealth = FindObjectOfType<PlayerController>();

        shieldSlider.maxValue = m_playerHealth.ShieldAmount;


        //Nikodem Hamrol
        m_offensiveAbilityReference = FindObjectOfType<OffensiveAbility>(); //Set reference of the offensive ability, which is the nuke. This will be used to get the max cooldown time
        
        //We set these bools as active so that, the cooldown can start as soon as the game starts
        OffensiveAbility.NukeEnabled = true;
        shieldCooldownActive = true;


        //At Start, set the max value for the generator slider and disable the overheating text
        GeneratorTemperatureSlider.maxValue = MaxGeneratorTemperature;
        OverheatingText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Ben Smith
        healthSlider.value = m_playerHealth.currentHealth/100;
        shieldSlider.value = PlayerController.ShieldHealth;

        if (shieldActive == true && shieldCooldownActive == false)
        {
            shieldCooldownActive = true;
            m_shieldCooldownTimer = 0;

            if (shieldSlider.value <= 0)
            {
                shieldActive = false;
            }
        }

        ammoDisplay.text = FindObjectOfType<WeaponBase>().CurrentAmmo.ToString() + " / " + FindObjectOfType<WeaponBase>().MaxAmmo.ToString(); //This displays the current ammo in the clip and maximum ammo left as a part of the player's HUD

        //Nikodem Hamrol
        UpdateWaveNumber();
        UpdateTimer();
        UpdateNukeCooldown();
        UpdateShieldCooldown();
        UpdateGeneratorSlider();
    }

    //This function will handle the indication of the wave number they're in and if they're in an intermission (Nikodem Hamrol)
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
        WaveTimerText.text = timerMinutes + ":" + timerSeconds;
    }

    //This function will handle the cooldown display of the offensive ability (Nikodem Hamrol)
    private void UpdateNukeCooldown()
    {
        //If the offensive ability is enabled
        if(OffensiveAbility.NukeEnabled == true)
        {
            m_nukeCooldownColourIndex = 1; //Set colour of the progression ring to grey
        }
        else //if it is not enabled
        {
            m_nukeCooldownColourIndex = 0; //Set colour of the progression ring to yellow
        }        
        
        NukeCooldownImage.fillAmount = OffensiveAbility.NukeCooldownTimer / m_offensiveAbilityReference.NukeCooldownMaxTime; //Update the image fill. Can't set max value of the fill amount, so we divide the cooldown timer by the max cooldown time
        NukeCooldownImage.color = new Color(AbilityReadyColour[m_nukeCooldownColourIndex].w, AbilityReadyColour[m_nukeCooldownColourIndex].x, AbilityReadyColour[m_nukeCooldownColourIndex].y, AbilityReadyColour[m_nukeCooldownColourIndex].z); //Update the colour using indexes
    }

    //This function will handle the cooldown display of the defensive ability (Nikodem Hamrol)
    private void UpdateShieldCooldown()
    {
        //If the cooldown is active and shield is deactivated
        if (shieldCooldownActive == true && shieldActive == false)
        {
            //Start counting and set the colour of the progression ring to grey
            m_shieldCooldownTimer += Time.deltaTime;
            m_shieldCooldownColourIndex = 1;

            //If shield 
            if (m_shieldCooldownTimer > ShiedCooldownMaxTime)
            {
                shieldCooldownActive = false;
                m_shieldCooldownColourIndex = 0;
            }
        }

        ShieldCooldownImage.fillAmount = m_shieldCooldownTimer / ShiedCooldownMaxTime; //Update the image fill.
        ShieldCooldownImage.color = new Color(AbilityReadyColour[m_shieldCooldownColourIndex].w, AbilityReadyColour[m_shieldCooldownColourIndex].x, AbilityReadyColour[m_shieldCooldownColourIndex].y, AbilityReadyColour[m_shieldCooldownColourIndex].z); //Update the colour using indexes
    }

    //this function will handle the temperature of the generator display (Nikodem Hamrol)
    private void UpdateGeneratorSlider()
    {
        GeneratorTemperatureSlider.value = WaveSystem.GeneratorTemperature;

        //Switch colours using the colour index
        switch (m_temperatureColourIndex)
        {
            case 0: //Green
                //Check if the temperature is between 100 and 200
                if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, (MaxGeneratorTemperature / TemperatureStatesAmount), ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2)))
                {
                    m_temperatureColourIndex = 1; //Set colour to yellow
                }
                break;

            case 1: //Yellow
                //Check if the temperature is between 200 and 299, this avoids flashing of colours when it reaches max temperature
                if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2), MaxGeneratorTemperature - 1))
                {
                    m_temperatureColourIndex = 2; //Set colour to Orange
                }
                //Check if the temperature is less than 100
                else if (WaveSystem.GeneratorTemperature < (MaxGeneratorTemperature / TemperatureStatesAmount))
                {
                    m_temperatureColourIndex = 0; //Set colour to Green
                }
                break;

            case 2: //Orange
                //Check if the temperature is max, which is 300
                if (WaveSystem.GeneratorTemperature == MaxGeneratorTemperature)
                {
                    m_temperatureColourIndex = 3; //Set colour to Red
                }
                //Check if the temperature is between 100 and 200
                else if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, (MaxGeneratorTemperature / TemperatureStatesAmount), ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2)))
                {
                    m_temperatureColourIndex = 1; //Set colour to Yellow
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
                    m_temperatureColourIndex = 2; 
                    WaveSystem.IsGeneratorOverheating = false;
                    OverheatingText.enabled = false;
                }
                break;
        }

        //Update the colour everytime using the colour index
        TemperatureFill.color = new Color(SliderColours[m_temperatureColourIndex].x, SliderColours[m_temperatureColourIndex].y, SliderColours[m_temperatureColourIndex].z);
    }
}