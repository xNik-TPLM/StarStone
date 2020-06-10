using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public float Health;
    public float MaxHealth;

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
        return Health / MaxHealth;
    }

    protected virtual void DisplayEnemyHealth()
    {
        HealthBarSlider.value = ReturnHealth();

        if (Health < MaxHealth)
        {
            HealthBarUI.SetActive(true);
        }

        if (Health >= MaxHealth)
        {
            HealthBarUI.SetActive(false);
            Health = MaxHealth;
        }
    }
}
