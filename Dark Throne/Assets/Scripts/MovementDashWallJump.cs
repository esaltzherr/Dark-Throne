using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MovementDashWallJump : MonoBehaviour
{
    //public static MovementDashWallJump instance;

    public ParticleSystem dust;
    public Animator animator;
    public string GameOver;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    private static int currentHealth = 100;
    
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool doubleJumpAvailable = false; // Added variable for double jump
    private bool jumpInputDetected = false; // Variable to track if jump input is detected
    private bool canJump = true; // Added variable to track if the player can jump
    private float jumpInputDelay = 0.1f; // Adjustable delay after landing before allowing jump input
    private float jumpCooldown = 0.2f; // Adjustable cooldown after landing before allowing jump input again

    private static bool doubleJumpPowerUp = false;
    private bool touchingDoubleJump = false;
    private bool collidingEnemy = false;    
    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] Slider health;
    [SerializeField] private Canvas powerUpCanvas;

    [SerializeField] private GameObject powerUp;

    private void Start()
    {
        powerUpCanvas = GameObject.FindGameObjectWithTag("PowerUpTag").GetComponent<Canvas>();
        powerUpCanvas.enabled = false;
        health.value = currentHealth;
    }

    void Update()
    {
        if(collidingEnemy)
        {
            ChangeHealth(10);
        }
        if (touchingDoubleJump && Input.GetKeyDown(KeyCode.E))
        {
            touchingDoubleJump = false;
            doubleJumpAvailable = true;
            powerUp.SetActive(false);
        }

        // Detect jump input
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            // shows dust particles
            CreateDust();

            jumpInputDetected = true;
            canJump = false; // Disable jumping temporarily
            StartCoroutine(EnableJump()); // Enable jumping after a short delay
        }

        // Handle jump
        if (jumpInputDetected)
        {
            if (IsGrounded() || doubleJumpAvailable)
            {
                Jump();

                if (doubleJumpPowerUp)
                {
                    doubleJumpAvailable = !doubleJumpAvailable;
                    // doubleJumped = true;
                }
                jumpInputDetected = false; // Reset jump input
            }
        }

        // Other input handling
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {   
            StopWallJumping();
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || currentHealth <= 0)
        {
            currentHealth = 2;
            SceneManager.LoadScene(GameOver);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeHealth(25);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeHealth(-25);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    private bool IsGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.3f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                // Print debug information
                // Debug.Log("Collider Name: " + collider.gameObject.name);
                // Debug.Log("Collider Tag: " + collider.tag);
                return true;
            }


        }
        return false;
    }

    private bool IsWalled()
    {
        // return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, 0.3f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                // Print debug information
                // Debug.Log("Collider Name: " + collider.gameObject.name);
                // Debug.Log("Collider Tag: " + collider.tag);
                return true;
            }


        }
        return false;
        // return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            //dust.Play();
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            Vector3 dustLocalScale = dust.transform.localScale;
            dustLocalScale.x *= -1f;
            dust.transform.localScale = dustLocalScale;

            dust.Play();
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;  

        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 0){
            horizontal = Mathf.Sign(transform.localScale.x);
        }

        rb.velocity = new Vector2(Mathf.Sign(horizontal) * Mathf.Abs(transform.localScale.x) * dashingPower, 0f);

        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(jumpInputDelay);
        canJump = true; // Enable jumping after a short delay
        yield return new WaitForSeconds(jumpCooldown); // Cooldown after landing before allowing jump input again
        jumpInputDetected = false; // Reset jump input
    }

    // Collision handling for double jump power-up
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            touchingDoubleJump = true;
            doubleJumpPowerUp = true;
            doubleJumpAvailable = true; // Enable double jump when colliding with power-up
            powerUpCanvas.enabled = true;
            //Destroy(collision.gameObject); // Destroy the power-up object
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collidingEnemy = true;
            /*if (!(GetComponent<PlayerInvulnerability>().isInvulnerable))
            {
                ChangeHealth(50);
                IEnumerator invuln = GetComponent<PlayerInvulnerability>().BecomeInvulnerable();
                StartCoroutine(invuln);
            }*/
        }
    }

    // Reset jump availability when leaving ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true; // Enable jumping when colliding with ground
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            touchingDoubleJump = false;
            powerUpCanvas.enabled = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collidingEnemy = false;
        }
    }

    public void ChangeHealth(int num)
    {
        if (!(GetComponent<PlayerInvulnerability>().isInvulnerable))
        {
            currentHealth -= num;
            health.value = currentHealth;
            animator.SetTrigger("Is_Hit");
            IEnumerator invuln = GetComponent<PlayerInvulnerability>().BecomeInvulnerable();
            StartCoroutine(invuln);
        }
        
    }

    private void CreateDust()
    {
        dust.Play();
    }

    

    
}
