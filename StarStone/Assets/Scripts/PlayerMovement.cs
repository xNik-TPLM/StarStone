using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float m_moveInputX;
    private float m_moveInputZ;

    private Vector3 m_movementDirection;

    private CharacterController m_characterController;

    public float m_movementSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        }
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_movementSpeed * 2f;
        }
        if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_movementSpeed;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_movementSpeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        }
        //if (Input.GetKey("a") && !Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
        //{
        //    transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed * 2f;
        //}
        //if (Input.GetKey("d") && !Input.GetKey("a"))
        //{
        //    transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        //}

        //m_moveInputX = Input.GetAxis("Horizontal");
        //m_moveInputZ = Input.GetAxis("Vertical");

        //m_movementDirection = transform.right * m_moveInputX + transform.forward * m_moveInputZ;


    }
}
