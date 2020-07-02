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
    //Private fields
    //Boolean field to chekc if the weapon is realodaing so that the player can't shoot, whilst realoding is happening
    private bool m_isWeaponReloading;

    public static bool IsFiring;

    public bool UseHitscan;

    //Float field that times the fire rate of a weapon
    private float m_fireTime;

    //Integer field to get the difference between ammo and clip size to have conservative ammo
    private int m_ammoDifference;

    //Weapon properties
    //Float properties
    public float FireRate; //Weapon fire rate
    public int CurrentAmmo; //Amount of ammo in the clip
    public int WeaponClipSize; //Weapon's clip size
    public int MaxAmmo; //Maximum ammo the weapon will have

    //Boolean property that will check if the weapon is automatic or semi-automatic
    public bool IsAutomatic;

    //Object references
    public GameObject WeaponProjectile; //Reference to the projectile that will be used to fire it out of the weapon
    public GameObject WeaponMuzzle; //Reference to the weapon muzzle that will be used to get the position of where the projectile will spawn


    public Transform recoilPosition;
    public Transform rotationalPoint;

    public float positionalrecoilspeed = 8f;
    public float rotationalrecoilspeed = 8f;

    public float positionalreturnspeed = 18f;
    public float rotationalreturnspeed = 38f;

    public Vector3 RecoilRotation = new Vector3(10, 5, 7);
    public Vector3 RecoilKickBack = new Vector3(0.015f, 0f, -0.2f);

    private Vector3 rotationalRecoil;
    private Vector3 positionalRecoil;
    private Vector3 Rotation;

    public EnemyBase EnemyTarget;

    private bool fired;

    private GameObject m_camera;

    protected GameObject SelectedProjectile;

    // Start is called before the first frame update
    void Start()
    {
        //Set current ammo as clip size and get reference 
        CurrentAmmo = WeaponClipSize;
        m_camera = GameObject.Find("Main Camera");
    }

    void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalreturnspeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalrecoilspeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalrecoilspeed * Time.fixedDeltaTime);
        Rotation = Vector3.Slerp(Rotation, rotationalRecoil, rotationalrecoilspeed * Time.fixedDeltaTime);
        rotationalPoint.localRotation = Quaternion.Euler(Rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //Run the shooting function
        PlayerShooting();
        WeaponReload();
    }

    //This function handles when the player shoots their weapon
    protected virtual void PlayerShooting()
    {
        //If there is ammo left
        if (CurrentAmmo > 0)
        {
            ///Automatic weapon solution
            //If player holds down the left mouse button and if it is time to fire and if the player is not reloading
            /*if (Input.GetMouseButton(0) && Time.time >= m_fireTime && m_isWeaponReloading == false)
            {
                IsFiring = true;

                SelectedProjectile = Instantiate(WeaponProjectile);
                CurrentAmmo -= 1;

                //Use Muzzle's position and rotation to fire the projectile
                SelectedProjectile.transform.position = WeaponMuzzle.transform.position;
                SelectedProjectile.transform.rotation = WeaponMuzzle.transform.rotation;
               
                //Set the fire timer
                m_fireTime = Time.time + 1 / FireRate;

                //Move camera for weapon recoil

                rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
                positionalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
            }*/

            ///Semi-automatic weapon solution
            //If the player presses the left mouse button and if the player is not reloading
            if(Input.GetMouseButtonDown(0) && m_isWeaponReloading == false)
            {
                //Instantiate a projectile and take away ammo by one
                //SelectedProjectile = Instantiate(WeaponProjectile);
                CurrentAmmo -= 1;

                //Initiate a raycast
                HitDetection();
                //Debug.Log("Firing");

                //Use Muzzle's position and rotation to fire the projectile
                //SelectedProjectile.transform.position = WeaponMuzzle.transform.position;
                //SelectedProjectile.transform.rotation = WeaponMuzzle.transform.rotation;

                //Move weapon for recoil
                rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
                positionalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
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

    //This function handles the raycast initiation
    private void HitDetection()
    {
        //Initiate raycast from the weapon muzzle and point it forward and get the information on the object hit from 
        if (Physics.Raycast(WeaponMuzzle.transform.position, WeaponMuzzle.transform.forward, out RaycastHit m_raycastHitDetector, 100) && UseHitscan == true)
        {
            EnemyTarget = m_raycastHitDetector.transform.GetComponent<EnemyBase>();

            if (EnemyTarget != null)
            {
                EnemyTarget.EnemyDamaged(45, InteractStarStone.StarStoneID);
            }
        }
    }
}
