using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPowerUpGetDash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Player")
        {
            Destroy(gameObject);

            PlayerDash DashScript = collider.GetComponent<PlayerDash>();

            // If the dashScript is found on the player, call gainDash.
            if (DashScript != null)
            {
                DashScript.gainDash();
            }
            else
            {
                Debug.LogError("PlayerDoubleJump component not found on " + collider.gameObject.name);
            }
        }
    }
}
