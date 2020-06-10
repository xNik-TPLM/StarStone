using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthElementalEnemy : EnemyBase
{
    public float RegeneratedHealth;

    private void RegenerateHealth()
    {
        Health += RegeneratedHealth * Time.deltaTime;
    }

    protected override void DisplayEnemyHealth()
    {
        base.DisplayEnemyHealth();
        RegenerateHealth();
    }
}
