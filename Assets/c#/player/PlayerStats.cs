using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Global/Player Stats")]
public class PlayerStats : MonoBehaviour
{
    public enum DamageType
    {
        Physical,
        Magic,
        Pure
    }

    [Header("Movement Settings")]
    public float movementSpeed = 1.0f;
    public float jumpForce = 3f;
    public float verticalSpeedLimit = 3.5f;
    [Space(10)]

    [Header("Movement Settings")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [HideInInspector]
    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.Log("Player object not found");
        }
    }

    private void Update()
    {
        if(currentHealth < 0 || currentHealth > maxHealth)
        {
            if(currentHealth < 0)
            {
                currentHealth = 0;
            } else
            {
                currentHealth = maxHealth;
            }
        }
    }

    public void Damage(float amount, DamageType type = DamageType.Pure)
    {
        currentHealth -= amount;
    }

    public void DamageOverTime(float amountPerTick, float tickInterval, float duration, DamageType type = DamageType.Pure)
    {
        StartCoroutine(DOT(amountPerTick, tickInterval, duration, false, type));
    }

    private IEnumerator DOT(float amountPerTick, float interval, float duration, bool isHealing = false, DamageType type = DamageType.Pure)
    {
        float timePassed = 0;
        while (timePassed < duration)
        {
            timePassed += interval;
            if(!isHealing)
            {
                Damage(amountPerTick, type);
            } else
            {
                Heal(amountPerTick);
            }
            
            yield return new WaitForSecondsRealtime(interval);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
    }

    public void HealOverTime(float amountPerTick, float tickInterval, float duration)
    {
        StartCoroutine(DOT(amountPerTick, tickInterval, duration, true));
    }

  
    
    public void ChangeMaxHP(float delta, bool preserveRatio = true)
    {
        float ratio = currentHealth / maxHealth;
        maxHealth += delta;
        if (preserveRatio)
        {
            currentHealth = maxHealth * ratio;
        }
    }
}
