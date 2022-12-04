using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : StatusBar
{
    private PlayerStats playerStats;
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerStats>();
        barTextPostfix = "HP";
    }

    private void Update()
    {
        if(value != playerStats.currentHealth)
        {
            value = playerStats.currentHealth;
            onValueChanged();
        }
        if(maxValue != playerStats.maxHealth)
        {
            maxValue = playerStats.maxHealth;
            onMaxValueChanged();
        }
    }
}
