using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveAbility : MonoBehaviour
{
    public float radius = 5f;
    public float force = 700f;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hasExploded = false;
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                
            }
        }
    }
}
