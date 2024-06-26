using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float detectionRange = 10f;
    public Transform targetPlayer;
    private Rigidbody2D rb;
    public Animator animator;
    public float attackrange = 0.5f;
    private bool shouldStopFollowing = false;
    public Transform hitBox;
    public float attackRate = 4f;
    float nextAttackTime = 0f;
    private Vector2 moveDirection;
    private bool shouldMove = false;

    //execute animation is by default backwards
    public int executeDirection = -1;

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
                Debug.LogWarning("CameraFollow: Player not found. Please assign a Player tag to the player object.");
            }
        }
    }

    void Update()
    {
        animator.SetBool("Moving", false);
        if (targetPlayer != null && !shouldStopFollowing && !targetPlayer.GetComponent<MeleeCombat>().inExecuteAnimation)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
            if (distanceToPlayer <= detectionRange)
            {
                moveDirection = (targetPlayer.position - transform.position).normalized;

                // Flip the enemy to face the player
                if (moveDirection.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    executeDirection = -1;
                }
                else if (moveDirection.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    executeDirection = 1;
                }
                animator.SetBool("Moving", true);
                shouldMove = true;

                if (distanceToPlayer <= 2 && Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else
            {
                shouldMove = false;
            }
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
            Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    private void Attack()
    {
        if (!targetPlayer.GetComponent<MeleeCombat>().inExecuteAnimation)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(attackHitbox());
        }
    }

    IEnumerator attackHitbox()
    {
        yield return new WaitForSeconds(0.25f);
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(hitBox.position, attackrange);
        foreach (Collider2D collider in playerHit)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth2>().ChangeHealth(-1);
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
