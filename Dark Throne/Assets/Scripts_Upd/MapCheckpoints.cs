using UnityEngine;

public class MapCheckpoints : MonoBehaviour
{
    public GameObject checkpointPrefab; // Assign your checkpoint prefab in the inspector.

    private GameObject mapPanel; // This will hold the reference to the map panel
    public float positionScale = 0.8f; // Scale to adjust the checkpoint's position
    public Vector3 positionOffset = new Vector3(1000, 1000, 0); // Offset to adjust the checkpoint's position

    public void FindMapPanel()
    {
        if (mapPanel == null)
        {
            Transform mapTransform = transform.Find("Map");
            if (mapTransform != null)
            {
                mapPanel = mapTransform.gameObject;
            }
            else
            {
                Debug.LogError("MapCheckpoints: Failed to find MapDisplay script or mapPanel.");
            }
        }
    }

    public void AddCheckpoint(Vector3 worldPosition, string newId)
    {
        if (mapPanel == null)
        {
            FindMapPanel();
            if (mapPanel == null)
            {
                Debug.LogError("MapCheckpoints: mapPanel is not assigned.");
                return;
            }
        }
        // Debug.Log("W:"+ worldPosition);
        // Debug.Log("S:"+ positionScale);
        // Apply scaling and offset to the world position to convert it to map coordinates
        Vector3 adjustedPosition = (worldPosition * positionScale) + positionOffset;
        // Debug.Log("A:" + adjustedPosition);

        // Instantiate the checkpoint at the adjusted position with default rotation
        GameObject newCheckpoint = Instantiate(checkpointPrefab, adjustedPosition, Quaternion.identity, mapPanel.transform);
        newCheckpoint.name = newId;
        
        // Set the local position as the adjusted position
        newCheckpoint.transform.localPosition = adjustedPosition;
    }

    public void ResetCheckpoints()
    {
        if (mapPanel == null)
        {
            FindMapPanel();
            if (mapPanel == null)
            {
                Debug.LogError("MapCheckpoints: mapPanel is not assigned.");
                return;
            }
        }

        // Loop backwards through the children of the mapPanel
        for (int i = mapPanel.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = mapPanel.transform.GetChild(i);
            if (child.gameObject.CompareTag("Checkpoint"))
            {
                Destroy(child.gameObject); // Destroy the child GameObject
            }
        }
    }

    public void useCheckpoint(string name)
    {
        Debug.Log("teleporting to checkpoint:" + name);
        SaveLoadJSONCheckpoints saveLoadScript = FindObjectOfType<SaveLoadJSONCheckpoints>();
        if (saveLoadScript != null)
        {
            Vector3 pos = saveLoadScript.GetLocation(name) ?? Vector3.zero;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = pos;
        }
        else
        {
            Debug.LogError("SaveLoadJSONCheckpoints script not found in the scene!");
        }
        disableMap();
    }

    private void disableMap()
    {
        if (mapPanel == null)
        {
            FindMapPanel();
            if (mapPanel == null)
            {
                Debug.LogError("MapCheckpoints: mapPanel is not assigned.");
                return;
            }
        }
        Time.timeScale = 1;
        mapPanel.SetActive(false);
    }
}
