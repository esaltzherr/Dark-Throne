using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string sceneToLoad = "ElroyScene2";
    public string spawnPointId = "123456789"; // Ensure this matches a spawnPointId in the scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the ID in SpawnManager
            SpawnManager.SetId(this.spawnPointId);
            
            // Load the next scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
