using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Global/Player Stats")]
public class PlayerStats : MonoBehaviour
{
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
    
}
