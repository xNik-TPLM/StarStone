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
    private WaveSystem m_waveSystem;

    // Public bool which checks if the game is paused
    public static bool IsGamePaused;

    // GameObjects to set reference to the pause menu and cutscene
    public GameObject pauseMenu;
    public GameObject Cutscene;

    // Reference to the sound effects script
    private SoundFX m_sound;

    //Start is called before the first frame update
    void Start()
    {
        m_sound = FindObjectOfType<SoundFX>();
        m_waveSystem = FindObjectOfType<WaveSystem>();
    }

    // This function is called when the resume button is clicked
    public void ReturnToGame()
    {
        UnFreezeGame(); // This will continue the game by resuming time back to it's normal speed
        pauseMenu.SetActive(false); // This will resume the game by disabling the pause menu
        Cursor.lockState = CursorLockMode.Locked; // This will lock the cursor back in the centre of the screen and hide it
    }

    // This function is called when the retry button is clicked
    public void RetryGame()
    {
        m_waveSystem.DestroyAllEnemies();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); // This will reload the game from the start
        UnFreezeGame(); 
    }

    // This function is called when the return button is clicked
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("GameMenu"); // This will take the player back to the main menu
        m_waveSystem.DestroyAllEnemies();
        UnFreezeGame();

        if (TutorialController.InTutorialScene)
        {
            TutorialController.InTutorialScene = false;
        }
    }

    // FreezeGame will set all of objects relient on time to stop moving
    public static void FreezeGame()
    {
        PlayerController.ControlsEnabled = false;
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    // UnFreezeGame will continue the game by resuming time back to it's normal speed
    public static void UnFreezeGame()
    {
        PlayerController.ControlsEnabled = true;
        IsGamePaused = false;
        Time.timeScale = 1;
    }
}
