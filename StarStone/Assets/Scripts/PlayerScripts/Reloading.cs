using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    public Animator reloadAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //reloadAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadCheck();
    }

    public void ReloadCheck()
    {
        if (WeaponBase.m_isWeaponReloading == true)
        {
            ReloadAnimation();
        }
        if (WeaponBase.m_isWeaponReloading == false)
        {
            StopReloadAnimation();
        }
    }

    public void ReloadAnimation()
    {
        reloadAnimation.SetBool("Reloading", true);
    }

    public void StopReloadAnimation()
    {
        if (reloadAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            reloadAnimation.SetBool("Reloading", false);
        }
    }
}