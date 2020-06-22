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
        int previousWeapon = InteractStarStone.WeaponID;

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
