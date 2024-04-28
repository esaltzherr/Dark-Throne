using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIncreaseCollectible : MonoBehaviour
{
    [SerializeField]
    private int attackPowerBonus = 5; // value of attack power increase for player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MeleeCombat meleeCombat = collision.gameObject.GetComponent<MeleeCombat>(); 
            if (meleeCombat != null) 
            {
                meleeCombat.IncreaseAttackDamage(attackPowerBonus); // increase player attack power
                Destroy(gameObject); // destroy collectible
            }
        }
    }
}

