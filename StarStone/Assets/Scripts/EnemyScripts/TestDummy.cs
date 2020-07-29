using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a child to the enemy base.
/// This is a test dummy that is used inside the tutorial scene. It takes away some of the enemy base functions, in order to stop movement, for example
/// </summary>

public class TestDummy : EnemyBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //Set the health
        m_enemyCurrentHealth = MaxHealth;
    }

    //This function is overridden to add an extra part on the death, which will set that the enemy has died
    protected override void EnemyHealth()
    {
        base.EnemyHealth();

        //If the enemy's health is less than or equal to 0, then not only it will destroy the test dummy, it will set that bool true, which will continue the tutorial and dialogue
        if (m_enemyCurrentHealth <= 0)
        {
            TutorialController.HasEnemyDied = true;
        }
    }

    //This function is overridden so that thg test dummy can do nothing
    protected override void EnemyBehaviour()
    {
    }
}
