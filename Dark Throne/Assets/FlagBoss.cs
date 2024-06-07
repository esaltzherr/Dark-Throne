using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBoss : MonoBehaviour
{
    public GameObject Tilemap;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
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
        if(enemy1 == null && enemy2 == null && enemy3 == null)
        {
            Tilemap.SetActive(false);
        }
    }
}
