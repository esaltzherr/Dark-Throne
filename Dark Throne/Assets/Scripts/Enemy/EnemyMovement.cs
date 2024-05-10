using System.Collections;
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


    //execute animation is by default backwards
    public int executeDirection = -1;

    private float timer = 0;

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
        if (targetPlayer != null  && !shouldStopFollowing || !targetPlayer.GetComponent<MeleeCombat>().inExecuteAnimation)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
            if (distanceToPlayer <= detectionRange)
            {
                Vector2 moveDirection = (targetPlayer.position - transform.position).normalized;

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
        if (!targetPlayer.GetComponent<MeleeCombat>().inExecuteAnimation)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(attackHitbox());
            /*Collider2D[] playerHit = Physics2D.OverlapCircleAll(hitBox.position, attackrange);
            foreach (Collider2D collider in playerHit)
            {
                if (collider.CompareTag("Player"))
                {
                    GameObject player = collider.gameObject;
                    player.GetComponent<PlayerHealth2>().ChangeHealth(-10);
                }
            }
            */
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
                player.GetComponent<PlayerHealth2>().ChangeHealth(-10);
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
