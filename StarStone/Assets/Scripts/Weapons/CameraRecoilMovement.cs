using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoilMovement : MonoBehaviour
{
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    public Vector3 RecoilRotation = new Vector3(2f, 2f, 2f);

    private Vector3 currentRotation;
    private Vector3 Rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        Rotation = Vector3.Slerp(Rotation, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(Rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && WeaponBase.CurrentAmmo > 0)
        {
            currentRotation += new Vector3(Random.Range(-RecoilRotation.x, RecoilRotation.x), currentRotation.y, Random.Range(-RecoilRotation.z, RecoilRotation.z));
        }       
    }
}
