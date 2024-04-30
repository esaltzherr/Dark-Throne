using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{

    public GameObject[] objectsToShow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleActive(GameObject otherGameObject)
    {
        otherGameObject.SetActive(!otherGameObject.activeSelf);
    }

    // public void HideObject(GameObject gameObject)
    // {
    //     gameObject.SetActive(false);
    //     Debug.Log(gameObject.activeSelf);
    // }

    public void SetBackButtonFunctionality(GameObject hide, GameObject show)
    {
        
    }
}
