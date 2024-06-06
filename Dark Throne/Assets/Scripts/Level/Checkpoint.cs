using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isAcquired = false;
    public string id = "123456789";
    public Animator animator;

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


    public void useCheckpoint(string name)
    {
        Debug.Log("teleporting to checkpoint:" + name);

        Vector3 pos = this.transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = pos;
    }

}
