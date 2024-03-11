using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float detectionRange = 10f;
    public Transform targetPlayer;
    private Rigidbody2D rb; // Reference to the enemy's Rigidbody2D

    private bool shouldStopFollowing = false; // Flag to control following behavior

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D reference
    }

    void Update()
    {
        if (targetPlayer != null && !shouldStopFollowing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
            if (distanceToPlayer <= detectionRange)
            {
                Vector2 moveDirection = (targetPlayer.position - transform.position).normalized;
                Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
                rb.MovePosition(newPosition);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shouldStopFollowing = true; // Stop following when colliding with the player
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shouldStopFollowing = false; // Resume following when no longer colliding
        }
    }
}
