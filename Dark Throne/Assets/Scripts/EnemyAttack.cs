using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform playerTransform;
    public float attackRange = 5.0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("IsIdle", false); 
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetBool("IsIdle", true);
        }
    }
}
