using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls the ending cutscene, by display the texts in the correct order
/// Worked By: Nikodem Hamrol
/// </summary>

public class EndingCutscene : MonoBehaviour
{
    //This is to change between game objects that shows the text
    private int m_cutsceneTextIndex;

    //These properties are text which shows at the end when the game ends, which are the ending text and the credits text
    [Tooltip("The final text that is attached to the cutscene canvas")]
    public GameObject EndingText;
    [Tooltip("The credits text that is attached to the cutscene canvas")]
    public GameObject CreditsText;

    // Start is called before the first frame update
    void Start()
    {
        m_cutsceneTextIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Switch between the index to show the correct text
        switch (m_cutsceneTextIndex)
        {
            case 1: //Show the ending text and hide the credits
                EndingText.SetActive(true);
                CreditsText.SetActive(false);
                break;

            case 2: //Hide the ending text and show the credits
                EndingText.SetActive(false);
                CreditsText.SetActive(true);
                break;

            case 3: //Go to the main menu
                WaveSystem.GameCompleted = false;
                SceneManager.LoadScene("GameMenu");
                break;
        }
    }

    //This function will be used on the button to show the next text
    public void ButtonNext()
    {
        m_cutsceneTextIndex++;
    }
}
