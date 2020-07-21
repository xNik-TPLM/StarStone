using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the ammo crate to provide the player with more ammo
///// Worked By: Ben Smith
///// </summary>
public class AmmoCrate : MonoBehaviour
{
    private WeaponBase m_weaponAdd; // Sets the reference to the weapon script
    public int ammoCrateValue; // This sets the ammo value the player receives once picked up

    // Start is called before the first frame update
    void Start()
    {
        m_weaponAdd = FindObjectOfType<WeaponBase>();
    }

    // This checks whether the player has picked up the crate
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_weaponAdd.MaxAmmo += ammoCrateValue;
            gameObject.SetActive(false); // This disables the crate
        }
    }
}