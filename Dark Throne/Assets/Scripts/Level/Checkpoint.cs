using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isAcquired = false;
    public string id = "123456789";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isAcquired)
            {
                Activate();

                if (AnalyticsManager.Instance != null)
                {
                    AnalyticsManager.Instance.AquiredCheckpoint(id);
                }
                else
                {
                    Debug.LogError("AnalyticsManager instance not found");
                }

                Debug.Log("Checkpoint acquired at: " + transform.position);
                SaveCheckpoint();
            }
            // Set the recent ID
            SaveID();
        }



    }

    private void SaveCheckpoint()
    {
        // Find the SaveLoadJSONCheckpoints script in the scene
        SaveLoadJSONCheckpoints saveLoadScript = FindObjectOfType<SaveLoadJSONCheckpoints>();
        if (saveLoadScript != null)
        {
            saveLoadScript.SaveGame(transform.position, id);
            saveLoadScript.MakeCheckpointsOnMap();

        }
        else
        {
            Debug.LogError("SaveLoadJSONCheckpoints script not found in the scene!");
        }
    }

    public void Activate()
    {
        isAcquired = true;

    }

    private void SaveID()
    {
        // Find the SaveLoadJSONCheckpoints script in the scene
        SaveLoadJSONPlayer saveLoadPlayerScript = FindObjectOfType<SaveLoadJSONPlayer>();
        if (saveLoadPlayerScript != null)
        {
            SpawnManager.SetId(id);
            saveLoadPlayerScript.SaveGame();
        }
        else
        {
            Debug.LogError("SaveLoadJSONPlayer script not found in the scene!");
        }
    }


}
