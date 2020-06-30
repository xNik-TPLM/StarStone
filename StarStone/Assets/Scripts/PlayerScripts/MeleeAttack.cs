using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the melee attack that player uses with the knife
/// Worked By: Ben Smith
/// </summary>
public class MeleeAttack : MonoBehaviour
{
    Animator meleeAnimation; // This sets the reference to the melee attack animation
    public float MeleeDamage; // This is the damage of the knife per hit

    // Start is called before the first frame update
    void Start()
    {
        meleeAnimation = GetComponent<Animator>(); // This sets the reference for the animation
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAnimation(); // This calls the animation to run
    }

    // This creates a collider which detects whether the knife has hit the enemy and then does damage accordingly
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>().EnemyDamaged(MeleeDamage, 0); // This applies the damage to the enemy's health
        }
    }

    public void MeleeAnimation()
    {
        // These set the melee attack animation to true or false depending on whether the player presses the attack key
        meleeAnimation.SetBool("MeleeAttack", true); // This will set the animation to run

        if (meleeAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            meleeAnimation.SetBool("MeleeAttack", false); // This stops the animation from running once it has completed a cycle
            gameObject.SetActive(false); // This disables the knife from view once the animation has completed a cycle
        }
    }      
}