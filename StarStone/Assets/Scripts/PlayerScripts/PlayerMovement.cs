using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Private fields
    //This bool will check if the player is on the ground
    private bool m_isGrounded;
    private bool m_ladderCollision;

    //Float fields
    //These floats will get the reference of axis of where the player will move.
    private float m_moveInputX;
    private float m_moveInputZ;

    //This is to set velocity of the player moving around
    public Vector3 m_playerVelocity;

    //Player properties
    //Float properties
    public float PlayerMovementSpeed; //Speed of the player movement
    public float PlayerJumpForce; //The force of Player's jump
    public float PlayerGravityForce; //The gravity force of the player
    public float GroundCheckRadius = 0.4f; //The radius to check if the player's still on the ground

    //Transform properties
    public Transform PlayerFeetPosition; //Position of the player's feet to check if player is grounded

    //Unity properties
    public CharacterController CharacterController; //Reference to the character controller for movement and changing height of the collider
    public GameObject Player; //Reference to the palyer model
    public LayerMask GroundType; //The layer in the scene, which is used to check if the player is on the ground


    //Only for WhiteBox scene
    //private float m_playerNormalHeight;
    //private float m_playerCrouchHeight;

    // Start is called before the first frame update
    void Start()
    {
        //m_playerCrouchHeight = Player.transform.localScale.y / 2;
        //m_playerNormalHeight = Player.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerSprint();
        PlayerCrouch();
    }

    private void OnTriggerStay(Collider collision)
    {
        m_ladderCollision = true;
        if (collision.CompareTag("LadderBottom"))
        {
            if (CharacterController.velocity.y >= 0)
            {
                m_playerVelocity.y = Mathf.Sqrt(PlayerJumpForce * -1.5f * PlayerGravityForce);
            }
            if (CharacterController.velocity.y < 0)
            {
                m_playerVelocity.y = Mathf.Sqrt(PlayerJumpForce * -200f * PlayerGravityForce);
            }

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        m_ladderCollision = false;
    }

    //This function controls movement of the player
    private void PlayerMove()
    {
        m_moveInputX = Input.GetAxis("Horizontal");
        m_moveInputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * m_moveInputX + transform.forward * m_moveInputZ;
        CharacterController.Move(move * PlayerMovementSpeed * Time.deltaTime);
        CharacterController.Move(m_playerVelocity * Time.deltaTime);
    }

    //This function controls the jumping of the player
    private void PlayerJump()
    {
        m_isGrounded = Physics.CheckSphere(PlayerFeetPosition.position, GroundCheckRadius, GroundType);

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

    //This function controls the sprint
    private void PlayerSprint()
    {
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * PlayerMovementSpeed * 2f;
        }
    }
    //This function controls the crouch
    private void PlayerCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            CharacterController.height = 0.02f;
            //Player.transform.localScale = new Vector3(Player.transform.localScale.x, m_playerCrouchHeight, Player.transform.localScale.z);

        }
        else
        {
            CharacterController.height = 0.063f;
            //Player.transform.localScale = new Vector3(Player.transform.localScale.x, m_playerNormalHeight, Player.transform.localScale.z);
        }
    }
}
