using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a data script, that will be used for interacting objects, which will show the controls and pop up messages
/// </summary>

[System.Serializable]
public class InteractionTextData
{
    [Tooltip("Text for controls, so that the player knows what button to press followed by the action it will do")]
    public string InteractControlsText; //Text to show the controls and what action it will do
    [Tooltip("Set the size of how many pop up messages the object should have, so that the player knows, why they can't interact with it")]
    public string[] InteractPopUpMessages; //Text to show the message of why the player can't interact with this object
}
