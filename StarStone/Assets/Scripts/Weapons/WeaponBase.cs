using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private float m_fireTime;

    public float FireRate;

    public float RecoilUp;
    public float RecoilSide;

    public GameObject WeaponBullet;
    public GameObject WeaponMuzzle;

    public CameraMovement Camera;

    public float m_recoilReturnPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerShooting();
    }

    protected virtual void PlayerShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= m_fireTime)
        {
            GameObject bullet = Instantiate(WeaponBullet);
            bullet.transform.position = WeaponMuzzle.transform.position;
            bullet.transform.rotation = WeaponMuzzle.transform.rotation;

            m_fireTime = Time.time + 1 / FireRate;

            Camera.WeaponRecoil(RecoilSide / 3f, RecoilUp / 3f);
        }
    }
}
