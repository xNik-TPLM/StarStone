using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles camera movement and recoil that is controlled using the camera
/// Worked By: Ben Smith & Nikodem Hamrol
/// </summary>

public class CameraMovement : MonoBehaviour
{
    //Float feilds 
    //These 2 floats will detect the mouse movement on the x and y axis
    private float m_mouseMovementX;
    private float m_mouseMovementY;

    //This will rotate the camera only on the x axis, but will be used to move the camera up down as well
    public float m_cameraRotationY; 
    
    //Properties
    //Float property which will control the mouse sensitivity
    public float m_mouseSensitivity = 200f;

    //Reference to the player character
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        //Lock the cursor the cursor to the center and it will hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the mouse axis so it can move and set sensitivty to it
        m_mouseMovementX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        m_mouseMovementY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        //Move the camera up and down
        m_cameraRotationY -= m_mouseMovementY; //Decrement the camera rotation x by mouse movement on y axis. If it was incrementing then the camera would flip
        m_cameraRotationY = Mathf.Clamp(m_cameraRotationY, -90f, 90f); //Clamp the rotation of the camera
        transform.localRotation = Quaternion.Euler(m_cameraRotationY, 0f, 0f); //Rotate the camera up and down

        //Rotate the player and camera on the x axis
        Player.Rotate(Vector3.up * m_mouseMovementX);
    }
}
