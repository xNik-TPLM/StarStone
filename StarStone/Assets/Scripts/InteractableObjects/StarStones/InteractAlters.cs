﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Worked By: Nikodem Hamrol
/// </summary>

public class InteractAlters : MonoBehaviour
{
    public static bool m_hasWindSigilInteracted;
    public static bool m_hasFireSigilInteracted;
    public static bool m_hasIceSigilInteracted;
    public static bool m_hasEarthSigilInteracted;

    public static bool HasSigilInteracted;
    public static bool HasAllSigilsActivated;

    public InteractionText InteractionText;
    public GameObject StarStone;

    // Start is called before the first frame update
    void Start()
    {
        m_hasWindSigilInteracted = false;
        m_hasFireSigilInteracted = false;
        m_hasIceSigilInteracted = false;
        m_hasEarthSigilInteracted = false;

        HasAllSigilsActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_hasWindSigilInteracted && m_hasFireSigilInteracted && m_hasIceSigilInteracted && m_hasEarthSigilInteracted)
        {
            HasAllSigilsActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = true;
            PlayerUI.PopUpControlsText = InteractionText.InteractControlsText;

            if (Input.GetButtonDown("Interact") && WaveSystem.InIntermission == true)
            {
                switch (gameObject.name)
                {
                    case "WindStarStoneAlter":
                        if (m_hasWindSigilInteracted == false)
                        {
                            m_hasWindSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        break;

                    case "FireStarStoneAlter":
                        if(m_hasWindSigilInteracted == true && m_hasFireSigilInteracted == false)
                        {
                            m_hasFireSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        else if(m_hasWindSigilInteracted == false)
                        {
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[1];
                        }
                        break;

                    case "IceStarStoneAlter":
                        if(m_hasFireSigilInteracted == true && m_hasIceSigilInteracted == false)
                        {
                            m_hasIceSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        else if(m_hasFireSigilInteracted == false)
                        {
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[2];
                        }
                        break;

                    case "EarthStarStoneAlter":
                        if(m_hasIceSigilInteracted == true && m_hasEarthSigilInteracted == false)
                        {
                            m_hasEarthSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        else if(m_hasIceSigilInteracted == false)
                        {
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[3];
                        }
                        break;
                }
            }
            else if(Input.GetButtonDown("Interact") && WaveSystem.InIntermission == false)
            {
                PlayerUI.PopUpMessageEnabled = true;
                PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[0];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = false;
        }
    }
}
