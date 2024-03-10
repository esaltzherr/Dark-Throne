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
        currentHealth -= damage;
        animator.SetTrigger("Is_Hit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead");

        this.GetComponent<EnemyFollow>().enabled = false;
        this.enabled = false;

        //GetComponent<Collider2D>().enabled = false;
        //Destroy(gameObject);
    }
}

