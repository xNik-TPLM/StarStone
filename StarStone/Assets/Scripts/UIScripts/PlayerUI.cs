using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This script handles the player's defensive ability activating a shield to give the player extra health
/// Worked By: Ben Smith
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public Text ammoDisplay; // This displays the current ammo left in the clip
    public Text maxAmmo; // This displays the maximum ammo that is left that the player has
    private WeaponBase m_weapon;
    private PlayerController m_playerHealth;

    private bool m_timerEnabled;
    private float m_timer;
    public float Timer;

    public static bool shieldActive; // This checks if the shield is currently enabled
    public Slider healthSlider; // This sets a reference for the health bar
    public Slider shieldSlider; // This sets a reference for the shield bar

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true); // This enables the Player UI
        m_weapon = FindObjectOfType<WeaponBase>();
        m_playerHealth = FindObjectOfType<PlayerController>();
        m_timer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = m_playerHealth.currentHealth / 100;
        StopwatchCooldown();
        TimerCooldown();
        // Once the player takes damage, they will lose health depending on whether the shield has been enabled or not
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shieldSlider.value > 0)
            {
                shieldSlider.value -= 0.1f; // This lowers the shield value once damage has been taken
            }
            else
            {
                healthSlider.value -= 0.1f; // This lowers the health value once damage has been taken
            }
        }
        if (shieldActive == true)
        {

            gameObject.SetActive(false);
        }


        ammoDisplay.text = m_weapon.CurrentAmmo.ToString(); // This displays the current ammo left in the clip as a part of the player's HUD
        maxAmmo.text = m_weapon.MaxAmmo.ToString(); // This displays the maximum ammo left in the weapon as a part of the player's HUD
    }
    private void StopwatchCooldown()
    {
        //If something you want is eneabled
        if (m_timerEnabled == true)
        {
            m_timer += Time.deltaTime;

            if (m_timer > Timer)
            {
                m_timerEnabled = false;
                m_timer = 0;
                //Do whatever
            }
        }
    }
    private void TimerCooldown()
    {
        //If something you want is eneabled
        if (m_timerEnabled == true)
        {
            
            m_timer -= Time.deltaTime;

            if (m_timer <= 0)
            {
                m_timerEnabled = false;
                m_timer = Timer;
                //Do whatever
            }
        }
    }
}