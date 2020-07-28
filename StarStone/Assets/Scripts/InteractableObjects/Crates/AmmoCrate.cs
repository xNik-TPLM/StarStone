using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the ammo crate to provide the player with more ammo
///// Worked By: Ben Smith
///// </summary>
public class AmmoCrate : MonoBehaviour
{
    public static bool HasAmmoRefilled;
    public InteractionTextData InteractionText;
    public int ammoCrateValue; // This sets the ammo value the player receives once picked up

    // Start is called before the first frame update
    void Start()
    {
        HasAmmoRefilled = false;
    }

    // This checks whether the player has picked up the crate
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = true;
            PlayerUI.PopUpControlsText = InteractionText.InteractControlsText;

            if (Input.GetButtonDown("Interact") && HasAmmoRefilled == false)
            {
                HasAmmoRefilled = true;
                FindObjectOfType<WeaponBase>().MaxAmmo += ammoCrateValue;
            }
            else if (Input.GetButtonDown("Interact") && HasAmmoRefilled == true)
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