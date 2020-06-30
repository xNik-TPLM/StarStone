using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the player's weapon switching controlled by the mouse wheel
/// Worked By: Ben Smith
/// </summary>
public class WeaponsSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = InteractStarStone.WeaponID; // This sets a reference for the previous weapon used

        // If the player scrolls the mouse wheel upwards, the previous weapon ID will be collected and the player will switch to that weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (InteractStarStone.WeaponID >= transform.childCount - 1)
            {
                InteractStarStone.WeaponID = 0;
            }
            else
            {
                InteractStarStone.WeaponID++;
            }
        }
        // If the player scrolls the mouse wheel downwards, the previous weapon ID will be collected and the player will switch to that weapon
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (InteractStarStone.WeaponID <= 0)
            {
                InteractStarStone.WeaponID = transform.childCount -1;
            }
            else
            {
                InteractStarStone.WeaponID--;
            }
        }
        if (previousWeapon != InteractStarStone.WeaponID)
        {
            SetWeapon();
        }
    }

    // This enables and disables the corresponding weapons relative to which weapon has just been used
    public void SetWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == InteractStarStone.WeaponID)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
