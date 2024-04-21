using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDash : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = true;

    public float movementSpeed = 7f; // Adjustable movement speed
    public float jumpForce = 14f;    // Adjustable jump force
    public float dashSpeed = 2f;
    public float dashDuration = 0.2f;
    public float dashCD = 2f;

    bool isDashing = false;
    // private bool canDash;

    [SerializeField] private TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return;
        }
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (Input.GetButtonDown("Fire3") && Input.GetKey(KeyCode.A) && !isDashing)
        {
            Debug.Log("Trying to dash Left");
            // Dash Left
            StartCoroutine(DashLeft());
        }
        if (Input.GetKey(KeyCode.D) && Input.GetButtonDown("Fire3") && !isDashing)
        {
            Debug.Log("Trying to dash Right");
            // Dash Right 
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }
    private IEnumerator Dash()
    {
        // canDash = false;
        isDashing = true;
        float startGrav = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        rb.gravityScale = startGrav;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        // canDash = true;
    }
    private IEnumerator DashLeft()
    {
        // canDash = false;
        isDashing = true;
        float startGrav = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed * -1, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        rb.gravityScale = startGrav;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        // canDash = true;
    }
}
