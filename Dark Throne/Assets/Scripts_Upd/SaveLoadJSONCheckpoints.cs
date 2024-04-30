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

        // if (Input.GetKeyDown(KeyCode.I))
        //     DeleteSaveFile();
    }

    public void SaveGame(Vector3 position)
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        GetDataFromFile();

        if (!checkpointData.allScenes.ContainsKey(currentSceneName))
        {
            checkpointData.allScenes[currentSceneName] = new List<CheckpointData>();
        }

        CheckpointData data = new CheckpointData();
        data.position = position;
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
    }

    public void LoadGame()
    {
        // TODO: Reactivate all allready aquired checkpoints for the scene.
        // basically just go through and if the positions exist call a activate fucntion ()

        GetDataFromFile();
        if (checkpointData != null && checkpointData.allScenes.ContainsKey(SceneManager.GetActiveScene().name))
        {
            List<CheckpointData> currentSceneCheckpoints = checkpointData.allScenes[SceneManager.GetActiveScene().name];
            foreach (CheckpointData checkpoint in currentSceneCheckpoints)
            {
                // Here you might want to handle activating checkpoints in the scene based on their acquired status
                Debug.Log($"Checkpoint at {checkpoint.position}");// acquired: {checkpoint.isAcquired}");
            }
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

    public void GetCheckpoints(string sceneName)
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints)
        {
            CheckpointData data = new CheckpointData();
            data.position = checkpoint.transform.position;
            // data.isAcquired = checkpoint.GetComponent<Checkpoint>().isAcquired; // Assuming there is a 'Checkpoint' script that contains the 'isAcquired' property

            checkpointData.allScenes[sceneName].Add(data);
        }
    }
}
