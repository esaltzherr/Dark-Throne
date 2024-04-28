using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainDoubleJump : MonoBehaviour
{
    public GameObject refer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has triggered the ability gain.");
            // Attempt to get the PlayerDash script attached to the player.
            PlayerPowerUps d_jump = other.GetComponent<PlayerPowerUps>();
            Destroy(refer);
            // If the dashScript is found on the player, call gainDash.
            if (d_jump != null)
            {
                d_jump.gainDoubleJump();
            }
            else
            {
                Debug.LogError("PlayerD_jump component not found on " + other.gameObject.name);
            }
        }
    }
}
