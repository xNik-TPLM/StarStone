using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// This script handles the health crate to grant the player more health
///// Worked By: Ben Smith
///// </summary>
public class HealthCrate : MonoBehaviour
{
    private PlayerController m_player; // Sets the reference to the player script
    public int healthCrateValue; // This sets the health value the player receives once picked up

    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<PlayerController>();
    }

    // This checks whether the player has picked up the crate
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_player.currentHealth += healthCrateValue;
            gameObject.SetActive(false); // This disables the crate
        }
    }
}