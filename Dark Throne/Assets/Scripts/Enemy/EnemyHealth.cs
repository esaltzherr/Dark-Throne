using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator animator;
    public bool isStaggering = false;
    public Transform targetPlayer;
    public int executeDirection = -1;

    public bool detected = false;
    void Start()
    {
        currentHealth = maxHealth;
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
        Vector2 moveDirection = (targetPlayer.position - transform.position).normalized;

        // Flip the enemy to face the player
        if (moveDirection.x > 0)
        {
            //player is on the right side of skeleton
            executeDirection = -1;
        }
        else if (moveDirection.x < 0)
        {
            //player is on the left side of skeleton
            executeDirection = 1;
        }
        //detected = false;
        //detection();
    }
    /*
    public void detection()
    {
        Transform ind = this.gameObject.transform.GetChild(1);
        GameObject indicator = ind.gameObject;
        if (!detected)
        {
            indicator.SetActive(false);
        }
        else
        {
            indicator.SetActive(true);
            indicator.GetComponent<Animator>().Play("Target");
        }
    }
    */
    public void SkeletonExecute()
    {
        isStaggering = false;
        animator.SetBool("Execute", true);
        //add any item drops or effects on execute here

        Destroy(this.gameObject, 3.5f);

    }

    public void TakeDamage(int damage)
    {
        //Die if staggered
        if(isStaggering == true)
        {
            Die();
        }

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
                animator.SetBool("Stagger", true);
                isStaggering = true;
                this.gameObject.GetComponent<EnemyFollow>().enabled = false;
                StartCoroutine(StaggerDeath());
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
    IEnumerator StaggerDeath()
    {
        yield return new WaitForSeconds(3f);
        if (isStaggering)
        {
            Die();
        }
    }
}

