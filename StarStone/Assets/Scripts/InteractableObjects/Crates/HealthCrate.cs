using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the health crate to grant the player more health
///// Worked By: Ben Smith
///// </summary>
public class HealthCrate : MonoBehaviour
{
    private PlayerController m_player; // Sets the reference to the player script
    public static bool HealthKitUsed;
    public InteractionTextData InteractionText;
    public int healthCrateValue; // This sets the health value the player receives once picked up

    // Start is called before the first frame update
    void Start()
    {
        HealthKitUsed = false;
        m_player = FindObjectOfType<PlayerController>();
    }

    // This checks whether the player has picked up the crate
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = true;
            PlayerUI.PopUpControlsText = InteractionText.InteractControlsText;

            if (Input.GetButtonDown("Interact") && HealthKitUsed == false && m_player.currentHealth != m_player.maxHealth)
            {
                HealthKitUsed = true;

                if (m_player.currentHealth < 50)
                {
                    m_player.currentHealth += healthCrateValue;
                }
                else
                {
                    m_player.currentHealth = m_player.maxHealth;
                }
            }
            else if (Input.GetButtonDown("Interact") && HealthKitUsed == true)
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