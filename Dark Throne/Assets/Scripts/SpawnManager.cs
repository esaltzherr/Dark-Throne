using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static string previousSceneName = "";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the spawn point that corresponds to the previous scene.
        GameObject spawnPoint = GameObject.Find(previousSceneName + "_Spawn");
        if (spawnPoint == null){
            spawnPoint = GameObject.Find("Anything_Spawn");
        }
        if (spawnPoint != null)
        {
            // Find the player and move them to the spawn point.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) // Check if the player exists.
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when the spawn manager is destroyed.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
