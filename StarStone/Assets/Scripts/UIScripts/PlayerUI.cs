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
    [Header("Ammo Display")]
    [Tooltip("This displays the text")]
    public Text ammoDisplay; // This displays the current ammo and max ammo of a weapon

    [Header("Shield Slider And Properties")]
    public Slider shieldSlider; // This sets a reference for the shield bar
    [Tooltip("This sets a timer for the defensive ability")]
    private float m_shieldCooldownTimer;
    public float ShiedCooldownMaxTime;
    public static bool shieldCooldownActive;
    public static bool shieldActive; // This checks if the shield is currently enabled

    [Header("Health Slider And Properties")]
    [Tooltip("This sets the players health")]
    public Slider healthSlider; // This sets a reference for the health bar
    private PlayerController m_playerHealth;


    //Nikodem Hamrol's fields and properties
    //These integers are used to change as indexes for the arrays of colours
    private int m_temperatureColourIndex;
    private int m_nukeCooldownColourIndex;
    private int m_shieldCooldownColourIndex;

    //Reference to the offesive ability script to access the nuke max time
    private OffensiveAbility m_offensiveAbilityReference;

    public static string PopUpMessageText;
    public static string PopUpControlsText;
    public static bool PopUpMessageEnabled;
    public static bool PopUpControlsEnabled;


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

    [Header("UI Components")]
    public Text WaveStateText; //This text object will show the wave number and if they are in an intermission phase
    public Text WaveTimerText; //This text object will show the time left for a wave to be completed
    public Text OverheatingText; //This text object will indicate if the generator is overheating
    public Text PopUpControls; //This text object will show the controls on objects that player will interact with
    public Text PopUpMessage; //This text object will show the message, if the player can't yet interact with the object
    public float PopUpMessageMaxDisplayTime; //This is the max time that the pop up message will be displayed for
    public GameObject NukeAbilityIcon; //This is the UI for the nuke, which contains the slider and icon
    public GameObject ShieldAbilityIcon; //This is the UI for the shield, which contains the slider and icon

    // Start is called before the first frame update
    void Start()
    {
        //Ben Smith
        gameObject.SetActive(true); // This enables the Player UI
        //m_weapon = FindObjectOfType<WeaponBase>();
        m_playerHealth = FindObjectOfType<PlayerController>();

        shieldSlider.maxValue = m_playerHealth.ShieldAmount;
        m_shieldCooldownTimer = ShiedCooldownMaxTime;


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

        // This function checks the defensive abilty and allows the player to use it when the cooldown has finished
        if (shieldActive == true && shieldCooldownActive == false)
        {
            m_shieldCooldownColourIndex = 1;

            if (shieldSlider.value <= 0)
            {   shieldActive = false;
                shieldCooldownActive = true;
                m_shieldCooldownTimer = 0;
            }
        }

        if (FindObjectOfType<WeaponBase>() != null)
        {
            ammoDisplay.text = FindObjectOfType<WeaponBase>().CurrentAmmo.ToString() + " / " + FindObjectOfType<WeaponBase>().MaxAmmo.ToString(); //This displays the current ammo in the clip and maximum ammo left as a part of the player's HUD
        }

        //Nikodem Hamrol
        UpdatePopUpMessages();
        UpdateGameState();
        UpdateTimer();
        UpdateNukeCooldown();
        UpdateShieldCooldown();
        UpdateGeneratorSlider();
        HUDDisplayingInTutorial();
    }

    //This coroutine is used to display pop up messages for certain amount of time (Nikodem Hamrol)
    private IEnumerator PopUpMessagesDisplayTime()
    {
        //Display the text and wait until time for displaying is up
        PopUpMessage.text = PopUpMessageText;
        PopUpMessage.enabled = true;
        yield return new WaitForSeconds(PopUpMessageMaxDisplayTime);

        //Then hide the text
        PopUpMessageEnabled = false;
        PopUpMessage.enabled = false;
    }

    //This function is to update the HUD with displaying the controls and messages to the player (Nikodem Hamrol)
    private void UpdatePopUpMessages()
    {
        //If the controls pop up can be displayed
        if(PopUpControlsEnabled == true)
        {
            //Set the text and display it on screen
            PopUpControls.text = PopUpControlsText;
            PopUpControls.enabled = true;
        }
        else //else, hide it
        {
            PopUpControls.enabled = false;
        }

        //If pop up message can be dispalyed then, start the coroutine to display the message at given time
        if (PopUpMessageEnabled == true)
        {
            StartCoroutine(PopUpMessagesDisplayTime());
        }
    }

    //This function will handle the indication text of the game state the player is in (Nikodem Hamrol)
    private void UpdateGameState()
    {
        //Switch between game states
        switch (WaveSystem.GameStateIndex)
        {
            case 0: //Before the waves are initiated, which will also hide the timer
                WaveStateText.text = "Use one of the Starstones that surrounds the generator";
                WaveTimerText.enabled = false;
                break;

            case 1: //When waves are initiated, which will show the wave number and show the timer         
                WaveStateText.text = "Wave: " + WaveSystem.WaveNumber.ToString();
                WaveTimerText.enabled = true;
                break;

            case 2: //This is when the player is in the intermission, which the player will be tasked to activate the alters
                switch (InteractAlters.AlterActivatedIndex)
                {
                    case 1: //Activate the wind alter
                        WaveStateText.text = "Activate the Wind alter, using its sigil";
                        break;

                    case 2: //Activate the fire alter
                        WaveStateText.text = "Activate the Fire alter, using its sigil";
                        break;

                    case 3: //Activate the ice alter
                        WaveStateText.text = "Activate the Ice alter, using its sigil";
                        break;

                    case 4: //Activate the earth alter
                        WaveStateText.text = "Activate the Earth alter, using its sigil";
                        break;

                    case 5: //Activate the power switch
                        WaveStateText.text = "Turn off the generator using the power switch";
                        break;
                }
                break;
        }
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

    //This function will handle the UI displaying throughout the tutorial phase (Nikodem Hamrol)
    private void HUDDisplayingInTutorial()
    {
        //If the player is int he tutorial scene
        if (TutorialController.InTutorialScene == true)
        {
            //Move the temperature bar out of the screen and disable the wave state text and wave time text
            GeneratorTemperatureSlider.transform.position = new Vector3(-100,0,0);
            WaveStateText.enabled = false;
            WaveTimerText.enabled = false;

            //Switch between the dialogue in the tutorial
            switch (TutorialController.CurrentDialogue)
            {
                //On the first dialogue, disable the ammo, nuke and shield UI
                case "Morning soldier. Welcome to this training facility, where we will test your capabilities with your new advancements.":
                    ammoDisplay.enabled = false;
                    NukeAbilityIcon.SetActive(false);
                    ShieldAbilityIcon.SetActive(false);
                    break;

                //On the shooting tutorial, show the ammo UI
                case "Now load your weapon, either your rifle or the pistol and shoot the test dummy, or use your knife on it.":
                    ammoDisplay.enabled = true;
                    break;

                //On the nuke tutorial, show the nuke UI
                case "Go ahead and use it on this test dummy.":
                    NukeAbilityIcon.SetActive(true);
                    break;

                //On the shield tutorial, show the shield UI
                case "Can you activate it?":
                    ShieldAbilityIcon.SetActive(true);
                    break;
            }
        }
    }
}