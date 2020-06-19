using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalreturnspeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalreturnspeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalrecoilspeed * Time.fixedDeltaTime);
        Rotation = Vector3.Slerp(Rotation, rotationalRecoil, rotationalrecoilspeed * Time.fixedDeltaTime);
        rotationalPoint.localRotation = Quaternion.Euler(Rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(recoilPosition.localPosition);

        if (Input.GetMouseButton(0))
        {
            rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
        }
    }
}
