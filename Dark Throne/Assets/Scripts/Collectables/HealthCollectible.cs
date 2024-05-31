using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField]
    private int healthBonus = 1; // value of health increase

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth2 maxHearts = collision.gameObject.GetComponent<PlayerHealth2>();
            if (maxHearts != null)
            {
                maxHearts.CollectHealthItem(healthBonus); // manage health item collection
                Destroy(gameObject); // destroy collectable
            }
        }
    }
}
