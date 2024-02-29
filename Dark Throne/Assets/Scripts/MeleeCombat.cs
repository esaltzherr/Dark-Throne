using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public Animator animator;
    public Transform hitBox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }
    void Attack()
    {

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in enemiesHit)
        {
            Debug.Log("Enemy Hit: " + enemy.name);
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
