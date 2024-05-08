using UnityEngine;
using UnityEngine.UI;

public class CheckpointButton : MonoBehaviour
{


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
}
