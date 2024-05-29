using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    // Private variables
    private GameObject speechBubble;
    private bool isPlayerInRange = false;
    private int currentDialogueIndex = 0;
    private GameObject[] dialogues;
    private GameObject interactButton;

    private void Start()
    {
        // Find the Speech Bubble child by its name
        speechBubble = transform.Find("SpeechBubble").gameObject;

        // Check if the Speech Bubble was found
        if (speechBubble != null)
        {
            // Get all children of the Speech Bubble and store them in an array
            dialogues = new GameObject[speechBubble.transform.childCount];
            for (int i = 0; i < speechBubble.transform.childCount; i++)
            {
                dialogues[i] = speechBubble.transform.GetChild(i).gameObject;
                dialogues[i].SetActive(false); // Disable all dialogue children initially
            }

            // Ensure the Speech Bubble itself is disabled
            speechBubble.SetActive(false);
        }
        else
        {
            Debug.LogWarning("SpeechBubble child not found in " + gameObject.name);
        }


        interactButton = transform.Find("InteractButton").gameObject;
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("InteractButton child not found in " + gameObject.name);
        }

    }

    private void Update()
    {
        // Check if the player is in range and the 'E' key is pressed
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (interactButton != null)
            {
                interactButton.SetActive(false);
            }
            speechBubble.SetActive(true);
            DisplayNextDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Enable the speech bubble and show the first dialogue
            if (speechBubble != null)
            {
                // speechBubble.SetActive(true);
                // DisplayNextDialogue();
            }

            if (interactButton != null)
            {
                interactButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Disable the speech bubble and all dialogues
            if (speechBubble != null)
            {
                speechBubble.SetActive(false);
                foreach (GameObject dialogue in dialogues)
                {
                    dialogue.SetActive(false);
                }
            }
            currentDialogueIndex = 0;

            if (interactButton != null)
            {
                interactButton.SetActive(false);
            }
        }
    }

    private void DisplayNextDialogue()
    {
        // Check if there's only one dialogue
        if (dialogues.Length == 1)
        {
            // Toggle the visibility of the single dialogue
            dialogues[0].SetActive(!dialogues[0].activeInHierarchy);

            // Hide the speech bubble if the dialogue is hidden
            if (!dialogues[0].activeInHierarchy)
            {
                speechBubble.SetActive(false);
            }
            return;
        }

        // For multiple dialogues
        bool first = false;
        bool last = false;

        if (!dialogues[currentDialogueIndex].activeInHierarchy)
        {
            first = true;
            dialogues[currentDialogueIndex].SetActive(true);
        }

        // Disable the current dialogue
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogues[currentDialogueIndex].SetActive(false);
        }

        // Skip this if it's the first dialog so that 
        if (!first)
        {
            // Move to the next dialogue
            int before = currentDialogueIndex;
            currentDialogueIndex = (currentDialogueIndex + 1) % dialogues.Length;
            if (before > currentDialogueIndex)
            {
                last = true;
            }
        }

        if (!last)
        {
            // Enable the next dialogue
            if (currentDialogueIndex < dialogues.Length)
            {
                dialogues[currentDialogueIndex].SetActive(true);
            }
        }
        else
        {
            if (speechBubble != null)
            {
                speechBubble.SetActive(false);
                foreach (GameObject dialogue in dialogues)
                {
                    dialogue.SetActive(false);
                }
            }
        }
    }

}
