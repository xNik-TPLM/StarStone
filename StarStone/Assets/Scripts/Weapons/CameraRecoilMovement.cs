using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This sript deals with camera movement for recoil of the weapon.
/// It is mainly attatched to the camera holder object, instead of the main camera object, because it the camera rotates on all axis, whereas the camera object roates only on the the x axis.
/// </summary>

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
        if (Input.GetMouseButtonDown(0) && GetComponentInChildren<WeaponBase>().CurrentAmmo > 0)
        {
            currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
        }      
    }
}
