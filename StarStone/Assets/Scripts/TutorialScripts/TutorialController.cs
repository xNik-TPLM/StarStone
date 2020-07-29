using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is script is the controller of tutorial scene
/// Worked By: Nikodem Hamrol
/// </summary>

public class TutorialController : MonoBehaviour
{
    //Tutorial Controller fields
    //Boolean fields
    private bool m_dialogueEnabled; //This boolean is used to check if the dialogue can carry on

    //These booleans are used to check if all movement keys have been used
    private bool m_movementCompleted;
    private bool m_hasPlayerMovedForward;
    private bool m_hasPlayerMovedLeft;
    private bool m_hasPlayerMovedRight;
    private bool m_hasPlayerMovedBack;

    //These booleans are used to check if all advanced movment keys have been used
    private bool m_advancedMovementCompleted;
    private bool m_hasPlayerSprinted;
    private bool m_hasPlayerJumped;
    private bool m_hasPlayerCrouched;

    //This boolean is to check if the player has loaded on of their weapons
    private bool m_hasPlayerLoadedWeapon;

    //This boolean is to check if the player has used the shield
    private bool m_hasShieldActivated;

    //This float is the time that the dialogue will be displayed for
    private float m_dialogueDisplayTime;

    //Integer fields
    private int m_amountOfEnemiesToSpawn; //This is amount of enemies to spawn on the map
    private int m_indexText; //This is the index to change on signle pop up tutorials

    //This is the queue for the dialogue in the tutorial
    private Queue<string> m_dialogues;
    
    //Public static fields
    //Static booleans
    public static bool InTutorialScene; //This is to check if the player is the tutorial map, which is used to disable some HUD elements and other objects
    public static bool HasCameraMoved; //This is to check if the player has moved their camera
    public static bool HasEnemyDied; //This is to check if the test dummy has died

    //This static string is used as a checker for the dialogue in other scripts, like to enable certain HUD elements
    public static string CurrentDialogue;

    //Tutotial properties
    [Header("Dialogue Properties")]
    [Tooltip("This is the difference between dialogues")]
    public float TimeDifferenceBetweenDialogues; //This is the time betweeen the current dialogue text and the next one
    [Tooltip("This array stores all of the dialogue that needs to be displayed")]
    public string[] Dialogue; //This array stores all of the dialogue to be displayed, which will be put into the queue
    [Tooltip("This the text object, which will display the dialogue")]
    public Text DialogueText; //The text object will display the dialogue onto the screen

    [Header("Pop-up Objects")]
    [Tooltip("This is an object that is used to display the Camera tutorial")]
    public GameObject CameraMovementPopUp;
    [Tooltip("This is an object that is used to display the Movement tutorial")]
    public GameObject PlayerMovementPopUp;
    [Tooltip("This is an object that is used to display the Advanced Movement tutorial")]
    public GameObject PlayerAdvancedMovementPopUp;
    [Tooltip("This is an object that is used to display any Shooting tutorial")]
    public GameObject PlayerShootingPopUp;
    [Tooltip("This is an object that is used to display any Nuke tutorial")]
    public GameObject PlayerNukePopUp;
    [Tooltip("This is an object that is used to display any Shield tutorial")]
    public GameObject PlayerShieldPopUp;    
    [Tooltip("This is an object that is used to display any Single Lined tutorial")]
    public GameObject PlayerSingleLinedPopUp;    

    [Header("Text Components")]
    [Tooltip("Text that shows forward movement controls")]
    public Text TextW;
    [Tooltip("Text that shows left movement controls")]
    public Text TextA;
    [Tooltip("Text that shows backward movement controls")]
    public Text TextS;
    [Tooltip("Text that shows right movement controls")]
    public Text TextD;
    [Tooltip("Text that shows sprint controls")]
    public Text TextSprint;
    [Tooltip("Text that shows jump controls")]
    public Text TextJump;
    [Tooltip("Text that shows crouch movement")]
    public Text TextCrouch;
    [Tooltip("Text that shows single lined controls")]
    public Text TextSingleLinedTutorial;    
    [Tooltip("This is the array that stores tutorials for single lined tutorials")]
    public string[] SingleTextPopUpTexts;

    [Header("Spawning Test Dummy Properties")]
    [Tooltip("The test dummy prefab to spawn")]
    public GameObject TestDummy;
    [Tooltip("The location where the test dummy will spawn")]
    public GameObject SpawnPoint;

    [Header("Weapon Holder")]
    [Tooltip("The player's weapon holder")]
    public GameObject WeaponHolder;

