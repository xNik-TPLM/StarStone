﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is how the player will interact with the alters in the "God Rooms" in order to progress through the game.
/// Worked By: Nikodem Hamrol
/// </summary>

public class InteractAlters : MonoBehaviour
{
    //These public static booleans will check if each elemental alter sigils have been interacted already.
    //We do not use it as private as it doesnt pass on the statement to toher alters.
    public static bool HasWindSigilInteracted;
    public static bool HasFireSigilInteracted;
    public static bool HasIceSigilInteracted;
    public static bool HasEarthSigilInteracted;

    //This is to check if one sigil has been interacted already, which will allow for next wave initiation
    public static bool HasSigilInteracted;

    //This is to check if all sigils have been activated, which allow the player to use the power switch
    public static bool HasAllSigilsActivated;

    [Tooltip("This is a reference to the interction text script, which allow to add control text and any pop up messages")]
    public InteractionTextData InteractionText;
    [Tooltip("This is for the starstone object inside tha alter prefab")]
    public GameObject StarStone;

    // Start is called before the first frame update
    void Start()
    {
        //Set all these values to false, when starting
        HasWindSigilInteracted = false;
        HasFireSigilInteracted = false;
        HasIceSigilInteracted = false;
        HasEarthSigilInteracted = false;
        HasSigilInteracted = false;
        HasAllSigilsActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This will check if all sigils have been activated, which will set the boolean to true and it will allow the player to use the power switch
        if(HasWindSigilInteracted && HasFireSigilInteracted && HasIceSigilInteracted && HasEarthSigilInteracted)
        {
            HasAllSigilsActivated = true;
        }
    }

    //When a player enters and stays within the sigils, box trigger
    private void OnTriggerStay(Collider other)
    {
        //Check if the object is tagged as player
        if (other.CompareTag("Player"))
        {
            //Set the controls text and display it on the screen
            PlayerUI.PopUpControlsText = InteractionText.InteractControlsText;
            PlayerUI.PopUpControlsEnabled = true;            

            //If the player pressed the "interact" button and the player is in the intermission phase
            if (Input.GetButtonDown("Interact") && WaveSystem.InIntermission == true)
            {
                //The activation of sigils will be based on the names of the game object
                switch (gameObject.name)
                {
                    case "WindStarStoneAlter":
                        //If the wind sigil hasn't been activated
                        if (HasWindSigilInteracted == false)
                        {
                            //The wind sigil has been interacted, which will initiate the next wave and show the wind starstone
                            HasSigilInteracted = true;
                            HasWindSigilInteracted = true;
                            StarStone.SetActive(true);
                            
                        }
                        break;

                    case "FireStarStoneAlter":
                        //If the wind sigil has been interacted but the fire sigil hasn't
                        if(HasWindSigilInteracted == true && HasFireSigilInteracted == false)
                        {
                            //The fire sigil has been interacted, which will initiate the next wave and show the fire starstone
                            HasSigilInteracted = true;
                            HasFireSigilInteracted = true;
                            StarStone.SetActive(true);
                        }
                        //If the wind sigil has still not been activated
                        else if(HasWindSigilInteracted == false)
                        {
                            //It will pop up a message to activate the wind sigil first
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[1];
                        }
                        break;

                    case "IceStarStoneAlter":
                        //If the fire sigil has been interacted but the ice sigil hasn't
                        if (HasFireSigilInteracted == true && HasIceSigilInteracted == false)
                        {
                            HasIceSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        //If the fire sigil has still not been activated
                        else if (HasFireSigilInteracted == false)
                        {
                            //It will pop up a message to activate the fire sigil first
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[2];
                        }
                        break;

                    case "EarthStarStoneAlter":
                        //If the ice sigil has been interacted but the earth sigil hasn't
                        if (HasIceSigilInteracted == true && HasEarthSigilInteracted == false)
                        {
                            HasEarthSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        //If the ice sigil has still not been activated
                        else if (HasIceSigilInteracted == false)
                        {
                            //It will pop up a message to activate the ice sigil first
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[3];
                        }
                        break;
                }
            }
            //If the player is not in the intermission phase, it will pop a message that they can't interact with it
            else if(Input.GetButtonDown("Interact") && WaveSystem.InIntermission == false)
            {
                PlayerUI.PopUpMessageEnabled = true;
                PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[0];
            }
        }
    }

    //When the player leaves the box trigger, it will hide the controls pop up
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = false;
        }
    }
}
