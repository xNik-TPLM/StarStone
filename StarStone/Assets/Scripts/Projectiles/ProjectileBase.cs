using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    private float m_projectileLifeTimer;

    public float ProjectileDuration;
    public float ProjectileSpeed;
    public static float ProjectileDamage;
    
    public static int ProjectileType = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_projectileLifeTimer = ProjectileDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (ProjectileSpeed * Time.deltaTime);

        m_projectileLifeTimer -= Time.deltaTime;

        if(m_projectileLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }

        Destroy(gameObject);
    }
}
