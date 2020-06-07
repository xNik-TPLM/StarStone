using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController m_characterController;
    public float m_movementSpeed;
    public float m_gravity = -9.81f;
    public float m_jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -9.81f;
        }

        // This controls the movement of the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        m_characterController.Move(move * m_movementSpeed * Time.deltaTime);

        // This allows the player to jump by adding a vertical force
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
        }

        // This sets the gravity on the player
        velocity.y += m_gravity * Time.deltaTime;
        m_characterController.Move(velocity * Time.deltaTime);

        // This allows the player to sprint
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_movementSpeed * 2f;
        }

        // This allows the player to crouch
        if (Input.GetKey(KeyCode.C))
        {
            m_characterController.height = 0.02f;
        }
        else
        {
            m_characterController.height = 0.07f;
        }
    }
}
