using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public float Health;
    public float MaxHealth;

    public GameObject HealthBarUI;
    public Slider HealthBar;

    void Start()
    {
        Health = MaxHealth;

        HealthBar.value = SetHealth();
    }

    void Update()
    {
        HealthBar.value = SetHealth();

        if(Health < MaxHealth)
        {
            HealthBarUI.SetActive(true);
        }

        if(Health <= 0)
        {
            Destroy(gameObject);
        }

        /*if(Health > MaxHealth)
        {
            HealthBarUI.SetActive(false);
        }*/
    }

    float SetHealth()
    {
        return Health / MaxHealth;
    }
}
