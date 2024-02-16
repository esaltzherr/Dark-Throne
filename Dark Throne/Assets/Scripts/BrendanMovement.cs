// Brendan's Movement Script
// Ability to change movement speed, acceleration, deceleration, direction change multiplier
// Having the ability to change these values make the movement feel smoother and more realistic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrendanMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = true;
    public Animator animator;
    public float movementSpeed = 10f; // Adjustable movement speed
    public float acceleration = 50f;   // Adjustable acceleration, Higher number = more acceleration
    public float deceleration = 100f; // Adjustable deceleration, Higher number = less deceleration
    public float directionChangeMultiplier = 50f; // Adjustable multiplier for deceleration when changing direction
    public float jumpForce = 18f;    // Adjustable jump force
    private float currentSpeed = 0f; // Current speed of the player

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float targetSpeed = dirX * movementSpeed;

        float appliedDeceleration = (dirX * rb.velocity.x > 0) ? deceleration : deceleration * directionChangeMultiplier;

        if (dirX == 0) // If not pressing movement button
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, appliedDeceleration * Time.deltaTime); // Decelerate
        }
        else // If pressing movement button
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime); // Accelerate
        }

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        animator.SetInteger("Speed", Mathf.Abs((int)rb.velocity.x));
    }
}
