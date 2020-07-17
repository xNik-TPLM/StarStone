using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : EnemyBase
{
    private bool m_testDummyHit;
    private float m_healthRegenerationWaitTime;

    public float RegeneratedHealth;

    // Start is called before the first frame update
    protected override void Start()
    {
        CurrentHealth = MaxHealth;
    }

    protected override void EnemyHealth()
    {
        base.EnemyHealth();

        if(CurrentHealth <= 0)
        {
            m_testDummyHit = true;
            TutorialController.HasEnemyDied = true;
            RegenerateHealth();
        }
    }

    protected override void EnemyBehaviour()
    {
    }

    private void RegenerateHealth()
    {
        if(m_testDummyHit == true)
        {
            m_healthRegenerationWaitTime += Time.deltaTime;

            if(m_healthRegenerationWaitTime > 1)
            {
                CurrentHealth += RegeneratedHealth * Time.deltaTime;
            }
        }
    }
}
