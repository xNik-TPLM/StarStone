using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This sript deals with camera movement for recoil of the weapon.
/// It is mainly attatched to the camera holder object, instead of the main camera object, because it the camera rotates on all axis, whereas the camera object roates only on the the x axis.
/// Worked By: Nikodem Hamrol
/// References: Swindle Creative. (2019). Weapon Recoil Script - How To Make Procedural Recoil In Unity [online]. Available: https://www.youtube.com/watch?v=6hyQ2rPkMDY [Last Accessed 19th June 2020].
/// </summary>

public class CameraRecoilMovement : MonoBehaviour
{
    //Camera recoil fields
    //Vector3 fields
    private Vector3 m_currentCameraHolderRotation; //This is the camera holder's current rotation, which is used to set a new rotation when fired and return it back to original position
    private Vector3 m_cameraHolderRotation; //This is the camera holder's rotation, which is used to rotate the camera holder with the current camera holder's rotation
    
    //Camera Recoil properties
    [Header("Recoil Properties")]
    [Tooltip("The speed of the camera holder's rotation")]
    public float RotationSpeed; //This is the speed of the rotation speed
    [Tooltip("The return speed of the camera holder, back to its previous rotation")]
    public float ReturnSpeed; //This is the speed of the rotation returning to its previous position
    [Tooltip("This is the maximum range the camera holder will rotate")]
    public Vector3 RecoilRotationMaxRange; //The max rotation range of the camera holder will be used to set the rotation of the camera recoil between the max negative vlaue and max positive value

    // Fixed Update is called once per fixed frame
    void FixedUpdate()
    {
        //Setting the current camera holder roation with a lerp, which will return the camera holder rotation back to 0, which is the rotation, before it was fired
        m_currentCameraHolderRotation = Vector3.Lerp(m_currentCameraHolderRotation, Vector3.zero, ReturnSpeed * Time.fixedDeltaTime);

        //Setting the camera holder rotation with a slerp, which will move the camera holder spherically. This helps the current camera holder rotation to move in the correct motion at constant rate as well 
        m_cameraHolderRotation = Vector3.Slerp(m_cameraHolderRotation, m_currentCameraHolderRotation, RotationSpeed * Time.fixedDeltaTime); 
        
        //This will rotate the camera holder and anything that's a child to it
        transform.localRotation = Quaternion.Euler(m_cameraHolderRotation);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the game is not paused and if there is a weapon
        if (PauseMenu.IsGamePaused == false && GetComponentInChildren<WeaponBase>() != null)
        {
            //These 2 checks are different, so that it can suit both semi-automatic and fully automatic rifles
            //If the player shoots an automatic rifle and if it has ammo loaded, it will move the camera holder up and pick a random range between the y and z (side and tilt) from negative to positive to rotate to 
            if (Input.GetButton("Fire1") && GetComponentInChildren<WeaponBase>().CurrentAmmo > 0 && GetComponentInChildren<WeaponBase>().IsAutomatic == true)
            {
                m_currentCameraHolderRotation += new Vector3(-RecoilRotationMaxRange.x, Random.Range(-RecoilRotationMaxRange.y, RecoilRotationMaxRange.y), Random.Range(-RecoilRotationMaxRange.z, RecoilRotationMaxRange.z));
            }

            //If the player shoots a sem-automatic rifle and if it has ammo loaded, it will do the same as the automatic weapon, but only once
            if (Input.GetButtonDown("Fire1") && GetComponentInChildren<WeaponBase>().CurrentAmmo > 0 && GetComponentInChildren<WeaponBase>().IsAutomatic == false)
            {
                m_currentCameraHolderRotation += new Vector3(-RecoilRotationMaxRange.x, Random.Range(-RecoilRotationMaxRange.y, RecoilRotationMaxRange.y), Random.Range(-RecoilRotationMaxRange.z, RecoilRotationMaxRange.z));
            }
        }
    }
}
