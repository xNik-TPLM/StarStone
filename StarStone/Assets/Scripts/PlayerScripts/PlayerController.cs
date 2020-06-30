using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// References: Brackeys. (2020). Unity Tutorial - How to make a HEALTH BAR in Unity! [online]. Available: https://www.youtube.com/watch?v=BLfNP4Sc_iA [Last Accessed 17th June 2020].
/// This script handles the player's movement around the map and damage source
/// Worked By: Ben Smith
/// </summary>
public class PlayerController : MonoBehaviour
{
    //Private fields
    //This bool will check if the player is on the ground
    private bool m_isGrounded;
    private bool m_ladderCollision;

    //public static bool meleeActive; //This bool checks whether the player is melee attacking
    public GameObject Knife; //This sets the reference to the melee weapon
    public GameObject playerShield;

    //Float fields
    //These floats will get the reference of axis of where the player will move.
    private float m_moveInputX;
    private float m_moveInputZ;

    //This is to set velocity of the player moving around
    public Vector3 m_playerVelocity;

    private bool ladderBottom; //Collider to check if the player has entered to move up ladder
    private bool ladderTop; //Collider to check if the player has entered to move down ladder
    public float ladderSpeed; //Speed of the player moving on the ladder

    //Player properties
    //Float properties
    public float PlayerMovementSpeed; //Speed of the player movement
    public float PlayerJumpForce; //The force of Player's jump
    public float PlayerGravityForce; //The gravity force of the player
    public float GroundCheckRadius = 0.4f; //The radius to check if the player's still on the ground
    public int maxHealth = 100;
    public int currentHealth;
    public int damage;

    //Transform properties
    public Transform PlayerFeetPosition; //Position of the player's feet to check if player is grounded

    //Unity properties
    public CharacterController CharacterController; //Reference to the character controller for movement and changing height of the collider
    public GameObject Player; //Reference to the palyer model
    public LayerMask GroundType; //The layer in the scene, which is used to check if the player is on the ground


    public static Transform KnifeStartPosition;


    //Only for WhiteBox scene
    //private float m_playerNormalHeight;
    //private float m_playerCrouchHeight;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        //m_playerCrouchHeight = Player.transform.localScale.y / 2;
        //m_playerNormalHeight = Player.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove(); // This activates the player's movement
        PlayerJump(); // This activates the player's jump
        PlayerSprint(); // This activates the player's sprint
        PlayerCrouch(); // This activates the player's crouch
        PlayerLadder(); // This activates the player's movement on the ladder
        Melee(); // This activates the player's melee attack
        ShieldActive(); // This checks if the player's shield is active
    }

    // Once the player presses the attack key, The knife will be enabled
    private void Melee()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Knife.SetActive(true); // This enables the knife
        }
    }

    // If the player uses the defensive ability, the player's shield will be enabled
    private void ShieldActive()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerShield.SetActive(true); // This enables the player's shield
        }
    }


    //This checks if the player has collided with the ladder
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("LadderBottom"))
        {
            // Once the player uses the 'use' key, the player will automatically move up or down the ladder relative to where they are once activated
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_ladderCollision = true; // This shows when the player is on/using the ladder
                ladderBottom = true; // This checks if the player is at the bottom of the ladder so the player can move in the right direction
            } 
        }
        else
        {
            ladderBottom = false;
        }
        if (collision.CompareTag("LadderTop"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ladderTop = true; // This checks if the player is at the top of the ladder so the player can move in the right direction
            }
        }
        else
        {
            ladderTop = false;
        }
        if (collision.CompareTag("StarStone"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerUI.shieldActive = true;
                Debug.Log("Works!!!!!");
                
                currentHealth -= damage;
                //healthBar.SetHealth(currentHealth);
                
            }
        }
    }
    // Once the player is no longer on/using the ladder, they can longer press the 'use' key
    private void OnTriggerExit(Collider collision)
    {
        m_ladderCollision = false;
    }

    //This function controls the ladder climbing of the player
    private void PlayerLadder()
    {
        if (ladderBottom == true && ladderTop == false)
        {
            PlayerGravityForce = 0;
            transform.Translate(Vector3.up * ladderSpeed * Time.deltaTime); // This moves the player up the ladder
        }
        if (ladderTop == true && ladderBottom == false && transform.position.x >= 17.5f)
        {
            PlayerGravityForce = 0;
            transform.Translate(Vector3.up * -ladderSpeed * Time.deltaTime); // This moves the player down the ladder
            if (transform.position.x > 18.2f)
            {
                transform.Translate(Vector3.forward * ladderSpeed * Time.deltaTime); // This makes sure the player is on the ladder before using it
            }
        }
        if (transform.position.y >= 10.0f)
        {
            ladderBottom = false;
        }
        if (transform.position.y <= 2.0f)
        {
            ladderTop = false;
        }
        if (ladderBottom == false && ladderTop == false)
        {
            PlayerGravityForce = -9.81f;
        }
    }

    //This function controls movement of the player
    private void PlayerMove()
    {
        if (m_ladderCollision == false)
        {
            m_moveInputX = Input.GetAxis("Horizontal"); // This allows the player to move horizontally with key presses
            m_moveInputZ = Input.GetAxis("Vertical"); // This allows the player to move vertically with key presses
        }
            Vector3 move = transform.right * m_moveInputX + transform.forward * m_moveInputZ; //  This sets a reference to the key presses so the player moves in the corresponding direction
            CharacterController.Move(move * PlayerMovementSpeed * Time.deltaTime); // This sets the movement speed of the player when moving
            CharacterController.Move(m_playerVelocity * Time.deltaTime);  // This sets the speed of the player when jumping
    }

    //This function controls the jumping of the player
    private void PlayerJump()
    {
        m_isGrounded = Physics.CheckSphere(PlayerFeetPosition.position, GroundCheckRadius, GroundType); // This checks if the player is on the ground

        if (m_isGrounded && m_playerVelocity.y < 0)
        {
            m_playerVelocity.y = PlayerGravityForce; //  This applies the gravity to the player if they are not jumping
        }

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_playerVelocity.y = Mathf.Sqrt(PlayerJumpForce * -2f * PlayerGravityForce); // This applies a force that moves the player upwards when jumping
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
            CharacterController.height = 0.02f; // This makes the player shorter if they are crouching
            //Player.transform.localScale = new Vector3(Player.transform.localScale.x, m_playerCrouchHeight, Player.transform.localScale.z);

        }
        else
        {
            CharacterController.height = 0.063f; // This returns the player to their normal height if they are not crouching
            //Player.transform.localScale = new Vector3(Player.transform.localScale.x, m_playerNormalHeight, Player.transform.localScale.z);
        }
    }
}