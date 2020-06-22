﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveAbility : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    //public GameObject explosionEffect;
    float countDown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        Debug.Log("BOOM!");
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }
}
