using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public CharacterController CharacterController;
    public static bool CanInteract;
    public Vector3 m_playerVelocity;
    public float m_ladderForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterController.Move(m_playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanInteract = true;
            m_playerVelocity.y = Mathf.Sqrt(m_ladderForce * 60f * 4f);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanInteract = false;
        }
    }
}
