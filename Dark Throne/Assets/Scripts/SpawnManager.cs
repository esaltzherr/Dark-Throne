using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; } // Singleton instance

    public static string id = "";
    public static string lastLevelScene = "";
    public static string previousSceneName; // to be deleted when LevelMoveRef is deleted

    private void Awake()
    {
        if (Instance == null)
        {
            // If there is no instance, this becomes the singleton instance of the SpawnManager
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // A SpawnManager instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        lastLevelScene = SceneManager.GetActiveScene().name;

        // Attempt to find a spawn point with a matching ID.
        SceneWalk[] spawnPoints = FindObjectsOfType<SceneWalk>();
        Transform targetSpawnPointTransform = null;
        foreach (var point in spawnPoints)
        {
            if (point.spawnPointId == id)
            {
                // Assuming each point.gameObject has a child named "SpawnPoint"
                Transform childTransform = point.gameObject.transform.Find("SpawnPoint");
                if (childTransform != null)
                {
                    targetSpawnPointTransform = childTransform;
                    // Debug.Log("Found spawn point: " + targetSpawnPointTransform.name);
                }
                else
                {
                    Debug.LogError("No 'SpawnPoint' child found under " + point.gameObject.name);
                }
                break; // Found the matching spawn point
            }
        }

        // If a matching spawn point (child) is found, move the player to it.
        if (targetSpawnPointTransform != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = targetSpawnPointTransform.position;
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // Method to explicitly set the ID.
    public static void SetId(string newId)
    {
        id = newId;
    }
}
