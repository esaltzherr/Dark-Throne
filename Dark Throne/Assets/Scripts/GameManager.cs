using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private bool isPaused = false;


    // Update is called once per frame
    void Start()
    {
        Unpause();
    }
    
    void Update()
    {
        // Check for the pause button (Escape key in this case)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // Optionally, display a pause menu when the game is paused
        if (isPaused)
        {
            // Code to display your pause menu
        }
        else
        {
            // Code to hide your pause menu
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = isPaused ? 0 : 1;

    }
    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
