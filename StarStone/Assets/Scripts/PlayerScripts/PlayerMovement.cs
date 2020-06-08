using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool m_isGrounded;

    private float m_moveInputX;
    private float m_moveInputZ;

    private Vector3 m_playerVelocity;

    public float PlayerMovementSpeed;
    public float PlayerJumpForce;
    public float PlayerGravityForce;

    public GameObject Player;






    public CharacterController m_characterController;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerSprint();
        PlayerCrouch();
    }

    private void PlayerMove()
    {
        m_moveInputX = Input.GetAxis("Horizontal");
        m_moveInputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * m_moveInputX + transform.forward * m_moveInputZ;
        m_characterController.Move(move * PlayerMovementSpeed * Time.deltaTime);
        m_characterController.Move(m_playerVelocity * Time.deltaTime);
    }

    private void PlayerJump()
    {
        m_isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (m_isGrounded && m_playerVelocity.y < 0)
        {
            m_playerVelocity.y = PlayerGravityForce;
        }

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_playerVelocity.y = Mathf.Sqrt(PlayerJumpForce * -2f * PlayerGravityForce);
        }

        m_playerVelocity.y += PlayerGravityForce * Time.deltaTime;
    }

    private void PlayerSprint()
    {
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * PlayerMovementSpeed * 2f;
        }
    }

    private void PlayerCrouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            m_characterController.height = 0.02f;
            Player.transform.localScale = new Vector3(40f, 30f, 100f);
        }
        else
        {
            Player.transform.localScale = new Vector3(40f, 60f, 100f);
            m_characterController.height = 0.07f;
        }
    }
}
