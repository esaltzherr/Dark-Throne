using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public GameObject backButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ResetPauseMenu()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
        backButton.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ExitToMenu()
    {
        // var scene = SceneManager.GetActiveScene();
        // SceneManager.LoadSceneAsync("MainMenu");
        // SceneManager.UnloadSceneAsync(scene);
    }

}
