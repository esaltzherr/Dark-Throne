using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string sceneToLoad = "ElroyScene2";
    public string spawnPointId = "123456789"; // Ensure this matches a spawnPointId in the scene
    public SaveLoadJSONEnemies enemyJSON;

    void Start()
    {
        // Try to find the SaveLoadJSONEnemies component automatically at startup
        enemyJSON = FindObjectOfType<SaveLoadJSONEnemies>();

        // Check if the component was found
        if (enemyJSON == null)
        {

            Debug.LogError("SaveLoadJSONEnemies component not found in the scene. Please attach it to an active GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyJSON == null)
            {
                enemyJSON = FindObjectOfType<SaveLoadJSONEnemies>();
                if (enemyJSON == null)
                {
                    Debug.LogError("SaveLoadJSONEnemies component is still not found in SceneWalk.");
                    return;  // Stop execution if still not found
                }
            }

            enemyJSON.SaveGame(); // Call SaveGame on the referenced component
            SpawnManager.SetId(this.spawnPointId);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
