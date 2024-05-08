using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

[System.Serializable]
public class CheckpointData
{
    [JsonConverter(typeof(Vector3Converter))]

    public Vector3 position;

    // Add the id to the spawnmanager.
    public string ID;


    // public bool isAcquired;
}

[System.Serializable]
public class AllScenesCheckpoints
{
    public Dictionary<string, List<CheckpointData>> allScenes = new Dictionary<string, List<CheckpointData>>();
}

public class SaveLoadJSONCheckpoints : MonoBehaviour
{
    AllScenesCheckpoints checkpointData;
    string saveFilePath;

    void Awake()
    {
        checkpointData = new AllScenesCheckpoints();
        saveFilePath = Application.persistentDataPath + "/CheckpointData.json";
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Y))
        //     SaveGame();

        // if (Input.GetKeyDown(KeyCode.U))
        //     LoadGame();

        if (Input.GetKeyDown(KeyCode.V))
            DeleteSaveFile();
    }

    public void SaveGame(Vector3 position, string id)
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        GetDataFromFile();

        if (!checkpointData.allScenes.ContainsKey(currentSceneName))
        {
            checkpointData.allScenes[currentSceneName] = new List<CheckpointData>();
        }

        CheckpointData data = new CheckpointData();
        data.position = position;
        data.ID = id;
        // data.isAcquired = checkpoint.GetComponent<Checkpoint>().isAcquired; // Assuming there is a 'Checkpoint' script that contains the 'isAcquired' property

        checkpointData.allScenes[currentSceneName].Add(data);

        string saveCheckpointData = JsonConvert.SerializeObject(checkpointData);
        File.WriteAllText(saveFilePath, saveCheckpointData);


        Debug.Log("Saved Checkpoint Data: " + saveCheckpointData);
        Debug.Log("Save file created at: " + saveFilePath);


        // string currentSceneName = SceneManager.GetActiveScene().name;
        // GetDataFromFile();

        // if (!checkpointData.allScenes.ContainsKey(currentSceneName))
        // {
        //     checkpointData.allScenes[currentSceneName] = new List<CheckpointData>();
        // }
        // else
        // {
        //     checkpointData.allScenes[currentSceneName].Clear();
        // }

        // GetCheckpoints(currentSceneName);

        // string saveCheckpointData = JsonConvert.SerializeObject(checkpointData);
        // File.WriteAllText(saveFilePath, saveCheckpointData);

        // Debug.Log("Saved Checkpoint Data: " + saveCheckpointData);
        // Debug.Log("Save file created at: " + saveFilePath);
    }

    public void GetDataFromFile()
    {
        if (File.Exists(saveFilePath))
        {
            string loadData = File.ReadAllText(saveFilePath);
            checkpointData = JsonConvert.DeserializeObject<AllScenesCheckpoints>(loadData);
        }
        else
        {
            Debug.Log("There is no save file to load! (Checkpoints)");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadGame();
        MakeCheckpointsOnMap();
    }

    public void LoadGame()
    {
        GetDataFromFile();
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (checkpointData != null && checkpointData.allScenes.ContainsKey(currentSceneName))
        {
            List<CheckpointData> savedCheckpoints = checkpointData.allScenes[currentSceneName];
            GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

            foreach (GameObject checkpointObject in checkpoints)
            {
                Checkpoint checkpointScript = checkpointObject.GetComponent<Checkpoint>();
                if (checkpointScript != null && !checkpointScript.isAcquired)
                {
                    Vector3 checkpointPos = checkpointObject.transform.position;
                    foreach (CheckpointData data in savedCheckpoints)
                    {
                        if (Vector3.Distance(checkpointPos, data.position) < 0.1f) // Consider a small threshold to account for floating point imprecision
                        {
                            checkpointScript.Activate();
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No checkpoint data available for this scene.");
        }
    }


    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted!");
        }
        else
        {
            Debug.Log("There is nothing to delete!");
        }
    }

    public void MakeCheckpointsOnMap()
    {
        GetDataFromFile();

        // Find the 'MapCheckpoints' script attached to a GameObject in the scene.
        MapCheckpoints mapCheckpointsScript = GameObject.FindObjectOfType<MapCheckpoints>();

        if (mapCheckpointsScript == null)
        {
            Debug.LogError("MapCheckpoints script not found in the scene.");
            return;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        if (checkpointData.allScenes.ContainsKey(sceneName))
        {
            mapCheckpointsScript.ResetCheckpoints();
            List<CheckpointData> checkpoints = checkpointData.allScenes[sceneName];
            foreach (CheckpointData checkpoint in checkpoints)
            {
                mapCheckpointsScript.AddCheckpoint(checkpoint.position, checkpoint.ID);
                
            }
        }
        else
        {
            Debug.Log("No checkpoint data available for this scene in the map.");
        }
    }




    // public void GetCheckpoints()//string sceneName)
    // {
    //     string sceneName = SceneManager.GetActiveScene().name;
    //     GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    //     foreach (GameObject checkpoint in checkpoints)
    //     {
    //         CheckpointData data = new CheckpointData();
    //         data.position = checkpoint.transform.position;
    //         // data.isAcquired = checkpoint.GetComponent<Checkpoint>().isAcquired; // Assuming there is a 'Checkpoint' script that contains the 'isAcquired' property

    //         checkpointData.allScenes[sceneName].Add(data);
    //     }
    // }
    public Vector3? GetLocation(string id)
    {
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if there are checkpoints for the current scene
        if (checkpointData.allScenes.ContainsKey(currentSceneName))
        {
            // Get the list of checkpoints for the current scene
            List<CheckpointData> checkpoints = checkpointData.allScenes[currentSceneName];

            // Loop through each checkpoint data to find the matching ID
            foreach (CheckpointData checkpoint in checkpoints)
            {
                if (checkpoint.ID == id)
                {
                    // If found, return the position
                    return checkpoint.position;
                }
            }
            // If no checkpoint with the matching ID is found
            Debug.Log("No checkpoint found with ID: " + id);
        }
        else
        {
            // If there are no checkpoints for the scene
            Debug.Log("No checkpoint data available for this scene.");
        }

        // Return null if nothing is found or if there are no checkpoints for the scene
        return null;
    }


}
