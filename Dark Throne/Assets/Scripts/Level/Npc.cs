using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    // Private variables
    private GameObject dialogue;
    private bool isPlayerInRange = false;
    private int currentDialogueIndex = 0;
    private GameObject[] dialogues;
    private GameObject interactButton;
    private NPCIcon npcIcon;
    public bool abilityGiver = false;
    public GameObject powerup;
    private float unitsInFront = 3;

    private GameObject speechBubbleSprite;

    private void Start()
    {
        // Find the Speech Bubble child by its name
        dialogue = transform.Find("Dialogue").gameObject;
        speechBubbleSprite = transform.Find("SpeechBubbleSprite").gameObject;
        speechBubbleSprite.SetActive(false);

        // Check if the Speech Bubble was found
        if (dialogue != null)
        {
            // Get all children of the Speech Bubble and store them in an array
            dialogues = new GameObject[dialogue.transform.childCount];
            for (int i = 0; i < dialogue.transform.childCount; i++)
            {
                dialogues[i] = dialogue.transform.GetChild(i).gameObject;
                dialogues[i].SetActive(false); // Disable all dialogue children initially
            }

            // Ensure the Speech Bubble itself is disabled
            dialogue.SetActive(false);
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

        npcIcon = GetComponentInChildren<NPCIcon>();
        if (npcIcon == null)
        {
            Debug.LogWarning("NPCIcon script not found in " + gameObject.name);
        }

    }

    private void Update()
    {
        // Check if the player is in range and the 'E' key is pressed
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            speechBubbleSprite.SetActive(true);
            dialogue.SetActive(true);
            DisplayNextDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;

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
            if (dialogue != null)
            {
                speechBubbleSprite.SetActive(false);
                dialogue.SetActive(false);
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
                speechBubbleSprite.SetActive(false);
                dialogue.SetActive(false);

                // Disable the NPC icon once all dialogues are finished
                if (npcIcon != null)
                {
                    npcIcon.Disable();
                }

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
            if (dialogue != null)
            {
                speechBubbleSprite.SetActive(false);
                dialogue.SetActive(false);
                foreach (GameObject dialogue in dialogues)
                {
                    dialogue.SetActive(false);
                }
                // Disable the NPC icon once all dialogues are finished
                if (npcIcon != null)
                {
                    npcIcon.Disable();
                }
                if (powerup != null && abilityGiver)
                {
                    SpawnPowerup();
                }
                if (interactButton != null)
                {
                    interactButton.SetActive(false);
                }
            }
        }
    }


    public void SpawnPowerup()
    {

        float direction = transform.localScale.x > 0 ? 1 : -1;

        // Calculate the spawn position
        Vector3 spawnPosition = transform.position + -transform.right * unitsInFront * direction;

        // Instantiate the power-up at the calculated position
        Instantiate(powerup, spawnPosition, Quaternion.identity);
        abilityGiver = false;
    }

}
