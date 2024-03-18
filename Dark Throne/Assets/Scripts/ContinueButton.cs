using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ContinueButton : MonoBehaviour
{
    //public string Scenename;
    public Canvas canvasOne;
    public Canvas canvasTwo;
    private void Start()
    {
        canvasOne.enabled = true;
        canvasTwo.enabled = false;
    }

    public void ChangeToNextCanvas()
    {
        canvasTwo.enabled = true;
        canvasOne.enabled = false;
        
    }

    public void changeScenes(string scene)
    {
        SceneManager.LoadScene(scene);
        canvasTwo.enabled = false;
    }
}
