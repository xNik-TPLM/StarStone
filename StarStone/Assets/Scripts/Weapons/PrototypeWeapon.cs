using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child of the weapon base
/// The prototype weapon 
/// </summary>

public class PrototypeWeapon : WeaponBase
{
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
            if (Input.GetButtonDown("Fire1"))
            {
                IsFiring = true;
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
                break;

            case 2:
                WeaponProjectile = IceProjectile;
                break;

            case 3:
                WeaponProjectile = WindProjectile;
                break;

            case 4:
                WeaponProjectile = EarthProjectile;
                break;
        }
    }
}
