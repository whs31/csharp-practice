using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCaller : MonoBehaviour
{
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            playerStats.Damage(10f);
        }
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            playerStats.Heal(5f);
        }
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            playerStats.DamageOverTime(2, 0.5f, 3f);
        }
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            playerStats.ChangeMaxHP(-30);
        }
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            playerStats.ChangeMaxHP(20);
        }
    }
}
