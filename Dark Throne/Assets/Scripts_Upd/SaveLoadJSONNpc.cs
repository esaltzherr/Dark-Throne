using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

[System.Serializable]
public class NPCIconData
{
    public string ID;
}

[System.Serializable]
public class AllScenesNPCIcons
{
    public Dictionary<string, List<NPCIconData>> allScenes = new Dictionary<string, List<NPCIconData>>();
}

public class SaveLoadJSONNpc : MonoBehaviour
{
    AllScenesNPCIcons npcIconData;
    string saveFilePath;

    void Awake()
    {
        npcIconData = new AllScenesNPCIcons();
        saveFilePath = Application.persistentDataPath + "/NPCIconData.json";
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            DeleteSaveFile();
    }

    public void SaveNPCIcon(string id)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        GetDataFromFile();

        if (!npcIconData.allScenes.ContainsKey(currentSceneName))
        {
            npcIconData.allScenes[currentSceneName] = new List<NPCIconData>();
        }

        NPCIconData data = new NPCIconData();
        data.ID = id;

        npcIconData.allScenes[currentSceneName].Add(data);

        string saveData = JsonConvert.SerializeObject(npcIconData);
        File.WriteAllText(saveFilePath, saveData);

        Debug.Log("Saved NPC Icon Data: " + saveData);
        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void GetDataFromFile()
    {
        if (File.Exists(saveFilePath))
        {
            string loadData = File.ReadAllText(saveFilePath);
            npcIconData = JsonConvert.DeserializeObject<AllScenesNPCIcons>(loadData);
        }
        else
        {
            Debug.Log("There is no save file to load! (NPC Icons)");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadNPCIcons();
    }
    
    public void LoadNPCIcons()
    {
        GetDataFromFile();
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (npcIconData != null && npcIconData.allScenes.ContainsKey(currentSceneName))
        {
            List<NPCIconData> savedNPCIcons = npcIconData.allScenes[currentSceneName];
            NPCIcon[] npcIcons = FindObjectsOfType<NPCIcon>();

            foreach (NPCIcon npcIconScript in npcIcons)
            {
                foreach (NPCIconData data in savedNPCIcons)
                {
                    if (data.ID == npcIconScript.id)
                    {
                        npcIconScript.Disable();
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("No NPC icon data available for this scene.");
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
}
