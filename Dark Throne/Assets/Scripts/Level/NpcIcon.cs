using UnityEngine;

public class NPCIcon : MonoBehaviour
{
    public string id;

    public void Disable()
    {
        // Set the game object to inactive
        gameObject.SetActive(false);

        // Find the SaveLoadJSONNpc script in the scene and save the NPC icon state
        SaveLoadJSONNpc saveLoad = FindObjectOfType<SaveLoadJSONNpc>();
        if (saveLoad != null)
        {
            saveLoad.SaveNPCIcon(id);
        }
        else
        {
            Debug.LogWarning("SaveLoadJSONNpc script not found in the scene.");
        }
    }
}
