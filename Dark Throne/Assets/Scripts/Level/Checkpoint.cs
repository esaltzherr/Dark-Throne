using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isAcquired = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAcquired)
        {
            isAcquired = true;
            Debug.Log("Checkpoint acquired at: " + transform.position);
            SaveCheckpoint();
        }
    }

    private void SaveCheckpoint()
    {
        // Find the SaveLoadJSONCheckpoints script in the scene
        SaveLoadJSONCheckpoints saveLoadScript = FindObjectOfType<SaveLoadJSONCheckpoints>();
        if (saveLoadScript != null)
        {
            saveLoadScript.SaveGame(transform.position);
        }
        else
        {
            Debug.LogError("SaveLoadJSONCheckpoints script not found in the scene!");
        }
    }

    public void Activate(){
        isAcquired = true;
    }
}
