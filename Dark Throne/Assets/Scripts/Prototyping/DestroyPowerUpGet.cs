using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class DestroyPowerUpGet : MonoBehaviour
{
    public GameObject powerupobtain;
    public PlayerPowerUps manager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(powerupobtain);
            manager.gainDoubleJump();
        }
    }
}
