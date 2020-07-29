using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is to check if all Starstone alters have been activated, in order to end the game
/// Worked By: Nikodem Hamrol
/// </summary>

public class PowerSwitch : MonoBehaviour
{
    //This boolean is used to check if all Starstone alters have been activated, which then the player can end the game
    public static bool CanInteractPowerSwitch;

    [Tooltip("This is a reference to the interction text script, which allow to add control text and any pop up messages")]
    public InteractionTextData InteractionText; //This script reference will make all the data available for editing in the inspector to set controls message and pop up message
    [Tooltip("This is a reference to the animator in the AnimationPivot object inside the powerswitch object")]
    public Animator PowerSwitchAnimator; //This animator will control the handle
    [Tooltip("This is object reference to the cutscene on the scene, which will display the ending")]
    public GameObject EndingCutscene; //This game object is the final cutscene with 

    // Start is called before the first frame update
    void Start()
    {
        //Power switch is disabled and turn the powerswitch on in terms of animation
        CanInteractPowerSwitch = false;
        PowerSwitchAnimator.SetBool("Switching", false);
    }

    //When a player enters and stays within the power switch, box trigger
    private void OnTriggerStay(Collider other)
    {
        //Ckeck ta for the player
        if (other.CompareTag("Player"))
        {
            //Set the controls text and display it on screen
            PlayerUI.PopUpControlsText = InteractionText.InteractControlsText;
            PlayerUI.PopUpControlsEnabled = true;
            
            //Check if the player interacts with the power switch and all alters have been activated, then end the game, which is in the coroutine
            //if (Input.GetButtonDown("Interact") && InteractAlters.HasAllSigilsActivated == true)
            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(EndTheGame());
            }

            //check if the the player interacts with the power switch and if all of the alters are not activated, then pop up a message, that says to activate all alters
            if(Input.GetButtonDown("Interact") && InteractAlters.HasAllSigilsActivated == false)
            {
                PlayerUI.PopUpMessageText = InteractionText.InteractPopUpMessages[0];
                PlayerUI.PopUpMessageEnabled = true;
            }
        }
    }

    //This coroutine will end the game and display the final cutscene
    private IEnumerator EndTheGame()
    {
        //Start the animation
        PowerSwitchAnimator.SetBool("Switching", true);

        //Wait until the time of he animation passes
        yield return new WaitForSeconds(1.1f);

        //Set the game has been completed, disable mouse lock, display the final cutscene and freeze the game
        WaveSystem.GameCompleted = true;
        Cursor.lockState = CursorLockMode.None;
        EndingCutscene.SetActive(true);
        PauseMenu.FreezeGame();
    }

    //When a player extis the power switch's box trigger, hide the controls text
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = false;
        }
    }
}
