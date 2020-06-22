using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // This creates a slider that represents the character's health

    // This sets the maximum health that the character can have
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    // This sets the current health that the character has
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
