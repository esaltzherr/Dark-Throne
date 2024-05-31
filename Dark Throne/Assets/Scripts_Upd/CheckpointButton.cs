using UnityEngine;
using UnityEngine.UI;

public class CheckpointButton : MonoBehaviour
{

    public GameObject mapPanel; // This will hold the reference to the map panel
    public void PrintParents()
    {
        // Access the grandparent GameObject
        Transform grandparent = transform.parent.parent; // Adjust this based on your hierarchy if needed
        if (grandparent != null)
        {
            //     // Accessthe specific script. Replace 'YourScriptName' with the actual name of your script
            MapCheckpoints checkpointsScript = grandparent.GetComponent<MapCheckpoints>();
            if (checkpointsScript != null)
            {
                // Call the method on the script, passing this GameObject's name
                checkpointsScript.useCheckpoint(gameObject.name);
            }
            else
            {
                Debug.LogError("Script not found on grandparent!");
            }
        }
        else
        {
            Debug.LogError("Grandparent not found!");
        }
    }

    void Start()
    {
        FindMapPanel();
    }
    public void Teleport()
    {


        if (mapPanel == null)
        {
            FindMapPanel();
        }
        if (mapPanel != null && mapPanel.activeSelf == true)
        {
            Debug.Log("" + this.gameObject.name);

            Transform grandparent = transform.parent.parent;
            Checkpoint checkpointsScript = grandparent.GetComponent<Checkpoint>();
            if (checkpointsScript != null)
            {
                // Call the method on the script, passing this GameObject's name
                checkpointsScript.useCheckpoint(gameObject.name);
            }
            else
            {
                Debug.LogError("Script not found on grandparent!");
            }
            disableMap();
        }

    }

    public void FindMapPanel()
    {
        if (mapPanel == null)
        {
            Canvas parentCanvas = GetComponentInParent<Canvas>(); // Find the parent Canvas
            if (parentCanvas != null)
            {
                if (parentCanvas.worldCamera != null)
                {
                    mapPanel = parentCanvas.worldCamera.gameObject; // Assign the event camera GameObject to mapPanel
                    Debug.Log("StartScript: Successfully fetched mapPanel from Canvas' event camera.");
                }
                {
                    Debug.LogError("MapCheckpoints: Failed to find WorldCamera");
                    Debug.Log(""+ parentCanvas.name);
                }
            }

            else
            {
                Debug.LogError("MapCheckpoints: Failed to find Canvas");
            }
        }
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
