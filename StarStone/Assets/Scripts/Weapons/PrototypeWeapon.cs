using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeWeapon : WeaponBase
{
    public GameObject FireProjectile;
    public GameObject EarthProjectile;

    protected override void PlayerShooting()
    {
        base.PlayerShooting();
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
