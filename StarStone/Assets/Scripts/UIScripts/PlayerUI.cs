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

    public static bool shieldActive;
    //public Slider shieldSlider;
    public Slider healthSlider;
    public Slider shieldSlider;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true); // This enables the 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shieldSlider.value > 0)
            {
                shieldSlider.value -= 0.1f;
            }
            else
            {
                healthSlider.value -= 0.1f;
            }
        }
        if (shieldActive == true)
        {

            gameObject.SetActive(false);
        }

        ammoDisplay.text = GetComponent<WeaponBase>().CurrentAmmo.ToString();
        maxAmmo.text = GetComponent<WeaponBase>().MaxAmmo.ToString();
    }
}