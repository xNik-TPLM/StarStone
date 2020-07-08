using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// References: Brackeys. (2017). Unity Tutorial - START MENU in Unity [online]. Available: https://www.youtube.com/watch?v=zc8ac_qUXQY [Last Accessed 8th July 2020].
/// This script handles the game's main menu
/// Worked By: Ben Smith
/// </summary>
public class MainMenu : MonoBehaviour
{
    // This function is called when the play button is clicked
    public void Play()
    {
        SceneManager.LoadScene("BenTestScene");
    }

    // This function is called when the quit button is clicked
    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
