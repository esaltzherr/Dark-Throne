using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection; // Add this at the top of your file
using Newtonsoft.Json;


[System.Serializable]
public class EnemyData
{
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 position;
    public int health;
}



[System.Serializable]
public class AllScenesEnemies
{
    public Dictionary<string, List<EnemyData>> allScenes = new Dictionary<string, List<EnemyData>>();
}



public class SaveLoadJSONEnemies : MonoBehaviour
{
    AllScenesEnemies enemyData;
    string saveFilePath;

    void Awake()
    {
        enemyData = new AllScenesEnemies();
        saveFilePath = Application.persistentDataPath + "/EnemyData.json";
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // void Start()
    // {
        
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.X))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.C))
            DeleteSaveFile();
    }

    public void SaveGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;


        // re-get all the data so we dont lose the other scenes
        GetDataFromFile();

        if (!enemyData.allScenes.ContainsKey(currentSceneName))
        {
            enemyData.allScenes[currentSceneName] = new List<EnemyData>();
        }
        else
        {
            enemyData.allScenes[currentSceneName].Clear();
        }


        getEnemies(currentSceneName);


        string saveEnemyData = JsonConvert.SerializeObject(enemyData);

        // Write JSON data to file
        File.WriteAllText(saveFilePath, saveEnemyData);

        Debug.Log("Saved Enemy Data: " + saveEnemyData);
        Debug.Log("Save file created at: " + saveFilePath);
    }


    public void GetDataFromFile()
    {
        Debug.Log("Path:" + saveFilePath);
        if (File.Exists(saveFilePath))
        {
            // Load data from JSON file
            string loadData = File.ReadAllText(saveFilePath);

            // Deserialize the JSON data into the AllScenesEnemies object
            enemyData = JsonConvert.DeserializeObject<AllScenesEnemies>(loadData);
        }
        else
        {
            Debug.Log("There is no save file to load! (Enemies)");
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (saveFilePath == null)
        {
            Debug.LogError("Save file path is not set.");
            return;
        }
        Debug.Log("Scene loaded: " + scene.name);
        // You can execute any additional setup code here
        LoadGame();
    }
    public GameObject enemyPrefab; // Assign this in the Unity Editor

    public void LoadGame()
    {

        GetDataFromFile();

        //



        // Spawn the enemies
        if (enemyData != null && enemyData.allScenes.ContainsKey(SceneManager.GetActiveScene().name))
        {
            Debug.Log("Cleared Enemies");
            ClearCurrentEnemies();

            List<EnemyData> currentSceneEnemies = enemyData.allScenes[SceneManager.GetActiveScene().name];


            foreach (EnemyData enemy in currentSceneEnemies)
            {
                if (enemyPrefab != null)
                {
                    // Instantiate the enemy at the loaded position
                    if (enemy.health <= 0)
                    {
                        continue;
                    }
                    GameObject spawnedEnemy = Instantiate(enemyPrefab, enemy.position, Quaternion.identity);

                    // Assuming the prefab has an EnemyHealth component to set its health
                    EnemyHealth enemyHealthScript = spawnedEnemy.GetComponent<EnemyHealth>();
                    if (enemyHealthScript != null)
                    {
                        // Set the health of the instantiated enemy
                        enemyHealthScript.setHealth(enemy.health);
                    }
                    else
                    {
                        Debug.LogError("EnemyHealth script not found on the enemy prefab!");
                    }
                }
                else
                {
                    Debug.LogError("Enemy prefab is not assigned!");
                }
            }
        }

    }

    void ClearCurrentEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");


        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }



    public void printFile()
    {
        if (enemyData != null && enemyData.allScenes.Count > 0)
        {
            // Iterate through each scene's enemy data
            foreach (var scene in enemyData.allScenes)
            {
                string sceneName = scene.Key;
                List<EnemyData> enemies = scene.Value;

                Debug.Log($"Scene: {sceneName}");
                foreach (EnemyData enemy in enemies)
                {
                    Debug.Log($"  Health: {enemy.health}, Position: {enemy.position}");
                }
            }
        }
        else
        {
            Debug.Log("No enemy data found in loaded save file.");
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
            Debug.Log("There is nothing to delete!");
    }


    // public void getScripts()
    // {
    //     EnemyHealth enemyHealthScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();

    // }
    public void getEnemies(string sceneName)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Cache the EnemyHealth script (assuming there's only one instance)

        foreach (GameObject enemy in enemies)
        {
            // Check if script has already been retrieved

            EnemyHealth enemyHealthScript = enemy.GetComponent<EnemyHealth>();

            EnemyData tempData = new EnemyData();
            int currentHealth = enemyHealthScript.getHealth();
            tempData.health = currentHealth; // Store retrieved health
            tempData.position = enemy.transform.position; // Use actual enemy position

            enemyData.allScenes[sceneName].Add(tempData);
        }
    }



}