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
    //Float feild that times the fire rate of a weapon
    private float m_fireTime;

    //Weapon properties
    //Float properties
    public float FireRate; //Weapon fire rate
    public float RecoilUp; //Weapon recoil going up
    public float RecoilSide; //Weapon recoil going to the side

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
    }

    //This function handles when the player shoots their weapon
    private void PlayerShooting()
    {
        //If player holds down the left mouse button and if it is time to fire then
        if (Input.GetMouseButton(0) && Time.time >= m_fireTime)
        {
            //Instantiate a projectile prefab and use Muzzle's position and rotation to fire the projectile
            GameObject bullet = Instantiate(WeaponProjectile);
            bullet.transform.position = WeaponMuzzle.transform.position;
            bullet.transform.rotation = WeaponMuzzle.transform.rotation;

            //Set the fire timer
            m_fireTime = Time.time + 1 / FireRate;

            //Move camera for weapon recoil
            Camera.WeaponRecoil(RecoilSide / 3f, RecoilUp / 3f);
        }
    }
}
