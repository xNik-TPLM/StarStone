using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [Tooltip("This is the time difference between dialogues")]
    public float TimeDifferenceBetweenDialogues; //This is the time betweeen the current dialogue text and the next one
    [Tooltip("This array stores all of the dialogue that needs to be displayed")]
    public string[] Dialogue; //This array stores all of the dialogue to be displayed, which will be put into the queue
    [Tooltip("This the text object, which will display the dialogue")]
    public Text DialogueText; //The text object will display the dialogue onto the screen

    //All of these game objects, which are groups of text elements, which will be used to display and hide them
    [Header("Pop-up Objects")]
    [Tooltip("This is an object that is used to display the Camera tutorial")]
    public GameObject CameraMovementPopUp;
    [Tooltip("This is an object that is used to display the Movement tutorial")]
    public GameObject PlayerMovementPopUp;
    [Tooltip("This is an object that is used to display the Advanced Movement tutorial")]
    public GameObject PlayerAdvancedMovementPopUp;
    [Tooltip("This is an object that is used to display any Single Lined tutorial")]
    public GameObject PlayerSingleLinedPopUp;    

    //All of these text elements, will be used to either change colour
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

    //This text element is used to change its text as its just for a single text
    [Tooltip("Text that shows single lined controls")]    
    public Text TextSingleLinedTutorial;

    //This string array is used to change the single lined text element
    [Tooltip("This is the array that stores tutorials for single lined tutorials")]
    public string[] SingleTextPopUpTexts;

    [Header("Spawning Test Dummy Properties")]
    [Tooltip("The test dummy prefab to spawn")]
    public GameObject TestDummy; //This is a test dummy that will spawn on the scene, instead of a normal enemy
    [Tooltip("The location where the test dummy will spawn")]
    public GameObject SpawnPoint; //This game object will be used to give the location to spawn the test dummy

    [Header("Weapon Holder")]
    [Tooltip("The player's weapon holder")]
    public GameObject WeaponHolder; //This is to show the weapon holder once it reaches the shooting tutorial

    // Start is called before the first frame update
    void Start()
    {
        //Set all weapon ammo to 0
        FindObjectOfType<WeaponBase>().CurrentAmmo = 0;

        //Set that the player is in the tutorial map
        InTutorialScene = true;

        //Set the tutorial checks to false
        HasCameraMoved = false;
        HasEnemyDied = false;

        //Enable dialogue
        m_dialogueEnabled = true;

        //Create a new queue of dialogue and clear the placeholder text
        m_dialogues = new Queue<string>();
        m_dialogues.Clear();

        //Enqueue every dialogue in order
        foreach(string dialogue in Dialogue)
        {
            m_dialogues.Enqueue(dialogue);
        }

        //Set the first dialogue on to the screen
        SetFirstDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is in the tutorial screen
        if (InTutorialScene == true)
        {
            //This switch will be based on dialogue that's currently displayed on screen
            switch (CurrentDialogue)
            {
                //On first dialogue, hide the weapon holder
                case "Morning soldier. Welcome to this training facility, where we will test your capabilities with your new advancements.":
                    WeaponHolder.SetActive(false);
                    break;

                case "Please could you look around for me":
                    //Disable the dialogue and display the camera tutorial
                    m_dialogueEnabled = false;
                    CameraMovementPopUp.SetActive(true);

                    //If the camera has moved, it will enabled the dialogue, hide the camera tutorial and skip to the next dialogue, by setting the current time equal to the time difference between dialogues
                    if (HasCameraMoved == true)
                    {
                        m_dialogueEnabled = true;
                        CameraMovementPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Good. Now we need to test your leg work, could you start walking around?":

                    //Start checking for the player movement, disable dialogue and display the movement tutorial
                    CheckPlayerMovement();
                    m_dialogueEnabled = false;
                    PlayerMovementPopUp.SetActive(true);

                    //If the movement tutorial is completed, it will enable the dialogue, hide the movement tutorial and skip to the next dialogue
                    if (m_movementCompleted == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerMovementPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Great. How good are you with running, jumping, and crouching?":

                    //Start checking for the advanced movement, disable dialogue and display advanced movement tutorial
                    CheckPlayerAdvancedMovement();
                    m_dialogueEnabled = false;
                    PlayerAdvancedMovementPopUp.SetActive(true);

                    //If the advanced movement tutorial is completed, it will enable the dialogue, hide the advanced movement tutorial and skip to the next dialogue
                    if (m_advancedMovementCompleted == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerAdvancedMovementPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Now load your weapon, either your rifle or the pistol and shoot the test dummy, or use your knife on it.":

                    //Show the weapon holder, start checking if shooting tutorial is completed, spawn a test dummy and disable dialogue
                    WeaponHolder.SetActive(true);
                    CheckPlayerReloading();
                    SpawnTestDummy();
                    m_dialogueEnabled = false;

                    //If the test dummy has been destroyed, enable dialogue, set amount of enemies on map to 0.
                    //Reset the enemy has died boolean back to false and increment the text array index of the single lined tutorials, to display the next single lined tutorial and skip dialogue
                    if (HasEnemyDied == true)
                    {
                        m_dialogueEnabled = true;
                        m_amountOfEnemiesToSpawn = 0;
                        HasEnemyDied = false;
                        m_indexText += 1;
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Go ahead and use it on this test dummy.":

                    //Disable the dialogue, display the single lined tutorial, which will go throught he nuke ability, and spawn a test dummy
                    m_dialogueEnabled = false;
                    PlayerSingleLinedPopUp.SetActive(true);
                    SpawnTestDummy();

                    //If test dummy has been detroyed, enable dilaogue, hide the single lined tutorial, set the amount of enemeis on map to 0 and reset that enemy has died.
                    //Increment the index of single lined tutorial and skip dilaogue
                    if (HasEnemyDied == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerSingleLinedPopUp.SetActive(false);
                        m_amountOfEnemiesToSpawn = 0;
                        HasEnemyDied = false;
                        m_indexText += 1;
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;

                case "Can you activate it?":

                    //Disable the dilaogue, display the shield ability tutorial, check if the tutorial has been completed
                    m_dialogueEnabled = false;
                    PlayerSingleLinedPopUp.SetActive(true);
                    CheckShieldActive();

                    //If shield tutorial is completed, enable dialogue, hide the single lined tutorial and skip dialogue
                    if (m_hasShieldActivated == true)
                    {
                        m_dialogueEnabled = true;
                        PlayerSingleLinedPopUp.SetActive(false);
                        m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                    }
                    break;
            }

            NextDialogue(); //Display the next dialogue
            TextSingleLinedTutorial.text = SingleTextPopUpTexts[m_indexText]; //Display the single lined based on the index its in
        }
    }

    //This function will set the first dialogue, when the scene loads
    private void SetFirstDialogue()
    {
        //Dequeue the first dialogue and display that dialogue into text element and set it to public static string
        string dialogue = m_dialogues.Dequeue();
        DialogueText.text = dialogue;
        CurrentDialogue = dialogue;
    }

    //This function will display next dialogue and check if there's no more dialogues in the queue
    private void NextDialogue()
    {
        //Check if there's no more dialogue left in the queue, it will run the end dialogue function, which will load the main menu scene
        if(m_dialogues.Count == 0)
        {
            EndDialogue();
        }
        else //However, if there's more dialogue in the queue
        {
            //Start counting the time of display the dialogue
            m_dialogueDisplayTime += Time.deltaTime;

            //Once the dialogue reaches the the time to display the next dialogue and dialogue is still enabled
            if (m_dialogueDisplayTime > TimeDifferenceBetweenDialogues && m_dialogueEnabled == true)
            {
                //Display the next dialogue by dequeuing it, display that dialogue and set it to the public static string and restart the timer
                string dialogue = m_dialogues.Dequeue();
                DialogueText.text = dialogue;
                CurrentDialogue = dialogue;
                m_dialogueDisplayTime = 0;
            }
        }
    }

    //This function will end the tutorial phase, by loading the main menu and setting the in tutorial to false. This will run once there's no more dialogue left
    private void EndDialogue()
    {
        SceneManager.LoadScene("GameMenu");
        InTutorialScene = false;
    }

    //This function will check if each movement key has been pressed, in order to complete the movmeent tutorial
    private void CheckPlayerMovement()
    {
        //If each WASD key has been pressed, it will change the colour of the key that has been recently pressed
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

        //Once all of the keys have been pressed, it wil completed the movement tutorial
        if(m_hasPlayerMovedForward && m_hasPlayerMovedLeft && m_hasPlayerMovedBack && m_hasPlayerMovedRight)
        {
            m_movementCompleted = true;
        }
    }

    //This function will check if each advanced movement key/s has been pressed, in order to complete the advanced movement tutorial
    private void CheckPlayerAdvancedMovement()
    {
        //Check if the playerhas sprinted, jump and crouched, for these 3 checks, it will change colour of the controls that has been pressed
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

        //Check if the player has sprinted, jumped, and crouched, which will complete the advanced movement tutorial
        if(m_hasPlayerSprinted && m_hasPlayerJumped && m_hasPlayerCrouched)
        {
            m_advancedMovementCompleted = true;
        }
    }

    //This function will check if the player has loaded their, primary or their secondary weapon, which will hide the control to relaod, which is the first part of the shooting tutorial
    private void CheckPlayerReloading()
    {
        //Check if the player has loaded their primary or seconday weapon, which will hide the tutorial and the player has loaded their weapon, to stop that pop tutorial from appearing
        if (Input.GetKeyDown(KeyCode.R) && m_hasPlayerLoadedWeapon == false && InteractStarStone.WeaponID == 0 || InteractStarStone.WeaponID == 1)
        {
            PlayerSingleLinedPopUp.SetActive(false);
            m_hasPlayerLoadedWeapon = true;
        }
        //If the player still hasn't loaded their weapon, then keep it on screen
        else if(m_hasPlayerLoadedWeapon == false)
        {
            PlayerSingleLinedPopUp.SetActive(true);
        }
    }

    //This function will check if the player has activated the shield, in order to complete the shield tutorial
    private void CheckShieldActive()
    {
        //If the has actiavted their shield, it will complete the shield tutorial
        if(Input.GetButtonDown("Defensive Ability") && m_hasShieldActivated == false)
        {
            m_hasShieldActivated = true;
        }
    }

    //This function will spawn a single test dummy
    private void SpawnTestDummy()
    {
        //if the dialogue has been disabled and there isn't a test dummy already on scene
        if(m_dialogueEnabled == false && m_amountOfEnemiesToSpawn < 1)
        {
            //Spawn a test dummy in its spawn location and add to enemies spawne don map, to prevent a new one spawning
            Instantiate(TestDummy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            m_amountOfEnemiesToSpawn += 1;
        }
    }
}
