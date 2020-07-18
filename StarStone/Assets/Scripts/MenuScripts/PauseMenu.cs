using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the in-game pause menu
/// Worked By: Ben Smith
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Cutscene;
    private SoundFX m_sound;

    //Start is called before the first frame update
    void Start()
    {
        m_sound = FindObjectOfType<SoundFX>();
    }

    // This function is called when the resume button is clicked
    public void ReturnToGame()
    {
        pauseMenu.SetActive(false); // This will resume the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // This function is called when the retry button is clicked
    public void RetryGame()
    {
        SceneManager.LoadScene("WhiteBox"); // This will reload the game from the start
    }

    // This function is called when the return button is clicked
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("GameMenu"); // This will take the player back to the main menu
    }
}
