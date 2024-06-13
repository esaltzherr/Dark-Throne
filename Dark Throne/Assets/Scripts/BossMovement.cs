using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float runSpeed = 7f;
    private float walkSpeed = 10f;
    public float detectionRange = 10f;
    public float closeRange = 3f;
    public Transform targetPlayer;
    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 moveDirection;
    private float speed;
    private bool shouldMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindPlayer();
    }

    private void FindPlayer()
    {
        if (targetPlayer == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                targetPlayer = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("BossMovement: Player not found. Please assign a Player tag to the player object.");
            }
        }
    }

    void Update()
    {
        animator.SetBool("Moving", false);
        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
            moveDirection = (targetPlayer.position - transform.position).normalized;

            // Flip the boss to face the player
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

            // Determine speed and whether the boss should move
            speed = distanceToPlayer > closeRange ? runSpeed : walkSpeed;
            shouldMove = true;
            animator.SetBool("Moving", true);
        }
        else
        {
            shouldMove = false;
        }
    }

    void FixedUpdate()
    {
        if (shouldMove)
        {
            Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Take damage
        }
    }
}
