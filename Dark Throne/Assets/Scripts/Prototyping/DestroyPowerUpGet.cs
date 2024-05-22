using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPowerUpGet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Player")
        {
            Destroy(gameObject);

            PlayerPowerUps DoubleJumpScript = collider.GetComponent<PlayerPowerUps>();

            // If the dashScript is found on the player, call gainDash.
            if (DoubleJumpScript != null)
            {
                DoubleJumpScript.gainDoubleJump();
            }
            else
            {
                Debug.LogError("PlayerDoubleJump component not found on " + collider.gameObject.name);
            }
        }
    }
}
