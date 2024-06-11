using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToMenu : MonoBehaviour
{
    private SaveLoadJSONPlayer playerJsonScript;
    private SaveLoadJSONNpc npcJsonScript;
    private SaveLoadJSONCheckpoints checkpointsJsonScript;

    void Start()
    {
        // Find the SaveLoadJSONPlayer script in the scene
        playerJsonScript = FindObjectOfType<SaveLoadJSONPlayer>();
        if (playerJsonScript == null)
        {
            Debug.LogError("SaveLoadJSONPlayer script not found in the scene!");
        }

        // Find the SaveLoadJSONNPC script in the scene
        npcJsonScript = FindObjectOfType<SaveLoadJSONNpc>();
        if (npcJsonScript == null)
        {
            Debug.LogError("SaveLoadJSONNpc script not found in the scene!");
        }

        // Find the SaveLoadJSONCheckpoints script in the scene
        checkpointsJsonScript = FindObjectOfType<SaveLoadJSONCheckpoints>();
        if (checkpointsJsonScript == null)
        {
            Debug.LogError("SaveLoadJSONCheckpoints script not found in the scene!");
        }
    }

    public void GoToMain()
    {
        // Find the player GameObject and destroy it
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("AHFUDJS:" + player);
        if (player != null)
        {
            Destroy(player);
            Debug.Log("Player object destroyed.");
        }
        else
        {
            Debug.LogError("Player object not found!");
        }

        if (playerJsonScript != null)
        {
            playerJsonScript.SaveGame(); // Save the game data
        }
        else
        {
            Debug.LogError("SaveLoadJSONPlayer script is not assigned!");
        }

        // Go to "MainMenu" scene
        SceneManager.LoadScene("MainMenu");
    }

    public void deleteData()
    {
        if (playerJsonScript != null)
        {
            playerJsonScript.DeleteSaveFile(); // Delete the player save data
        }
        else
        {
            Debug.LogError("SaveLoadJSONPlayer script is not assigned!");
        }

        if (npcJsonScript != null)
        {
            npcJsonScript.DeleteSaveFile(); // Delete the NPC save data
        }
        else
        {
            Debug.LogError("SaveLoadJSONNPC script is not assigned!");
        }

        if (checkpointsJsonScript != null)
        {
            checkpointsJsonScript.DeleteSaveFile(); // Delete the checkpoints save data
        }
        else
        {
            Debug.LogError("SaveLoadJSONCheckpoints script is not assigned!");
        }
    }
}
