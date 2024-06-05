using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private int attacknumber;
    public bool CanAttack;
    public float attackCooldown = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack)
        {
            attacknumber = Random.Range(0, 3);
            ChooseAttack(attacknumber);
            StartCoroutine(StartCooldown());
        }
    }
    
    //use random 0-3 to choose which attack to use
    public void ChooseAttack(int choice)
    {
        if(choice == 0)
        {
            Attack1();
        }
        if(choice == 1)
        {
            Attack2();
        }
        if(choice == 2)
        {
            Attack3();
        }
    }

    //Use Attack 1 (Punch)
    public void Attack1()
    {

    }
    public void Attack2()
    {

    }
    public void Attack3()
    {

    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }
}
