using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private int attacknumber;
    public bool CanAttack = true;
    public float attackCooldown = 3f;
    public Animator animator;
    public Transform Attack1Hitbox;
    public Transform Attack2Hitbox1;
    public Transform Attack2Hitbox2;
    public Transform Attack3Hitbox;
    public float Attack1Range = 0.5f;
    public float Attack2Range1 = 0.5f;
    public float Attack2Range2 = 0.5f;

    public float Attack3Range = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        CanAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack)
        {
            Debug.Log("inside canattack");
            attacknumber = Random.Range(0, 3);
            ChooseAttack(attacknumber);
            CanAttack = false;
            StartCoroutine(StartCooldown());
        }
    }

    //use random 0-3 to choose which attack to use
    public void ChooseAttack(int choice)
    {
        Debug.Log("inside choose attack");
        if (choice == 0)
        {
            Attack1();
        }
        if (choice == 1)
        {
            Attack2();
        }
        if (choice == 2)
        {
            Attack3();
        }
    }

    //Use Attack 1 (Punch)
    public void Attack1()
    {
        animator.SetTrigger("Attack1");
        StartCoroutine(attack1HitboxSpawn());
        //StartCoroutine(StartCooldown());
    }
    public void Attack2()
    {
        animator.SetTrigger("Attack2");
        StartCoroutine(attack2HitboxSpawn());

        //StartCoroutine(StartCooldown());
    }
    public void Attack3()
    {
        animator.SetTrigger("Attack3");
        StartCoroutine(attack3HitboxSpawn());

        //StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (Attack1Hitbox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(Attack1Hitbox.position, Attack1Range);
        Gizmos.DrawWireSphere(Attack2Hitbox1.position, Attack2Range1);
        Gizmos.DrawWireSphere(Attack2Hitbox2.position, Attack2Range2);
        Gizmos.DrawWireSphere(Attack3Hitbox.position, Attack3Range);
    }
    IEnumerator attack1HitboxSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(Attack1Hitbox.position, Attack1Range);
        foreach (Collider2D collider in playerHit)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth2>().ChangeHealth(-1);
            }
        }
    }
    IEnumerator attack2HitboxSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(Attack2Hitbox1.position, Attack2Range1);
        foreach (Collider2D collider in playerHit)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth2>().ChangeHealth(-1);
            }
        }
        yield return new WaitForSeconds(0.15f);
        Collider2D[] player1Hit = Physics2D.OverlapCircleAll(Attack2Hitbox2.position, Attack2Range2);
        foreach (Collider2D collider in player1Hit)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth2>().ChangeHealth(-1);
            }
        }
    }

    IEnumerator attack3HitboxSpawn()
    {
        yield return new WaitForSeconds(0.08f);
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(Attack3Hitbox.position, Attack3Range);
        foreach (Collider2D collider in playerHit)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth2>().ChangeHealth(-1);
            }
        }
    }
}
