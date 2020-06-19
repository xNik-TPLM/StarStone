using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a base script for weapons in the game.
/// It holds main functionalities, such as firing, and properties that each weapon will have.
/// Worked by: Nikodem Hamrol
/// References: Gabriel Aguiar Prod. (2018). Unity 2018 - Game VFX - Projectile/Bullet Raycast Tutorial [online]. Available: https://www.youtube.com/watch?v=xenW67bXTgM [Last Accessed 9th June June 2020].
/// </summary>

public class WeaponBase : MonoBehaviour
{
    //Private feilds
    //Boolean feild to chekc if the weapon is realodaing so that the player can't shoot, whilst realoding is happening
    private bool m_isWeaponReloading;

    //Float feild that times the fire rate of a weapon
    private float m_fireTime;

    //Integer field to get the difference between ammo and clip size to have conservative ammo
    private int m_ammoDifference;

    //Weapon properties
    //Float properties
    public float FireRate; //Weapon fire rate
    public float RecoilUp; //Weapon recoil going up
    public float RecoilSide; //Weapon recoil going to the side
    public static int CurrentAmmo; //Amount of ammo in the clip
    public int WeaponClipSize; //Weapon's clip size
    public int MaxAmmo; //Maximum ammo the weapon will have

    //Boolean property that will check if the weapon is automatic or semi-automatic
    public bool IsAutomatic;

    //Object references
    public GameObject WeaponProjectile; //Reference to the projectile that will be used to fire it out of the weapon
    public GameObject WeaponMuzzle; //Reference to the weapon muzzle that will be used to get the position of where the projectile will spawn

    //Reference to the camera script to get the recoil movement
    public CameraMovement Camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Run the shooting function
        PlayerShooting();
        WeaponReload();
    }

    //This function handles when the player shoots their weapon
    private void PlayerShooting()
    {
        //If there is ammo left
        if (CurrentAmmo > 0)
        {
            //If player holds down the left mouse button and if it is time to fire and if the player is not reloading
            if (Input.GetMouseButton(0) && Time.time >= m_fireTime && m_isWeaponReloading == false)
            {
                //Instantiate a projectile prefab and take away from ammo
                GameObject bullet = Instantiate(WeaponProjectile);
                CurrentAmmo -= 1;

                //Use Muzzle's position and rotation to fire the projectile
                bullet.transform.position = WeaponMuzzle.transform.position;
                bullet.transform.rotation = WeaponMuzzle.transform.rotation;
                
                //Set the fire timer
                m_fireTime = Time.time + 1 / FireRate;

                //Move camera for weapon recoil
                Camera.WeaponRecoil(RecoilSide / 3f, RecoilUp / 3f);
            }
        }
    }

    //This function handles the reloading of a weapon
    private void WeaponReload()
    {
        //If R key is pressed and if player is not already reloading
        if (Input.GetKeyDown(KeyCode.R) && m_isWeaponReloading == false)
        {
            //Realoding is true, so it's in progress
            m_isWeaponReloading = true;

            //If current clip is not full
            if (CurrentAmmo < WeaponClipSize)
            {
                //Set ammo difference, by subtracting the clip size by ammo in clip
                m_ammoDifference = WeaponClipSize - CurrentAmmo;

                //If there's more ammo left
                if(MaxAmmo > m_ammoDifference)
                {
                    CurrentAmmo += m_ammoDifference; //Add the ammo difference to the current ammo, so that it's not bigger than the clip size
                    MaxAmmo -= m_ammoDifference; //Subtract max ammo by the ammo difference
                }
                else //If there's no ammo left
                {
                    //Add the remaing ammo and set max ammo to 0
                    CurrentAmmo += MaxAmmo;
                    MaxAmmo = 0;
                }

                //Once all of that is done, set realoding to false
                m_isWeaponReloading = false;
            }
        }
    }
}
