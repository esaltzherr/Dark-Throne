using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Utility : MonoBehaviour
{

    public GameObject[] objectsToShow;
    public AudioMixer audioMixer;
    
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

    public void PlaySFX(AudioSource audioSource)
    {
        Debug.Log(audioSource);
        // audioSource.Play();
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVol", MathF.Log10(value) * 20);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
