using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform targetPlayer;
    public float attackRange = 2.0f;
    private Animator animator;
    public float attackRate = 4f;
    float nextAttackTime = 0f;
    public Transform hitBox;
    public float attackrange = 0.5f;


    void Start()
    {
        animator = GetComponent<Animator>();
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
                player.GetComponent<PlayerHealth2>().ChangeHealth(-1);
            }
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
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
