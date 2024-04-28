using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField]
    private int healthBonus = 10; // value of health increase in player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerHealthOld playerHealthOld = collision.gameObject.GetComponent<PlayerHealthOld>();
            if (playerHealthOld != null)
            {
                playerHealthOld.Heal(healthBonus); // heal player
                Destroy(gameObject); // destroy collectable
            }
        }
    }
}
