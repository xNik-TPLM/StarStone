using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Float feilds 
    //These 2 floats will detect the mouse movement on the x and y axis
    private float m_mouseMovementX;
    private float m_mouseMovementY;

    private float m_recoilUp;
    private float m_recoilSide;

    public float RecoilSpeed;

    //This will rotate the camera only on the x axis, but will be used to move the camera up down as well
    public float m_cameraRotationY; 
    
    //Properties
    //Float property which will control the mouse sensitivity
    public float m_mouseSensitivity = 200f;

    //Reference to the player character
    public Transform Player;

    public float m_recoilReturnPosition;

    private bool m_recoilReturned;

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
        m_mouseMovementX = m_recoilSide + Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        m_mouseMovementY = m_recoilUp + Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        //Move the camera up and down
        m_cameraRotationY -= m_mouseMovementY; //Decrement the camera rotation x by mouse movement on y axis. If it was incrementing then the camera would flip
        m_cameraRotationY = Mathf.Clamp(m_cameraRotationY, -90f, 90f); //Clamp the rotation of the camera
        transform.localRotation = Quaternion.Euler(m_cameraRotationY, 0f, 0f); //Rotate the camera up and down

        //Rotate the player and camera on the x axis
        Player.Rotate(Vector3.up * m_mouseMovementX);

        m_recoilSide -=  RecoilSpeed * Time.deltaTime;
        m_recoilUp -= RecoilSpeed * Time.deltaTime;

        if (m_recoilSide < 0)
        {
            m_recoilSide = 0;
        }

        if (m_recoilUp < 0)
        {
            m_recoilUp = 0;

            if(m_cameraRotationY != m_recoilReturnPosition && m_recoilReturned == false)
            {
                m_cameraRotationY = Mathf.MoveTowards(m_cameraRotationY, m_recoilReturnPosition, Time.deltaTime *10);
            }
            else if (m_cameraRotationY == m_recoilReturnPosition)
            {
                m_recoilReturned = true;
            }
        }
    }

    public void WeaponRecoil(float recoilSide, float recoilUp)
    {
        m_recoilSide += recoilSide;
        m_recoilUp += recoilUp;
        m_recoilReturnPosition = m_cameraRotationY;
        m_recoilReturned = false;
    }
}