    // Start is called before the first frame update
    void Start()
    {

        FindObjectOfType<WeaponBase>().CurrentAmmo = 0;

        InTutorialScene = true;
        HasCameraMoved = false;
        HasEnemyDied = false;
        m_dialogueEnabled = true;

        m_dialogues = new Queue<string>();
        m_dialogues.Clear();

        foreach(string dialogue in Dialogue)
        {
            m_dialogues.Enqueue(dialogue);
        }

        SetFirstDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (InTutorialScene == true)
        {
            switch (CurrentDialogue)
            {
                case "Morning soldier. Welcome to this training facility, where we will test your capabilities with your new advancements.":
                    WeaponHolder.SetActive(false);
                    break;

                case "Please could you look around for me":

                    m_dialogueEnabled = false;
                    CameraMovementPopUp.SetActive(true);

                    if (HasCameraMoved == true)
                    {
                        m_dialogueEnabled = true;
                        CameraMovementPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Good. Now we need to test your leg work, could you start walking around?":
                    CheckPlayerMovement();
                    m_dialogueEnabled = false;
                    PlayerMovementPopUp.SetActive(true);

                    if (m_movementCompleted == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerMovementPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Great. How good are you with running, jumping, and crouching?":
                    CheckPlayerAdvancedMovement();
                    m_dialogueEnabled = false;
                    PlayerAdvancedMovementPopUp.SetActive(true);

                    if (m_advancedMovementCompleted == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerAdvancedMovementPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Now load your weapon, either your rifle or the pistol and shoot the test dummy, or use your knife on it.":
                    m_dialogueEnabled = false;
                    WeaponHolder.SetActive(true);
                    CheckPlayerShooting();
                    SpawnTestDummy();


                    if (HasEnemyDied == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerShootingPopUp.SetActive(false);
                        m_amountOfEnemiesToSpawn = 0;
                        m_indexText += 1;
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                        HasEnemyDied = false;
                    }
                    break;

                case "Go ahead and use it on this test dummy.":
                    m_dialogueEnabled = false;
                    PlayerSingleLinedPopUp.SetActive(true);
                    SpawnTestDummy();

                    if (HasEnemyDied == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerSingleLinedPopUp.SetActive(false);
                        m_amountOfEnemiesToSpawn = 0;
                        m_indexText += 1;
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                        HasEnemyDied = false;
                    }
                    break;

                case "Can you activate it?":
                    m_dialogueEnabled = false;
                    PlayerSingleLinedPopUp.SetActive(true);
                    CheckShieldActive();

                    if (m_hasShieldActivated == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerSingleLinedPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;
            }

            NextDialogue();
            TextSingleLinedTutorial.text = SingleTextPopUpTexts[m_indexText];
        }
    }

    private void SetFirstDialogue()
    {
        string dialogue = m_dialogues.Dequeue();
        DialogueText.text = dialogue;
        CurrentDialogue = dialogue;
    }

    private void NextDialogue()
    {
        if(m_dialogues.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            m_dialogueDisplayTime += Time.deltaTime;

            if (m_dialogueDisplayTime > TimeDifferenceBetweenDialogues && m_dialogueEnabled == true)
            {
                string dialogue = m_dialogues.Dequeue();
                DialogueText.text = dialogue;
                CurrentDialogue = dialogue;
                m_dialogueDisplayTime = 0;
            }
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End");
        InTutorialScene = false;
    }

    private void CheckPlayerMovement()
    {
        if(Input.GetKeyDown(KeyCode.W) && m_hasPlayerMovedForward == false)
        {
            TextW.color = new Color(0, 255, 0);
            m_hasPlayerMovedForward = true;
        }

        if(Input.GetKeyDown(KeyCode.A) && m_hasPlayerMovedLeft == false)
        {
            TextA.color = new Color(0, 255, 0);
            m_hasPlayerMovedLeft = true;
        }

        if(Input.GetKeyDown(KeyCode.S) && m_hasPlayerMovedBack == false)
        {
            TextS.color = new Color(0, 255, 0);
            m_hasPlayerMovedBack = true;
        }

        if(Input.GetKeyDown(KeyCode.D) && m_hasPlayerMovedRight == false)
        {
            TextD.color = new Color(0, 255, 0);
            m_hasPlayerMovedRight = true;
        }

        if(m_hasPlayerMovedForward && m_hasPlayerMovedLeft && m_hasPlayerMovedBack && m_hasPlayerMovedRight)
        {
            m_movementCompleted = true;
        }
    }

    private void CheckPlayerAdvancedMovement()
    {
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && m_hasPlayerSprinted == false)
        {
            TextSprint.color = new Color(0, 255, 0);
            m_hasPlayerSprinted = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && m_hasPlayerJumped == false)
        {
            TextJump.color = new Color(0, 255, 0);
            m_hasPlayerJumped = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) == m_hasPlayerCrouched == false)
        {
            TextCrouch.color = new Color(0, 255, 0);
            m_hasPlayerCrouched = true;
        }

        if(m_hasPlayerSprinted && m_hasPlayerJumped && m_hasPlayerCrouched)
        {
            m_advancedMovementCompleted = true;
        }
    }

    private void CheckPlayerShooting()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_hasPlayerLoadedWeapon == false && InteractStarStone.WeaponID == 0 || InteractStarStone.WeaponID == 1)
        {
            PlayerSingleLinedPopUp.SetActive(false);
            m_hasPlayerLoadedWeapon = true;
        }
        else if(m_hasPlayerLoadedWeapon == false)
        {
            PlayerSingleLinedPopUp.SetActive(true);
        }
    }

    private void CheckShieldActive()
    {
        if(Input.GetButtonDown("Defensive Ability") && m_hasShieldActivated == false)
        {
            m_hasShieldActivated = true;
        }
    }

    private void SpawnTestDummy()
    {
        if(m_dialogueEnabled == false && m_amountOfEnemiesToSpawn < 1)
        {
            Instantiate(TestDummy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            m_amountOfEnemiesToSpawn += 1;
        }
    }
}
