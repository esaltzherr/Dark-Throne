using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    bool playerInDoor = false;
    public string nextScene;
    public Canvas doorCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInDoor)
        {
            Debug.Log("NEXT Level");
            // Update the previous scene name before transitioning.
            SpawnManager.previousSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(nextScene);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoor = true;
            doorCanvas.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoor = false;
            doorCanvas.enabled = false;

        }
    }
}
