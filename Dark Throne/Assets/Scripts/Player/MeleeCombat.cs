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
    public float detectionRange = 4f;

    public GameObject indicatorprefab;
    private GameObject indicatorInstance;

    Collider2D closestEnemy = null;
    public bool inExecuteAnimation = false;

    private Vector2 enemyPosition;

    public KeyCode executeKey = KeyCode.F;



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
        findClosestEnemy();
        if(closestEnemy != null && Input.GetKeyDown(executeKey) && closestEnemy.GetComponent<EnemyHealth>().isStaggering && !inExecuteAnimation)
        {
            inExecuteAnimation = true;
            closestEnemy.GetComponent<EnemyHealth>().SkeletonExecute();
            enemyPosition = closestEnemy.transform.position;
            this.transform.position = enemyPosition;

            this.GetComponent<PlayerMovement>().enabled = false;
            this.GetComponent<PlayerDash>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(enableMovement());
        }
    }

    void findClosestEnemy()
    {
        Collider2D[] enemiesDetected = Physics2D.OverlapCircleAll(this.transform.position, detectionRange, enemyLayer);
        float closestDistance = Mathf.Infinity;
        closestEnemy = null;

        foreach (Collider2D enemy in enemiesDetected)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
            if (closestEnemy != null && closestEnemy.GetComponent<EnemyHealth>().isStaggering)
            {
                if (indicatorInstance == null)
                {
                    Vector2 newPosition = new Vector2(closestEnemy.transform.position.x, closestEnemy.transform.position.y + 50);
                    Debug.Log("new Position =" + newPosition);
                    Transform ind = closestEnemy.gameObject.transform.GetChild(1);
                    GameObject indicator = ind.gameObject;
                    indicatorInstance = Instantiate(indicatorprefab, indicator.transform.position, Quaternion.identity);
                    indicatorInstance.transform.SetParent(closestEnemy.transform); // Set the indicator's parent to the enemy
                    indicatorInstance.SetActive(true);
                    indicatorInstance.GetComponent<Animator>().Play("Target");
                    Destroy(indicatorInstance, 1f);
                }
                else
                {
                    Transform ind = closestEnemy.gameObject.transform.GetChild(1);
                    GameObject indicator = ind.gameObject;
                    indicatorInstance.transform.position = indicator.transform.position;
                    Destroy(indicatorInstance, 1f);


                }
                //enemy.GetComponent<EnemyHealth>().detection();
            }
            else
            {
                if (indicatorInstance != null)
                {
                    Destroy(indicatorInstance);
                    indicatorInstance = null;
                }
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
    public void IncreaseAttackDamage(int amount)
    {
        attackDamage += amount;
        Debug.Log("Attack Damage Increased: " + attackDamage);
    }
    private void OnDrawGizmosSelected()
    {
        if(hitBox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitBox.position, attackRange);

        Gizmos.DrawWireSphere(this.transform.position, detectionRange);
    }

    IEnumerator enableMovement()
    {
        yield return new WaitForSeconds(3.5f);
        this.GetComponent<PlayerMovement>().enabled = true;
        this.GetComponent<PlayerDash>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        inExecuteAnimation = false;
    }
}
