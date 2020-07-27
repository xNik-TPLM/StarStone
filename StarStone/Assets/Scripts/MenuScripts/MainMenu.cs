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
    // Public GameObjects to set references to the menu and cutscene
    public GameObject Menu;
    public GameObject Cutscene;
    
    // Reference for the sound script
    private SoundFX m_sound;

    //Start is called before the first frame update
    void Start()
    {
        m_sound = FindObjectOfType<SoundFX>();
        m_sound.MenuTheme.Play();
        Cursor.lockState = CursorLockMode.None;
    }

    // This function is called when the play button is clicked
    public void Play()
    {
        PauseMenu.UnFreezeGame(); // This resets time to pass at the normal rate
        Cutscene.SetActive(true); // This enables the cutscene canvas
        Menu.SetActive(false); // This disables the main menu
        PlayerController.ControlsEnabled = true;

        // These stop the main menu theme and play the cutscene theme
        m_sound.MenuTheme.Stop();
        m_sound.CutsceneTheme.Play();
    }

    // This function is called when the start cutscene has finished
    public void LoadGame()
    {
        SceneManager.LoadScene("WhiteBox");
    }

    // This function is called when the end cutscene has finished
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    // This function is called when the tutorial button is clicked
    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialMap");
        PlayerController.ControlsEnabled = true;
    }

    // This function is called when the quit button is clicked
    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit(); // This will quit the game
    }
}