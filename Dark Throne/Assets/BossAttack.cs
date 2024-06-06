using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private int attacknumber;
    public bool CanAttack = true;
    public float attackCooldown = 3f;
    public Animator animator;
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
        //StartCoroutine(StartCooldown());
    }
    public void Attack2()
    {
        animator.SetTrigger("Attack2");
        //StartCoroutine(StartCooldown());
    }
    public void Attack3()
    {
        animator.SetTrigger("Attack3");
        //StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }
}
