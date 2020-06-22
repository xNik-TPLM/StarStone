using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject Knife; // This sets the reference to the player knife
    Animator meleeAnimation; // This sets the reference to the melee attack animation
    public float MeleeDamage; // This is the damage of the knife per hit

    // Start is called before the first frame update
    void Start()
    {
        meleeAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // These set the melee attack animation to true or false depending on whether the player presses the attack key
        if (Input.GetKeyDown(KeyCode.Q))
        {
            meleeAnimation.SetBool("MeleeAttack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            meleeAnimation.SetBool("MeleeAttack", false);
        }
    }
    
    // This creates a collider which detects whether the knife has hit the enemy and then does damage accordingly
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Works");
            collision.gameObject.GetComponent<EnemyBase>().EnemyDamaged(MeleeDamage, 0);
        }
    }
}
