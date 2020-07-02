using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is a child to the enemy base.
/// This is an Earth Elemental enemy. His health regenerates over time and is weak to wind attacks
/// Worked By: Nikodem Hamrol
/// </summary>

public class EarthElementalEnemy : EnemyBase
{
    //Earth Elemental Properties
    //Float Properties
    [Header("Earth Elemental Properties")]
    public float RegeneratedHealth; //This is the amount of health points the enemy will regerenate per second

    //Override this function to add the regenerate health function
    protected override void EnemyHealth()
    {
        base.EnemyHealth();
        RegenerateHealth();
    }
    
    //This function will regenerate the enemy's health per second
    private void RegenerateHealth()
    {
        //Check if health is less than max health
        if(CurrentHealth < MaxHealth && m_isEnemyBurning == false)
        {
            //Regenerate health
            CurrentHealth += RegeneratedHealth * Time.deltaTime;
        }
    }

    //This function is overridden to deal different damage based on the elemental projectile
    public override void EnemyDamaged(float damage, int projectileType)
    {
        base.EnemyDamaged(damage, projectileType);
        switch(projectileType)
        {
            case 1:
                CurrentHealth -= damage;
                break;

            case 2:
                CurrentHealth -= damage;
                m_isEnemyBurning = true;
                break;
        }
    }
}
