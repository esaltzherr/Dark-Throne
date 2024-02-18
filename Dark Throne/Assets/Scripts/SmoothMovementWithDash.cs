using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovementWithDash : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = false; // Start with false as we need to be grounded first
    public Animator animator;
    public float movementSpeed = 10f; // Adjustable movement speed
    public float acceleration = 50f;   // Adjustable acceleration, Higher number = more acceleration
    public float deceleration = 100f; // Adjustable deceleration, Higher number = less deceleration
    public float directionChangeMultiplier = 50f; // Adjustable multiplier for deceleration when changing direction
    public float jumpForce = 18f;    // Adjustable jump force
    public float dashSpeed = 500f;    // Adjustable dash speed
    public float dashAcceleration = 100f; // Adjustable dash acceleration
    public float dashDeceleration = 100f; // Adjustable dash deceleration
    public float dashDuration = 0.12f; // Adjustable dash duration
    public float dashCooldown = 1f; // Adjustable dash cooldown duration
    public GameObject trailRendererPrefab; // Assign the Trail Renderer prefab in the Inspector
    private float currentSpeed = 0f; // Current speed of the player
    private bool isDashing = false;  // Flag to check if player is currently dashing
    private bool isDashCooldown = false; // Flag to check if player is on dash cooldown

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

        if (Input.GetKeyDown(KeyCode.W) && canJump) // Changed to 'W' key for jumping and added check for canJump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false; // Prevent double jumping
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isDashCooldown) // Check for dashing input and not already dashing or on cooldown
        {
            StartCoroutine(Dash());
        }

        animator.SetInteger("Speed", Mathf.Abs((int)rb.velocity.x));
    }

    IEnumerator Dash()
    {
        isDashing = true;
        isDashCooldown = true;

        float originalSpeed = movementSpeed;
        float originalAcceleration = acceleration;
        float originalDeceleration = deceleration;

        movementSpeed = dashSpeed; // Set movement speed to dash speed
        acceleration = dashAcceleration; // Set acceleration to dash acceleration
        deceleration = dashDeceleration; // Set deceleration to dash deceleration

        // Instantiate the trail renderer prefab
        GameObject trail = Instantiate(trailRendererPrefab, transform.position, Quaternion.identity);
        trail.transform.parent = transform; // Parent the trail renderer to the player

        yield return new WaitForSeconds(dashDuration);

        movementSpeed = originalSpeed; // Restore original movement speed
        acceleration = originalAcceleration; // Restore original acceleration
        deceleration = originalDeceleration; // Restore original deceleration
        isDashing = false;

        // Destroy the trail renderer after dash duration
        Destroy(trail);

        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }

    // Check for collision with ground to enable jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    // Check for leaving ground to disable jumping
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
