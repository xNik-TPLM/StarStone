using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    Animator meleeAnimation;
    // Start is called before the first frame update
    void Start()
    {
        meleeAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            meleeAnimation.SetBool("MeleeAttack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            meleeAnimation.SetBool("MeleeAttack", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            meleeAnimation.SetBool("MeleeAttackHorizontal", true);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            meleeAnimation.SetBool("MeleeAttackHorizontal", false);
        }
    }
}
