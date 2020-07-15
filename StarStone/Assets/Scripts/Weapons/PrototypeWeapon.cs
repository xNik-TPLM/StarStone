using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child of the weapon base
/// The prototype weapon 
/// Worked By: Nikodem Hamrol
/// </summary>

public class PrototypeWeapon : WeaponBase
{
    private float m_fireRate;

    public GameObject FireProjectile;
    public GameObject IceProjectile;
    public GameObject WindProjectile;
    public GameObject EarthProjectile;

    public SoundFX m_sound;

    protected override void Start()
    {
        m_sound = FindObjectOfType<SoundFX>();
    }

    protected override void PlayerShooting()
    {
        if (CurrentAmmo > 0)
        {   
            if(Input.GetButton("Fire1") && Time.time >= m_fireRate && IsAutomatic == true)
            {
                m_sound.PrototypeFire.Play();
                GameObject projectile = Instantiate(WeaponProjectile);
                CurrentAmmo -= 1;
                projectile.transform.position = WeaponMuzzle.transform.position;
                projectile.transform.rotation = WeaponMuzzle.transform.rotation;

                m_fireRate = Time.time + 1 / FireRate;
            }

            if (Input.GetButtonDown("Fire1") && IsAutomatic == false)
            {
                m_sound.PrototypeFire.Play();
                GameObject projectile = Instantiate(WeaponProjectile);
                CurrentAmmo -= 1;
                projectile.transform.position = WeaponMuzzle.transform.position;
                projectile.transform.rotation = WeaponMuzzle.transform.rotation;
            }
        }
        SetPrototypeProjectile();
    }

    private void SetPrototypeProjectile()
    {
        switch (InteractStarStone.StarStoneID)
        {            
            case 1:
                WeaponProjectile = FireProjectile;
                IsAutomatic = false;
                break;

            case 2:
                WeaponProjectile = IceProjectile;
                IsAutomatic = false;
                break;

            case 3:
                WeaponProjectile = WindProjectile;
                IsAutomatic = true;
                break;

            case 4:
                WeaponProjectile = EarthProjectile;
                IsAutomatic = false;
                break;
        }
    }
}
