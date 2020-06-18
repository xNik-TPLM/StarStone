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
    }

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
