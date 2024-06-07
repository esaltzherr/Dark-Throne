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
            Tilemap.SetActive(true);
        }
    }
}
