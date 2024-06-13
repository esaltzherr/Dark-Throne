using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection; // Add this at the top of your file

public class PlayerData
{
    public int maxHearts;
    public int hearts;
    public Vector3 position;
    public string sceneName;
    public bool dashAquired;
    public bool doubleJumpAquired;
    public string ID;
}

public class SaveLoadJSONPlayer : MonoBehaviour
{
    PlayerData playerData;
    string saveFilePath;

    private PlayerPowerUps playerPowerUpsScript;
    private PlayerDash playerDashScript;
    private PlayerHealth2 playerHealthScript;

    public GameObject player; // Assign this in the Unity Editor


    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player is tagged correctly
        }
        if (player != null)
        {
            getScripts();
        }
        // getScripts();

        // place the data into the object
        playerData = new PlayerData();

        // getData();
        // save it
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.J))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.K))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.L))
            DeleteSaveFile();
        */
    }


    public void SaveGame()
    {

        getData();
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);
        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                // Instantiate a new player if not found
                GameObject playerPrefab = Resources.Load<GameObject>("Player"); // Assumes a player prefab named "PlayerPrefab" is in a Resources folder
                if (playerPrefab != null)
                {
                    Vector3 spawnPosition = new Vector3(-10, -61, 0);
                    player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
                    player.tag = "Player"; // Ensure the instantiated player has the correct tag
                }
                else
                {
                    Debug.LogError("PlayerPrefab not found in Resources!");
                    return;
                }
            }
            getScripts();
        }

        if (File.Exists(saveFilePath))
        {
            // Gets the data and places it into the format
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            printFile();

            useData();
            useCheckpoint();
        }
        else
        {
            Debug.Log("There is no save files to load! (Player)");
        }
    }

    public void Respawn()
    {
        if (File.Exists(saveFilePath))
        {
            // Gets the data and places it into the format
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            printFile();

            useDataFullHealth();
            useCheckpoint();
        }
        else
        {
            Debug.Log("There is no save files to load! (Player)");
        }
    }

    public void printFile()
    {
        FieldInfo[] fields = typeof(PlayerData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        string output = "Load game complete! \n";
        foreach (FieldInfo field in fields)
        {
            output += field.Name + ": " + field.GetValue(playerData) + ", ";
        }

        Debug.Log(output.TrimEnd(',', ' ')); // Removes the last comma and space
    }

    public void DeleteSaveFile()
    {
        playerData = new PlayerData();
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }

    public void getScripts()
    {
        if (player != null)
        {
            playerPowerUpsScript = player.GetComponent<PlayerPowerUps>();
            playerDashScript = player.GetComponent<PlayerDash>();
            playerHealthScript = player.GetComponent<PlayerHealth2>();
        }
        else
        {
            Debug.LogError("Player object not set or found!");
        }
    }

    public void getData()
    {
        // if (player == null)
        // {
        //     player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player is tagged correctly

        //     if (player == null)
        //     {
        //         Debug.LogWarning("Player object is null!");
        //         return;
        //     }
        // }
        Debug.Log("Thing:" + SceneManager.GetActiveScene().name);

        if (playerData == null)
        {
            Debug.LogWarning("WENT INTO");
            playerData = new PlayerData();
        }

        playerData.sceneName = SceneManager.GetActiveScene().name;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player is tagged correctly
            if (player != null)
            {
                playerData.position = player.transform.position;
            }
        }



        if (playerHealthScript != null)
        {
            playerData.hearts = playerHealthScript.getHearts();
            playerData.maxHearts = playerHealthScript.getMaxHearts();
        }
        else
        {
            Debug.LogWarning("PlayerHealth2 script is null!");
        }

        if (playerDashScript != null)
        {
            playerData.dashAquired = playerDashScript.dashGained();
        }
        else
        {
            Debug.LogWarning("PlayerDash script is null!");
        }

        if (playerPowerUpsScript != null)
        {
            playerData.doubleJumpAquired = playerPowerUpsScript.doubleJumpGained();
        }
        else
        {
            Debug.LogWarning("PlayerPowerUps script is null!");
        }

        playerData.ID = SpawnManager.GetId();
        Debug.Log("Player ID: " + playerData.ID);
    }

    public void useData()
    {
        playerHealthScript.setMaxHearts(playerData.maxHearts);
        playerHealthScript.setHearts(playerData.hearts);
        playerDashScript.setDashGained(playerData.dashAquired);
        playerPowerUpsScript.setDoubleJumpGained(playerData.doubleJumpAquired);
    }

    public void useDataFullHealth()
    {
        playerHealthScript.setMaxHearts(playerData.maxHearts);
        playerHealthScript.setHearts(playerData.maxHearts);
        Debug.Log("GAINGNIGNNGNGNN DASHSHSHHSHSHSH");
        playerDashScript.setDashGained(playerData.dashAquired);
        playerPowerUpsScript.setDoubleJumpGained(playerData.doubleJumpAquired);
    }

    public void useCheckpoint()
    {
        Debug.Log("TELEOPRTING TO id" + playerData.ID);
        SpawnManager.SetId(playerData.ID);
        SceneManager.LoadScene(playerData.sceneName);
    }

    public void teleportToCheckpoint(string id, string sceneName, Vector3 position)
    {
        useData();
        SpawnManager.SetId(id);
        SceneManager.LoadScene(sceneName);
        player.transform.position = position;
    }
    public bool hasSaveData()
    {  
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(saveFilePath))
        {
            return true;
        }
        return false;
    }
}
