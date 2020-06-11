using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    //Enemy properties
    //Normal damage to the enemy
    public float NormalDamage;

    //Relating to burn damage to the enemy
    private bool m_isBurning;
    private float m_burnTimer;
    public float BurnDamage; //Burning damage
    public float BurnDuration; //Duration of burning
    
    //Float properties
    public float CurrentHealth; //This keeps track of how much health does the enemy have
    public float MaxHealth; //This sets the max health of an enemy

    public GameObject HealthBarUI;
    public Slider HealthBarSlider;


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


}
