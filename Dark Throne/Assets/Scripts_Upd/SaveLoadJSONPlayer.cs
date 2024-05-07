using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Reflection; // Add this at the top of your file

public class PlayerData
{
    public int maxHealth;
    public int health;
    public Vector3 position;
    public string sceneName;
    // public Vector3 positionOfLastCheckpoint;
    public bool dashAquired;
    public bool doubleJumpAquired;
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
        player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player is tagged correctly

        getScripts();

        // place the data into the object
        playerData = new PlayerData();


        getData();
        // save it
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.K))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.L))
            DeleteSaveFile();
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
        if (File.Exists(saveFilePath))
        {
            // Gets the data and places it into the format
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);


            printFile();


            useData();
            usePosition();
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
        playerData.sceneName = SceneManager.GetActiveScene().name;
        playerData.position = player.transform.position;
        playerData.health = playerHealthScript.getHealth();
        playerData.maxHealth = playerHealthScript.getMaxHealth();
        playerData.dashAquired = playerDashScript.dashGained();
        playerData.doubleJumpAquired = playerPowerUpsScript.doubleJumpGained();
    }

    public void useData()
    {
        playerHealthScript.setMaxHealth(playerData.maxHealth);
        playerHealthScript.setHealth(playerData.health);
        playerDashScript.setDashGained(playerData.dashAquired);
        playerPowerUpsScript.setDoubleJumpGained(playerData.doubleJumpAquired);
    }
    public void usePosition()
    {
        SpawnManager.SetId("GettingRidOfID");
        SceneManager.LoadScene(playerData.sceneName);
        player.transform.position = playerData.position;
    }

    public void teleportToCheckpoint(string id, string sceneName, Vector3 position){
        useData();
        SpawnManager.SetId(id);
        SceneManager.LoadScene(sceneName);
        player.transform.position = position;
    }
}