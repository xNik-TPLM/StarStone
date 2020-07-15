using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Worked By: Nikodem Hamrol
/// </summary>

public class InteractStarStone : MonoBehaviour
{
    private bool m_enableCharging;
    private bool m_isFullyCharged;

    private float m_chargeTimer;

    private int m_chargeColourIndex;




    public static int StarStoneID;
    public static int WeaponID;


    [Header("Charging Properties")]
    public float MaxChargeTime;
    public float ChargeRate;

    [Header("Ammo Properties")]
    public int AmmoToGive;

    [Header("UI Properties")]
    public Vector3[] ChargeColours;
    public Slider ChargingSlider;
    public Image ChargeFill;


    // Start is called before the first frame update
    void Start()
    {
        ChargingSlider.maxValue = MaxChargeTime;
        m_chargeTimer = MaxChargeTime;
        m_isFullyCharged = true;

        SetChargeColours();
    }

    // Update is called once per frame
    void Update()
    {
        ChargingStarStone();
    }

    private void OnTriggerStay(Collider trigger)
    {
        if (trigger.CompareTag("Player"))
        {
            if (Input.GetButton("Interact") && m_isFullyCharged == true)
            {
                if(WaveSystem.IsWaveSystemInitiated == false)
                {
                    WaveSystem.IsWaveSystemInitiated = true;
                }


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

                WeaponID = 2;

                m_isFullyCharged = false;
                m_enableCharging = true;
                m_chargeTimer = 0;

                FindObjectOfType<WeaponsSelect>().SetWeapon();
                FindObjectOfType<PrototypeWeapon>().CurrentAmmo = AmmoToGive;
            }

        }
    }

    private void ChargingStarStone()
    {
        ChargingSlider.value = m_chargeTimer;

        if(m_enableCharging == true)
        {
            m_chargeTimer += Time.deltaTime * ChargeRate;

            if(m_chargeTimer > MaxChargeTime)
            {
                m_chargeTimer = MaxChargeTime;
                m_isFullyCharged = true;
            }
        }
    }

    private void SetChargeColours()
    {
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
        ChargeFill.color = new Color(ChargeColours[m_chargeColourIndex].x, ChargeColours[m_chargeColourIndex].y, ChargeColours[m_chargeColourIndex].z);
    }
}
