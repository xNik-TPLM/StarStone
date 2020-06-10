using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float ProjectileLifeTime;
    public float ProjectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMovement();
    }

    private void ProjectileMovement()
    {
        transform.position += transform.forward * (ProjectileSpeed * Time.deltaTime);
    }
}
