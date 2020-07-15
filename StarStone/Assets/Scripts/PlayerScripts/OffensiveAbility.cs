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
    //public float force = 700f; // This sets the amount of force that the objects within the radius will experience
    public float nukeDamage; // This sets the damage that the nuke has on enemies
    

    public static bool NukeEnabled;
    public static float NukeCooldownTimer;
    public float NukeCooldownMaxTime;
    private SoundFX m_sound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_sound = FindObjectOfType<SoundFX>();
    }

    // Update is called once per frame
    void Update()
    {
        // This checks whether the player has pressed the offensive ability key
        if (Input.GetButtonDown("Offensive Ability") && NukeEnabled == false)
        {
            NukeCooldownTimer = 0;
            NukeEnabled = true;
            m_sound.Explosion.Play();
            Explode();
        }

        NukeCooldown();
    }

    private void NukeCooldown()
    {
        if(NukeEnabled == true)
        {
            NukeCooldownTimer += Time.deltaTime;

            if(NukeCooldownTimer > NukeCooldownMaxTime)
            {
                NukeEnabled = false;
            }
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // This puts all of the objects affected by the nuke into an array
        
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