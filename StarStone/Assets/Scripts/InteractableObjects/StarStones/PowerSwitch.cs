using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Worked By: Nikodem Hamrol
/// </summary>

public class PowerSwitch : MonoBehaviour
{
    public static bool CanInteractPowerSwitch;

    public GameObject EndingCutscene;

    // Start is called before the first frame update
    void Start()
    {
        CanInteractPowerSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerUI.PopUpControlsEnabled = true;
            PlayerUI.PopUpControlsText = "Press [F] To Turn Off The Generator";

            //if (Input.GetButtonDown("Interact") && InteractAlters.HasAllSigilsActivated == true)
            if (Input.GetButtonDown("Interact"))
            {
                Cursor.lockState = CursorLockMode.None;
                EndingCutscene.SetActive(true);
                WaveSystem.GameCompleted = true;
                PauseMenu.FreezeGame();
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
