using UnityEngine;
using UnityEngine.SceneManagement; // Import for SceneManager

public class winGame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object the script is colliding with has the "Player" tag
        if (other.gameObject.tag == "Player")
        {
            // Call the function to trigger the win condition
            TriggerWin();
        }
    }

    void TriggerWin()
    {
         // Load the win and credits scene
        SceneManager.LoadScene("WinAndCredits");
        // Add your win condition logic here
        Debug.Log("You win!");

        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // if (player != null)
        // {
        //     Destroy(player);
        //     Debug.Log("Player object destroyed.");
        // }
        // else
        // {
        //     Debug.LogError("Player object not found!");
        // }
        
       
    }
}
