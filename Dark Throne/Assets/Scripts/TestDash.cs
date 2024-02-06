using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDash : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = true;

    public float movementSpeed = 7f; // Adjustable movement speed
    public float jumpForce = 14f;    // Adjustable jump force
    public float dashSpeed = 5f;
    public float dashDuration = 0.2f;
    public float dashCD = 1f;

    bool isDashing = false;
    private float lastDashTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (Input.GetButton("Fire3") && Input.GetKey(KeyCode.A) && !isDashing)
        {
            Debug.Log("Trying to dash Left");
            // Dash Left
            DashLeft();
        }
        if (Input.GetButton("Fire3") && Input.GetKey(KeyCode.D) && !isDashing)
        {
            Debug.Log("Trying to dash Right");
            // Dash Right 
            DashRight();
        }
    }

    void DashLeft()
    {
        isDashing = true;
        lastDashTime = Time.time;

        // Dash to the left
        rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
        Invoke("StopDash", dashDuration);
    }
    void DashRight()
    {
        isDashing = true;
        lastDashTime = Time.time;

        // Dash to the right
        rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        Invoke("StopDash", dashDuration);
    }

    void StopDash()
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }
}
