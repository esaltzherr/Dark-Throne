using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{

    public GameObject[] objectsToShow;
    
    // Start is called before the first frame update
    void Start()
    {
        StartAsync();
    }

    async void StartAsync()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SendAnalyticEvent()
    {
        CustomEvent ligma = new CustomEvent("deez")
        {
            { "ligma", "whats ligma" }
        };
        AnalyticsService.Instance.RecordEvent(ligma);
        AnalyticsService.Instance.Flush();
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

    public void PlaySFX(AudioSource audioSource)
    {
        Debug.Log(audioSource);
        audioSource.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
