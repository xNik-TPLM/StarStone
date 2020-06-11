using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    //Enemy properties    
    //Float properties
    public float CurrentHealth; //This keeps track of how much health does the enemy have
    public float MaxHealth; //This sets the max health of an enemy

    public GameObject HealthBarUI;
    public Slider HealthBarSlider;

    void Start()
    {
        
    }

    void Update()
    {
        DisplayEnemyHealth();
    }

    private float ReturnHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    protected virtual void DisplayEnemyHealth()
    {
        HealthBarSlider.value = ReturnHealth();

        if (CurrentHealth < MaxHealth)
        {
            HealthBarUI.SetActive(true);
        }

        if (CurrentHealth >= MaxHealth)
        {
            HealthBarUI.SetActive(false);
            CurrentHealth = MaxHealth;
        }
    }

    public void EnemyDamaged(float projectileDamage)
    {
        CurrentHealth -= projectileDamage;
    }
}
