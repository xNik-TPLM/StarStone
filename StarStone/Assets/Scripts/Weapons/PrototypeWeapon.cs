using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeWeapon : WeaponBase
{
    private GameObject m_currentProjectile;

    public GameObject FireProjectile;
    public GameObject EarthProjectile;


    // Start is called before the first frame update
    /*void Start()
    {

    }*/

    // Update is called once per frame
    /*void Update()
    {

    }*/

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
