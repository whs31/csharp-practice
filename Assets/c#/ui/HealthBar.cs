using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class HealthBar : StatusBar
{
    [SerializeField] private string textPostfix = "HP";
    private PlayerStats playerStats;
    private void Start()
    {
        barTextPostfix = textPostfix;
        playerStats = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerStats>();
        textPrefferedWidth = barText.preferredWidth;
    }

    private void Update()
    {
        if(value != playerStats.currentHealth)
        {
            if(value > playerStats.currentHealth)
            {
                Fade();
            }
            value = playerStats.currentHealth;
            onValueChanged();
        }
        if(maxValue != playerStats.maxHealth)
        {
            maxValue = playerStats.maxHealth;
            onMaxValueChanged();
        }
        UpdateFader();
    }
}
