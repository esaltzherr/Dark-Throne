using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public string ContinueScene, GiveUpScene;
    void Start()
    {
        // This will set the scene to return to as the one last played
        if (SpawnManager.lastLevelScene != "")
        {
            ContinueScene = SpawnManager.lastLevelScene;
        }
        else
        {
            ContinueScene = GiveUpScene;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(ContinueScene);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {

            SceneManager.LoadScene(GiveUpScene);
        }
    }
}
