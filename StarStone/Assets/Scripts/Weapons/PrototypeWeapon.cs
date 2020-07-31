using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child of the weapon base
/// The prototype weapon will shoot elemental projectles, which are picked up from the Starstones. This will shoot physical projectiles, instead of using raycasts
/// Worked By: Nikodem Hamrol
/// References: Swindle Creative. (2019). Weapon Recoil Script - How To Make Procedural Recoil In Unity [online]. Available: https://www.youtube.com/watch?v=6hyQ2rPkMDY [Last Accessed 19th June 2020].
/// </summary>

public class PrototypeWeapon : WeaponBase
{
    //Prototype Weapon fields
    //This float is the fire rate of the projectiles that will spawn
    private float m_fireRate;

    [Header("Elemental Projectiles")]
    [Tooltip("This is the projectile that prototype weapon is currenty using. Only for debugging")]
    public GameObject ProjectileToFire; //This game object is the projectile that will be fired

    //These game objects are prefabs of each elemental projectile
    [Tooltip("Fire projectile prefab")]
    public GameObject FireProjectile;
    [Tooltip("Ice projectile prefab")]
    public GameObject IceProjectile;
    [Tooltip("Ice projectile prefab")]
    public GameObject WindProjectile;
    [Tooltip("Earth projectile prefab")]
    public GameObject EarthProjectile;

    //The start function is overridden to just get the sound reference
    protected override void Start()
    {
        m_soundReference = FindObjectOfType<SoundFX>();
    }

    //This function is overridden to change the way this weapon fires as it uses physical projectiles
    protected override void PlayerShooting()
    {
        //If there's ammo in this weapon
        if (CurrentAmmo > 0)
        {   
            //When the player fires on automatic mode of the prototype weapon, which is the mode for the wind Starstone
            if(Input.GetButton("Fire1") && Time.time >= m_fireRate && IsAutomatic == true)
            {
                //Play the firing sound and instantiate the projectile and decrement ammo by 1
                m_soundReference.PrototypeFire.Play();
                GameObject projectile = Instantiate(ProjectileToFire);
                CurrentAmmo -= 1;

                //Spawn the projectile where the muzzle is
                projectile.transform.position = WeaponMuzzle.transform.position;
                projectile.transform.rotation = WeaponMuzzle.transform.rotation;

                //Set fire rate
                m_fireRate = Time.time + 1 / FireRate;
            }

            //When the player fires on semi-automatic mode of the prototype weapon, which any Starstone other than the wind
            if (Input.GetButtonDown("Fire1") && IsAutomatic == false)
            {
                //Play the firing sound, instantiate the projectile and decrement ammo by ammo
                m_soundReference.PrototypeFire.Play();
                GameObject projectile = Instantiate(ProjectileToFire);
                CurrentAmmo -= 1;

                //Instantiate the projectile at the muzzle position
                projectile.transform.position = WeaponMuzzle.transform.position;
                projectile.transform.rotation = WeaponMuzzle.transform.rotation;

                //Apply recoil, with kick back and rotation
                m_weaponRotationalRecoil += new Vector3(-RecoilRotationMaxRange.x, Random.Range(-RecoilRotationMaxRange.y, RecoilRotationMaxRange.y), Random.Range(-RecoilRotationMaxRange.z, RecoilRotationMaxRange.z));
                m_weaponCurrentPosition += new Vector3(Random.Range(-RecoilKickBackMaxRange.x, RecoilKickBackMaxRange.x), Random.Range(-RecoilKickBackMaxRange.y, RecoilKickBackMaxRange.y), RecoilKickBackMaxRange.z);
            }
        }

        //Set the projectile based on the Starstone it was interacted
        SetPrototypeProjectile();
    }

    //This function will set the projectiles for the prototype weapon
    private void SetPrototypeProjectile()
    {
        //This switch will set the prototype weapon based on Starstone it was interacted
        switch (InteractStarStone.StarStoneID)
        {            
            case 1: //Fire Starstone, sets the fire projectile
                ProjectileToFire = FireProjectile;
                IsAutomatic = false;
                break;

            case 2: //Ice Starstone, sets the ice projectile
                ProjectileToFire = IceProjectile;
                IsAutomatic = false;
                break;

            case 3: //Wind Starstone, sets the wind projectile, but also makes the weapon fully automatic
                ProjectileToFire = WindProjectile;
                IsAutomatic = true;
                break;

            case 4: //Earth Starstone, sets the earth projectile
                ProjectileToFire = EarthProjectile;
                IsAutomatic = false;
                break;
        }
    }
}
