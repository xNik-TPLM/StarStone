using System.Collections;
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
    public InteractionText InteractionText;
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
            //Set the text and display it
            PlayerUI.PopUpControlsText = InteractionText.InteractControlsText;
            PlayerUI.PopUpControlsEnabled = true;            

            //If the player pressed the "interact" button and the player is in the intermission phase
            if (Input.GetButtonDown("Interact") && WaveSystem.InIntermission == true)
            {

                switch (gameObject.name)
                {
                    case "WindStarStoneAlter":
                        if (HasWindSigilInteracted == false)
                        {
                            HasWindSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        break;

                    case "FireStarStoneAlter":
                        if(HasWindSigilInteracted == true && HasFireSigilInteracted == false)
                        {
                            HasFireSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        else if(HasWindSigilInteracted == false)
                        {
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[1];
                        }
                        break;

                    case "IceStarStoneAlter":
                        if(HasFireSigilInteracted == true && HasIceSigilInteracted == false)
                        {
                            HasIceSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        else if(HasFireSigilInteracted == false)
                        {
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[2];
                        }
                        break;

                    case "EarthStarStoneAlter":
                        if(HasIceSigilInteracted == true && HasEarthSigilInteracted == false)
                        {
                            HasEarthSigilInteracted = true;
                            StarStone.SetActive(true);
                            HasSigilInteracted = true;
                        }
                        else if(HasIceSigilInteracted == false)
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
