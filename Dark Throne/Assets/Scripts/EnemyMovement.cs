using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float detectionRange = 10f;
    public Transform targetPlayer;
    private Rigidbody2D rb;
    public Animator animator;
    public float attackrange = 0.5f;
    private bool shouldStopFollowing = false;
    public Transform hitBox;
    public float attackRate = 4f;
    float nextAttackTime = 0f;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetBool("Moving", false);
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


                if(distanceToPlayer <= 2)
                {
                    if (Time.time >= nextAttackTime)
                    {
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;

                    }

                }
                Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
                animator.SetBool("Moving", true);
                rb.MovePosition(newPosition);
            }
        }
    }
    
    private void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(hitBox.position, attackrange);
        foreach (Collider2D collider in playerHit)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                player.GetComponent<MovementDashWallJump>().ChangeHealth(10);
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
    private void OnDrawGizmosSelected()
    {
        if (hitBox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitBox.position, attackrange);
    }
}
