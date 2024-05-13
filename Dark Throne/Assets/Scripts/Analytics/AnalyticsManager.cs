using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

using UnityEngine.Analytics;

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


    public void AquiredCheckpoint(string ID)
    {
        CustomEvent checkpoint = new CustomEvent("Checkpoint")
        {
            { "ID", ID },
        };
        AnalyticsService.Instance.RecordEvent(checkpoint);
        Debug.Log("Event Saved");
    }

    public void ExecutedEnemy()
    {
        CustomEvent enemies = new CustomEvent("ExecuteEnemy")
        {   
            // will probably be swapped to a "exit" and then track the amount
            { "Enemies", 1 },
        };
        AnalyticsService.Instance.RecordEvent(enemies);
        Debug.Log("Event Saved");
    }


    public void DashGainEvent()
    {
        CustomEvent dash = new CustomEvent("DashGainEvent")
        {   
            // Dunno if a time needs to be saved, cause it has it, probably will make a counter from start of game though eventually
            { "Time", 1000 },
        };
        AnalyticsService.Instance.RecordEvent(dash);
        Debug.Log("Event Saved");
    }

    public void PlayerDeathEvent()
    {
        CustomEvent death = new CustomEvent("PlayerDeathEvent")
        {   
            // Dunno if a time needs to be saved, cause it has it, probably will make a counter from start of game though eventually
            { "Time", 1000 },
        };
        AnalyticsService.Instance.RecordEvent(death);
        Debug.Log("Event Saved");
    }
}
