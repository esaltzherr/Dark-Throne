using UnityEngine;

public class Npc : MonoBehaviour
{
    // Private variable to hold the Speech Bubble GameObject
    private GameObject speechBubble;

    private void Start()
    {
        // Find the Speech Bubble child by its name
        speechBubble = transform.Find("SpeechBubble").gameObject;

        // Check if the Speech Bubble was found and disable it
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
        else
        {
            Debug.LogWarning("SpeechBubble child not found in " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enable the speech bubble
            if (speechBubble != null)
            {
                speechBubble.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Disable the speech bubble
            if (speechBubble != null)
            {
                speechBubble.SetActive(false);
            }
        }
    }
}
