using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public Animator animator;
    public Transform hitBox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;



    // Update is called once per frame 
    void Update()
    {
        if(Time.time >= nextAttackTime){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in enemiesHit)
        {

            Debug.Log("Enemy Hit: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(hitBox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }
}
