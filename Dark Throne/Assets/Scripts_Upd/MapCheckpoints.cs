using UnityEngine;

public class MapCheckpoints : MonoBehaviour
{
    public GameObject checkpointPrefab; // Assign your checkpoint prefab in the inspector.

    private GameObject mapPanel; // This will hold the reference to the map panel

    public void FindMapPanel()
    {
        // Get the MapDisplay script attached to the same canvas and access the mapPanel
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

    public void AddCheckpoint(Vector3 position, string newId)
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

        // Instantiate the checkpoint at the given position with default rotation
        GameObject newCheckpoint = Instantiate(checkpointPrefab, position, Quaternion.identity, mapPanel.transform);
        
        newCheckpoint.name = newId;
        // Optionally set the local position if needed, especially if position is meant to be local
        newCheckpoint.transform.localPosition = position;


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

            // Check if the child is an instance of the checkpointPrefab
            if (child.gameObject.CompareTag("Checkpoint")) // Make sure your checkpoint prefabs are tagged as "Checkpoint"
            {
                Destroy(child.gameObject); // Destroy the child GameObject
            }
        }
    }

    public void useCheckpoint(string name){

        //string id = name - not the id;
        Debug.Log("teleporting to checkpoint:" + name);
        SaveLoadJSONCheckpoints saveLoadScript = FindObjectOfType<SaveLoadJSONCheckpoints>();
        if(saveLoadScript != null){
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


    private void disableMap(){
       if (mapPanel == null)
        {
            FindMapPanel();
            if (mapPanel == null)
            {
                Debug.LogError("MapCheckpoints: mapPanel is not assigned.");
                return;
            }

        }
        mapPanel.SetActive(false);

    }
}
