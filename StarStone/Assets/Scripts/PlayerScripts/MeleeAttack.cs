﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject Knife;
    Animator meleeAnimation;
    public float MeleeDamage;

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
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Works");
            collision.gameObject.GetComponent<EnemyBase>().EnemyDamaged(MeleeDamage, 0);
        }
    }
}
