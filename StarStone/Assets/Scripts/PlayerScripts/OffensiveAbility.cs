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
    public float force = 700f; // This sets the amount of force that the objects within the radius will experience
    public float nukeDamage; // This sets the damage that the nuke has on enemies

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This checks whether the player has pressed the offensive ability key
        if (Input.GetKeyDown(KeyCode.O))
        {
                Explode(); // This runs the nuke function
        }
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // This puts all of the objects affected by the nuke into an array
        
        // For each object in the array, the nuke will apply a force to
        foreach (Collider nearbyObject in colliders)
        {
            
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>(); // This gets all of the rigidbodies that the nuke affects
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius); // This applies the force to the objects in the array
                //GetComponent<EnemyBase>().EnemyDamaged(nukeDamage, 0);
            }
        }
    }
}