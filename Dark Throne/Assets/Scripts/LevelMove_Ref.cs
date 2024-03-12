using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    
    bool playerInDoor = false;
    public string nextScene;
      
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerInDoor)
        {
            Debug.Log("NEXT Level");
            SceneManager.LoadScene(nextScene);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerInDoor = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        playerInDoor = false;
    }

}

