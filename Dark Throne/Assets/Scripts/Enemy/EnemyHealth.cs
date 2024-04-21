using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (this.tag == "FlyingEnemy")
        {
            Die();
        }
        else
        {
            currentHealth -= damage;
            animator.SetTrigger("Is_Hit");
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        if (this.tag == "FlyingEnemy")
        {
            this.GetComponent<EnemyAttack>().enabled = false;
            this.GetComponent<flying_enemy>().enabled = false;
            Rigidbody2D r = GetComponent<Rigidbody2D>();
            r.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Destroy(this.gameObject, 1f);

        }
        else
        {
            this.GetComponent<EnemyFollow>().enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

            Destroy(this.gameObject, 7f);
        }
        //GetComponent<Collider2D>().enabled = false;
        //Destroy(gameObject);
    }
}

