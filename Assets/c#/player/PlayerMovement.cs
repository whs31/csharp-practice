using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/Movement")]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;


    private float horizontalSpeedLimit;
    private float verticalSpeedLimit;
    private float movementSpeed;
    private float jumpForce;

    private float horizontalSpeed;
    private float verticalSpeed;
    private bool flipState = false;
    private bool isGrounded = false;

    private PlayerStats playerStats;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerStats>();
        jumpForce = playerStats.jumpForce;
        movementSpeed = playerStats.movementSpeed;
        horizontalSpeedLimit = playerStats.verticalSpeedLimit;
        verticalSpeedLimit = playerStats.verticalSpeedLimit;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float x_axis = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("horizontalSpeed", x_axis);
        animator.SetFloat("verticalSpeed", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        if (x_axis != 0)
        {
            MoveHorizontal(x_axis);
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.02f, groundLayer);
        float jump_axis = Input.GetAxisRaw("Jump");
        if (jump_axis != 0 && isGrounded)
        {
            Jump();
        }
    }

    private void MoveHorizontal(float x)
    {
        horizontalSpeed = x * movementSpeed;
        rb.velocity = new Vector2 (horizontalSpeed, rb.velocity.y);
        if(x < 0 && !flipState || x > 0 && flipState)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        flipState = !flipState;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
