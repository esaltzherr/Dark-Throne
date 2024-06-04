using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeCombat : MonoBehaviour
{
    public Animator animator;
    public Transform hitBox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 40;
    private float attackRate = 2f;
    float nextAttackTime = 0f;
    public float detectionRange = 4f;
    private float swipeAttackCD = 1f;

    public bool CanAttack = true;
    public GameObject indicatorprefab;
    private GameObject indicatorInstance;

    Collider2D closestEnemy = null;
    public bool inExecuteAnimation = false;

    private Vector2 enemyPosition;

    public KeyCode executeKey = KeyCode.F;

    private float nextSwipeTime = 0f;


    public KeyCode attackOneKey = KeyCode.Mouse0; // Left mouse button
    private KeyCode attackTwoKey = KeyCode.Mouse1; // Right mouse button



    // Update is called once per frame 
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(attackOneKey))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(attackTwoKey) && CanAttack && (Time.time >= nextSwipeTime))
            {
                Swipe();
                nextAttackTime = Time.time + 1f / swipeAttackCD;
                nextSwipeTime = Time.time + 2f / swipeAttackCD;

            }
        }
        /*else if(canCombo)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canCombo)
            {
                Swipe();
                nextAttackTime = Time.time + 1f / swipeAttackCD;
                canCombo = false;
            }
        
        }
        */
        findClosestEnemy();
        if (closestEnemy != null && Input.GetKeyDown(executeKey) && closestEnemy.GetComponent<EnemyHealth>().isStaggering && !inExecuteAnimation)
        {
            this.GetComponent<PlayerHealth2>().Heal(1);
            this.GetComponent<PlayerInvulnerability>().ExecuteInvulnerability();
            inExecuteAnimation = true;
            if (closestEnemy.tag == "FlyingEnemy")
            {
                closestEnemy.GetComponent<EnemyHealth>().FlyingExecute();
            }
            else
            {
                closestEnemy.GetComponent<EnemyHealth>().SkeletonExecute();
            }
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
        StartCoroutine(spawnStabHitbox());
        /*Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in enemiesHit)
        {

            Debug.Log("Enemy Hit: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        StartCoroutine(stabCD());
        */
    }

    void Swipe()
    {
        animator.SetTrigger("AttackSwing");
        StartCoroutine(spawnSwipeHitbox());
        /*Vector2 swipeHitbox = new Vector2(hitBox.position.x - 1, hitBox.position.y);

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(swipeHitbox, attackRange + 0.5f, enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {

            Debug.Log("Enemy Hit: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            Vector2 movePos = this.transform.position - enemy.transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(movePos.normalized * -500f, ForceMode2D.Impulse);
            if(enemy.tag != "FlyingEnemy")
            {
                enemy.GetComponent<EnemyFollow>().enabled = false;
                StartCoroutine(enableEnemyMovement(enemy.gameObject));
            }
        }
        StartCoroutine(swipeCD());
        */
    }
    public void IncreaseAttackDamage(int amount)
    {
        attackDamage += amount;
        Debug.Log("Attack Damage Increased: " + attackDamage);
    }
    private void OnDrawGizmosSelected()
    {
        if (hitBox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitBox.position, attackRange);

        Vector2 swipeHitbox = new Vector2(hitBox.position.x - 1, hitBox.position.y);
        Gizmos.DrawWireSphere(swipeHitbox, attackRange + 0.5f);


        Gizmos.DrawWireSphere(this.transform.position, detectionRange);
    }

    public IEnumerator enableMovement()
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

    IEnumerator stabCD()
    {
        yield return new WaitForSeconds(0.5f);
        CanAttack = true;
    }
    IEnumerator swipeCD()
    {
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }
    IEnumerator enableEnemyMovement(GameObject enemy)
    {
        yield return new WaitForSeconds(0.5f);
        if (enemy.GetComponent<EnemyHealth>().getHealth() > 0)
        {
            enemy.GetComponent<EnemyFollow>().enabled = true;
        }
    }

    IEnumerator spawnStabHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {

            Debug.Log("Enemy Hit: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        StartCoroutine(stabCD());
    }

    IEnumerator spawnSwipeHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        Vector2 swipeHitbox = new Vector2(hitBox.position.x - 1, hitBox.position.y);

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(swipeHitbox, attackRange + 0.5f, enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {

            Debug.Log("Enemy Hit: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            Vector2 movePos = this.transform.position - enemy.transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(movePos.normalized * -500f, ForceMode2D.Impulse);
            if (enemy.tag != "FlyingEnemy")
            {
                enemy.GetComponent<EnemyFollow>().enabled = false;
                StartCoroutine(enableEnemyMovement(enemy.gameObject));
            }
        }
        StartCoroutine(swipeCD());
    }
}
