using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();
        // AskForConsent();
        ConsentGiven();
    }

    // void AskForConsent()
    // {
    //     // TODO: Implement a proper UI to ask for player consent
    //     // For demonstration purposes, we assume consent is given:
    //     ConsentGiven();
    // }

    void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
    }

    public void OnLevelComplete(string transitionID)
    {
        
        

#if ENABLE_CLOUD_SERVICES_ANALYTICS

    
        CustomEvent LevelComplete = new CustomEvent("LevelComplete")
        {
            { "ID", transitionID },
            { "Time", 1000 }
        };
        AnalyticsService.Instance.RecordEvent(LevelComplete);

        // Analytics.CustomEvent("LevelComplete", new Dictionary<string, object>
        // {
        //     { "ID", transitionID },
        //     { "time", 1000 }
        // });
        // Debug.Log("CLOUD SERIVES INDEED ON");
#endif
        Debug.Log("Level complete event attempted to send.");
    }
}
