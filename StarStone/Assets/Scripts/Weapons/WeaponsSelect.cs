using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetWeapon();

        int previousWeapon = InteractStarStone.StarStoneID; // This sets the previously used weapon as the starstone ID

        // These check which direction the mouse wheel is being scrolled so the correct weapon can be enabled and used
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (InteractStarStone.StarStoneID >= transform.childCount - 1)
            {
                InteractStarStone.StarStoneID = 0;
            }
            else
            {
                InteractStarStone.StarStoneID++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (InteractStarStone.StarStoneID <= 0)
            {
                InteractStarStone.StarStoneID = transform.childCount -1;
            }
            else
            {
                InteractStarStone.StarStoneID--;
            }
        }
        if (previousWeapon != InteractStarStone.StarStoneID)
        {
            SetWeapon();
        }
    }

    // This enables and disables the weapons depending on which weapon the player has switched to
    public void SetWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == InteractStarStone.StarStoneID)
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
