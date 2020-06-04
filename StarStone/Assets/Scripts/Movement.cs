using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float m_movementSpeed;
    public float m_mouseSensitivity = 200f;
    public Transform Player;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        }
        else if (Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_movementSpeed;
        }
        else if (Input.GetKey("a"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_movementSpeed;
        }
        if (Input.GetKey("w") && !Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed * 2f;
        }
        else if (Input.GetKey("s") && !Input.GetKey("w"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        }
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
        Player.Rotate(Vector3.forward * -mouseY);

        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10);
        }
    }
}
