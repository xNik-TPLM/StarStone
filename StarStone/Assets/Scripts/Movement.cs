using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float m_movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey ("w"))
        {
            transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * m_movementSpeed * 2f;

        }
        else if (Input.GetKey("w") && !Input.GetKey (KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * m_movementSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * m_movementSpeed;
        }
        if (Input.GetKey ("a") && !Input.GetKey ("d"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * m_movementSpeed;
        }
    }
}
