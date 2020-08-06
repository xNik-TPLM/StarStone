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

    //This will be used to display the text of wha alter to activate
    public static int AlterActivatedIndex;

    [Tooltip("This is a reference to the interction text script, which allow to add control text and any pop up messages")]
    public InteractionTextData InteractionText; //This script reference will make all the data available for editing in the inspector to set controls message and pop up message
    [Tooltip("This is for the starstone object inside tha alter prefab")]
    public GameObject StarStone; //The starstone object is sed as visual feedback that the starstone has been interacted

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

        //Set this index to 1 as the wind alter should be the first one to activated
        AlterActivatedIndex = 1;
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

    //When a player enters and stays within a sigils, box trigger
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

                            //It will set the text back to the wave number and increment the alter activated index to display the text to activate the next alter, for the next time that player's in the intermission phase
                            WaveSystem.GameStateIndex = 1;
                            AlterActivatedIndex++;
                        }
                        //If this sigil has been interacted
                        else if(HasWindSigilInteracted == true && HasSigilInteracted == false) 
                        {
                            //Pop up a message to tell that the player has already interacted this Starstone and activate the next one
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[1];
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
                            WaveSystem.GameStateIndex = 1;
                            AlterActivatedIndex++;
                        }

                        //If the wind sigil has still not been activated
                        if(HasWindSigilInteracted == false)
                        {
                            //It will pop up a message to activate the wind sigil first
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[2];
                        }

                        //If this sigil has been interacted
                        if (HasFireSigilInteracted == true && HasSigilInteracted == false)
                        {
                            //Pop up a message to tell that the player has already interacted this Starstone and activate the next one
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[3];
                        }
                        break;

                    case "IceStarStoneAlter":
                        //If the fire sigil has been interacted but the ice sigil hasn't
                        if (HasFireSigilInteracted == true && HasIceSigilInteracted == false)
                        {
                            HasIceSigilInteracted = true;
                            HasSigilInteracted = true;
                            StarStone.SetActive(true);
                            WaveSystem.GameStateIndex = 1;
                            AlterActivatedIndex++;
                        }

                        //If the fire sigil has still not been activated
                        if (HasFireSigilInteracted == false)
                        {
                            //It will pop up a message to activate the fire sigil first
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[4];
                        }

                        //If this sigil has been interacted
                        if (HasIceSigilInteracted == true && HasSigilInteracted == false)
                        {
                            //Pop up a message to tell that the player has already interacted this Starstone and activate the next one
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[5];
                        }
                        break;

                    case "EarthStarStoneAlter":
                        //If the ice sigil has been interacted but the earth sigil hasn't
                        if (HasIceSigilInteracted == true && HasEarthSigilInteracted == false)
                        {
                            HasEarthSigilInteracted = true;
                            HasSigilInteracted = true;
                            StarStone.SetActive(true);
                            WaveSystem.GameStateIndex = 1;
                            AlterActivatedIndex++;
                        }

                        //If the ice sigil has still not been activated
                        if (HasIceSigilInteracted == false)
                        {
                            //It will pop up a message to activate the ice sigil first
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[6];
                        }

                        //If this sigil has been interacted
                        if(HasEarthSigilInteracted == true && HasSigilInteracted == false)
                        {
                            //Pop up a message to tell that the player has already interacted this Starstone and activate the next one
                            PlayerUI.PopUpMessageEnabled = true;
                            PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[7];
                        }
                        break;
                }
            }
            //If the player is not in the intermission phase, it will pop a message that they can't interact with it
            else if(Input.GetButtonDown("Interact") && WaveSystem.InIntermission == false)
            {
                PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[0];
                PlayerUI.PopUpMessageEnabled = true;
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
