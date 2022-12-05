using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [Space(10)]
    [SerializeField] private ValueBar healthBar;

    private void Update()
    {
        if(healthBar != null)
        {
            if(playerStats.currentHealth != healthBar.GetValue())
            {
                healthBar.SetValue(playerStats.currentHealth);
                healthBar.OnValueChanged();
            }
            if (playerStats.maxHealth != healthBar.GetMaxValue())
            {
                healthBar.SetMaxValue(playerStats.maxHealth);
                healthBar.OnMaxValueChanged();
            }
        }
    }
}
