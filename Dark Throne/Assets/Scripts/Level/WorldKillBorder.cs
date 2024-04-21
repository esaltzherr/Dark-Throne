using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this at the top for scene management

public class WorldKillBorder : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Exited: " + other.gameObject.name); // This will log the name of the object that exited the trigger

        if (other.CompareTag("Player") && SceneManager.GetActiveScene().isLoaded)
        {
            other.gameObject.GetComponent<MovementDashWallJump>().ChangeHealth(9999999);

        }
    }
}
