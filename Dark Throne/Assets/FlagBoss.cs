using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBoss : MonoBehaviour
{
    public GameObject Tilemap;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            Tilemap.SetActive(true);
        }
    }

    private void Update()
    {
        if(EnemyHealth.GetKills() > 3)
        {
            Tilemap.SetActive(false);
        }
    }
}
