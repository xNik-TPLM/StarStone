using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is how the player will interact with the Starstones that surround the generator in order to the change to different elemental projectiles for the prototype weapon.
/// Worked By: Nikodem Hamrol
/// </summary>

public class InteractStarStone : MonoBehaviour
{
    //Starstone fields
    //Boolena fields
    private bool m_enableCharging; //This bool will enable the charging of the Starstones
    private bool m_isFullyCharged; //This bool will check if the starstone is fully charged, which will allow the player to use the projectiles for the prototype weapons

    //This float is the time of the Starstone charge
    private float m_starstoneChargeTime;

    //This integer is the index that charge colour will change, based on the element the Starstone represents, which uses the elements of the array
    private int m_chargeColourIndex;

    //Static fields
    public static int StarStoneID; //This integer represents the element of the Starstone, which will give the correct projectile to the prototype weapon
    public static int WeaponID; //This integer represents the weapon in the weapon holder

    //Starstone properties
    [Header("Charging Properties")]
    [Tooltip("The mmaximum charge time needed to charge the Starstone")]
    public float MaxChargeTime; //The max time needed to charge each star stone
    [Tooltip("The speed that the Starstone will charge at")]
    public float ChargeRate; //The amount of charge per second

    [Header("Ammo Properties")]
    [Tooltip("The amount ammo to give the prototype weapon")]
    public int AmmoToGive; //The amount of ammo the starstone will give to the prototype weapon

    [Header("UI Properties")]
    [Tooltip("Colours in RGB that represent the each starstone")]
    public Vector3[] ChargeColours; //This array will hold different colours that will represent each starstone
    [Tooltip("This is the slider that is attached to the Starstone")]
    public Slider ChargingSlider; //This slider will be used to change the value based on the charge time
    [Tooltip("This is the image fill of the slider")]
    public Image ChargeFill; //This image is the filler of the slider that will be mainly used to change colour.

    // Start is called before the first frame update
    void Start()
    {
        //Set the weapon ID and Starstone ID to 0, to start with the primary weapon first
        WeaponID = 0;
        StarStoneID = 0;

        //Set the slider's max vlaue to max Starstone charge and make it available at the start of the game
        ChargingSlider.maxValue = MaxChargeTime;
        m_starstoneChargeTime = MaxChargeTime;
        m_isFullyCharged = true;

        //Set the colour based on the name of the starstone
        SetChargeColours();
    }

    // Update is called once per frame
    void Update()
    {
        ChargingStarStone();
    }

    //When the player enters the box trigger of the starstone, it wlll start the wave system and give the representative elemental projectile from the Starstone they interacted
    private void OnTriggerStay(Collider trigger)
    {
        //Check the tag for the player
        if (trigger.CompareTag("Player"))
        {
            //When the player interacts with it and if the Starstone fully charged
            if (Input.GetButton("Interact") && m_isFullyCharged == true)
            {
                //Initiate the waves
                if(WaveSystem.IsWaveSystemInitiated == false)
                {
                    WaveSystem.IsWaveSystemInitiated = true;
                }

                //Based on the name of the Starstone elemental, set the Starstone ID to use that elemental projectile
                switch (gameObject.name)
                {
                    case "FireStarStone":
                        StarStoneID = 1;
                        break;

                    case "IceStarStone":
                        StarStoneID = 2;
                        break;

                    case "WindStarStone":
                        StarStoneID = 3;
                        break;

                    case "EarthStarStone":
                        StarStoneID = 4;
                        break;
                }

                //Set the weapon Id to the prototype weapon to display that
                WeaponID = 2;

                //eanble charging as teh starstone is no longer fully charged adn charge time to 0
                m_isFullyCharged = false;
                m_enableCharging = true;
                m_starstoneChargeTime = 0;

                //Set the weapon to the prototype weapon and give ammo to the prototype weapon
                FindObjectOfType<WeaponsSelect>().SetWeapon();
                FindObjectOfType<PrototypeWeapon>().CurrentAmmo = AmmoToGive;
            }
        }
    }

    //This function handles the charging of the Starstone
    private void ChargingStarStone()
    {
        //Set the value of the slider using the charge time
        ChargingSlider.value = m_starstoneChargeTime;

        //If the starstone has to charge
        if(m_enableCharging == true)
        {
            //Start counting up the charge time with the charge rate
            m_starstoneChargeTime += Time.deltaTime * ChargeRate;

            //if the Starstone is fully charged
            if(m_starstoneChargeTime > MaxChargeTime)
            {
                //Set the charge time to max charge time and set the boolean that the Starstone is fully charged, so player can take more ammo
                m_starstoneChargeTime = MaxChargeTime;
                m_isFullyCharged = true;
            }
        }
    }

    //This function will set the colours representing each Starstone based on their names
    private void SetChargeColours()
    {
        //Based on the Starstone elemental name, set the index to use that colour element in the array
        switch (gameObject.name)
        {
            case "FireStarStone":
                m_chargeColourIndex = 0;
                break;

            case "IceStarStone":
                m_chargeColourIndex = 1;
                break;

            case "WindStarStone":
                m_chargeColourIndex = 2;
                break;

            case "EarthStarStone":
                m_chargeColourIndex = 3;
                break;
        }

        //Set the colour using the values of the vector 3 array
        ChargeFill.color = new Color(ChargeColours[m_chargeColourIndex].x, ChargeColours[m_chargeColourIndex].y, ChargeColours[m_chargeColourIndex].z);
    }
}
