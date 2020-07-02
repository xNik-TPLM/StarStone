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
    public GameObject EarthProjectile;

    protected override void PlayerShooting()
    {
        if (CurrentAmmo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                WeaponProjectile = EarthProjectile;
                break;

            case 2:
                WeaponProjectile = FireProjectile;
                break;
        }
    }
}
