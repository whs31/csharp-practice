using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    private float movementSpeed = 1f;
    private Vector2 movement;
    private Animator headAnimator;

    void Awake()
    {
        GameObject playerHead = playerStats.playerHead;
        movementSpeed = playerStats.speed;
        headAnimator = playerHead.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        movementSpeed = playerStats.speed;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movement.normalized * Time.fixedDeltaTime * movementSpeed*4);
        GetComponent<Animator>().SetFloat("VelocityX", movement.x);
        GetComponent<Animator>().SetFloat("VelocityY", movement.y);
        headAnimator.SetFloat("VelocityX", movement.x);
        headAnimator.SetFloat("VelocityY", movement.y);
        if (movement.magnitude > 0f)
        {
            GetComponent<Animator>().SetBool("isWalking", true);
            headAnimator.SetBool("isWalking", true);
        } else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
            headAnimator.SetBool("isWalking", false);
        }
    }
}
