using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBoss : MonoBehaviour
{
    public GameObject Tilemap;
    public MapDisplay mapDisplayScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            Tilemap.SetActive(true);
            // disable the map
            mapDisplayScript.enabled = false;
        }
    }

    private void Start()
    {
        Debug.Log("Entered Boss Room");
        EnemyHealth.SetKills(0);
    }
    private void Update()
    {
        if (EnemyHealth.GetKills() >= 3)
        {
            Tilemap.SetActive(false);
            // Enable the map
            mapDisplayScript.enabled = true;
        }
    }
}
