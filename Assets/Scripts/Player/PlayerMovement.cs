using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    Vector2 movement;

    Rigidbody2D rb;
    Animator animator;

    float lastMovement;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastMovement = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // get player movement inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y != 0)
        {
            lastMovement = movement.y;
        }

        // update animator based on movement
        animator.SetFloat("Horizontal", movement.x);

        // if player is almost idle
        if (movement.sqrMagnitude < 0.01f)
        {
            // keep last direction
            animator.SetFloat("Vertical", lastMovement);
        }
        else
        {
            // update direction
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // move the player based on input and speed 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
