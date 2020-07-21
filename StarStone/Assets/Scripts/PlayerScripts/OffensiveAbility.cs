using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script handles the player's offensive ability
/// Worked By: Ben Smith
/// </summary>
public class OffensiveAbility : MonoBehaviour
{
    // Public float variables
    public float radius = 5f; //  This sets the radius at which the nuke affects
    public float nukeDamage; // This sets the damage that the nuke has on enemies
    public static float NukeCooldownTimer; // This sets the cooldown of the nuke
    public float NukeCooldownMaxTime; // This checks if the nuke can be used

    // Public bools
    public static bool NukeEnabled; // This checks whether the nuke has been used

    // Public GameObjects
    public GameObject explosionVFX; // This sets the reference to the explosion visual effect

    // Private variables
    private SoundFX m_sound; // This allows the sound effects script to be accessed

    // Start is called before the first frame update
    void Start()
    {
        m_sound = FindObjectOfType<SoundFX>(); // This sets the reference to the sound effects script
        NukeCooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // This checks whether the player has pressed the offensive ability key
        if (Input.GetButtonDown("Offensive Ability") && NukeEnabled == false)
        {
            NukeCooldownTimer = 0; // This resets the nuke timer
            NukeEnabled = true; // This indicates the nuke has been used
            m_sound.Explosion.Play(); // This plays the explosion sound effect
            Explode(); // This calls the Explode function
        }

        NukeCooldown(); // This calls the NukeCooldown function
    }

    // This sets the nuke cooldown so it can only be used every set time period
    private void NukeCooldown()
    {
        if (NukeEnabled == true)
        {
            NukeCooldownTimer += Time.deltaTime;

            if (NukeCooldownTimer > NukeCooldownMaxTime)
            {
                NukeEnabled = false;
            }
        }
    }

    // This causes an explosion to occur around the player
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // This puts all of the objects affected by the nuke into an array
        Instantiate(explosionVFX, transform.position, transform.rotation); // This plays the explosion visual effect on the player

        // For each object in the array, the nuke will apply a force to
        foreach (Collider nearbyObject in colliders)
        {
            EnemyBase enemy = nearbyObject.GetComponent<EnemyBase>();

            if (enemy != null)
            {
                enemy.EnemyDamaged(nukeDamage, 0);
            }
        }
    }
}