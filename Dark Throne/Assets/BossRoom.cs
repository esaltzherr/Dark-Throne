using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject tm;
    public Canvas bossCanvas;
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tm.SetActive(true);
            bossCanvas.gameObject.SetActive(true);
            boss.GetComponent<BossMovement>().enabled = true;
        }
    }

    private void Update()
    {
        
        if (BossHealthBar.GetIsKilled())
        {
            tm.SetActive(false);
        }
    }
}
