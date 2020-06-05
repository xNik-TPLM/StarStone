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
        m_moveInputX = Input.GetAxis("Horizontal");
        m_moveInputZ = Input.GetAxis("Vertical");

        m_movementDirection = transform.right * m_moveInputX + transform.forward * m_moveInputZ;


    }
}
