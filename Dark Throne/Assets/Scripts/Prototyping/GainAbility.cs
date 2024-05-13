using UnityEngine;

public class GainAbility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has triggered the ability gain.");
            // Attempt to get the PlayerDash script attached to the player.
            PlayerDash dashScript = other.GetComponent<PlayerDash>();

            // If the dashScript is found on the player, call gainDash.
            if (dashScript != null)
            {
                if (AnalyticsManager.Instance != null)
                {
                    AnalyticsManager.Instance.DashGainEvent();
                }
                else
                {
                    Debug.LogError("AnalyticsManager instance not found");
                }
                dashScript.gainDash();
            }
            else
            {
                Debug.LogError("PlayerDash component not found on " + other.gameObject.name);
            }
        }
    }


}
