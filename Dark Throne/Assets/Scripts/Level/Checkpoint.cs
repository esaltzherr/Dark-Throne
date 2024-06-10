using UnityEngine;
using System.Collections;
public class Checkpoint : MonoBehaviour
{
    public bool isAcquired = false;
    public string id = "123456789";
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the recent ID
            SaveID();
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
        }
    }

    private void SaveCheckpoint()
    {
        // Find the SaveLoadJSONCheckpoints script in the scene
        SaveLoadJSONCheckpoints saveLoadScript = FindObjectOfType<SaveLoadJSONCheckpoints>();
        if (saveLoadScript != null)
        {
            saveLoadScript.SaveGame(transform.position, id);
            // saveLoadScript.MakeCheckpointsOnMap();

        }
        else
        {
            Debug.LogError("SaveLoadJSONCheckpoints script not found in the scene!");
        }
    }

    public void Activate()
    {
        animator.enabled = true;
        isAcquired = true;

        // Find the child with the CheckpointButton script
        CheckpointButton checkpointButton = GetComponentInChildren<CheckpointButton>(true);
        if (checkpointButton != null)
        {
            checkpointButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("CheckpointButton script not found in child objects!");
        }
    }


    public void SaveID()
    {
        Debug.Log("Saved Checkpoint ID: " + id);
        SaveLoadJSONPlayer saveLoadPlayerScript = FindObjectOfType<SaveLoadJSONPlayer>();
        if (saveLoadPlayerScript != null)
        {
            SpawnManager.SetId(id);
            Debug.Log("Saving game:" + saveLoadPlayerScript);
            saveLoadPlayerScript.SaveGame(); // Call SaveGame as a coroutine
            Debug.Log("Saved game:" + saveLoadPlayerScript);
        }
        else
        {
            Debug.LogError("SaveLoadJSONPlayer script not found in the scene!");
        }
    }




    public void useCheckpoint(string name)
    {
        Debug.Log("teleporting to checkpoint:" + name);

        Vector3 pos = this.transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = pos;
    }

}
