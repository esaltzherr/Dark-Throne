using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float detectionRange = 10f;
    public Transform targetPlayer;
    private Rigidbody2D rb;

    private bool shouldStopFollowing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (targetPlayer != null && !shouldStopFollowing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
            if (distanceToPlayer <= detectionRange)
            {
                Vector2 moveDirection = (targetPlayer.position - transform.position).normalized;

                // Flip the enemy to face the player
                if (moveDirection.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (moveDirection.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
                rb.MovePosition(newPosition);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shouldStopFollowing = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shouldStopFollowing = false;
        }
    }
}
