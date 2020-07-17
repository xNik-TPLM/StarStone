using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Worked By: Nikodem Hamrol
/// </summary>

public class TutorialController : MonoBehaviour
{
    private bool m_dialogueEnabled;

    private bool m_movementCompleted;
    private bool m_hasPlayerMovedForward;
    private bool m_hasPlayerMovedLeft;
    private bool m_hasPlayerMovedRight;
    private bool m_hasPlayerMovedBack;

    private bool m_advancedMovementCompleted;
    private bool m_hasPlayerSprinted;
    private bool m_hasPlayerJumped;
    private bool m_hasPlayerCrouched;

    private float m_dialogueDisplayTime;
    

    public static string CurrentDialogue;

    private Queue<string> m_dialogues;


    public static bool InTutorialScene;
    public static bool DisplayStarStoneIcon;
    public static bool DisplayAmmo;
    public static bool EnableWeaponHolder;
    public static bool DisplayAbilities;

    public static bool HasCameraMoved;


    public float TimeDifferenceBetweenDialogues;

    public string[] Dialogue;

    public Text DialogueText;

    [Header("Pop-up Objects")]
    public GameObject CameraMovementPopUp;
    public GameObject PlayerMovementPopUp;
    public GameObject PlayerAdvancedMovementPopUp;
    public GameObject PlayerShootingPopUp;

    [Header("Text Components")]
    public Text TextW;
    public Text TextA;
    public Text TextS;
    public Text TextD;
    public Text TextSprint;
    public Text TextJump;
    public Text TextCrouch;

    // Start is called before the first frame update
    void Start()
    {
        InTutorialScene = true;
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
        switch (CurrentDialogue)
        {
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

                if(m_movementCompleted == true)
                {
                    m_dialogueEnabled = true;
                    PlayerMovementPopUp.SetActive(false);
                    m_dialogueDisplayTime = TimeDifferenceBetweenDialogues;
                }
                break;

            case "Great work. How good are you with running, jumping, and crouching?":
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

            case "Now load your weapon, either your rifle or the pistol and shoot the enemy target.":
                CheckPlayerShooting();
                m_dialogueEnabled = false;

                break;
        }

        NextDialogue();
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

    }

}
