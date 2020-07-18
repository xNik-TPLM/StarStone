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
    public GameObject Menu;
    public GameObject Cutscene;
    private SoundFX m_sound;

    //Start is called before the first frame update
    void Start()
    {
        m_sound = FindObjectOfType<SoundFX>();
    }

    // This function is called when the play button is clicked
    public void Play()
    {
        PauseMenu.UnFreezeGame();
        Cutscene.SetActive(true);
        Menu.SetActive(false);
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

    // This function is called when the quit button is clicked
    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
