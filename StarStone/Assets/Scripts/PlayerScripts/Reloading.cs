using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the player's reload animation
/// Worked By: Ben Smith
/// </summary>
public class Reloading : MonoBehaviour
{
    public Animator reloadAnimation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponBase.m_isWeaponReloading == true)
        {
            reloadAnimation.SetBool("Reloading", true);

            if (reloadAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                WeaponBase.m_isWeaponReloading = false;
            }
        }
        else
        {
            reloadAnimation.SetBool("Reloading", false);
        }
    }
}