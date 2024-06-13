using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;


public class CustomTextLocalizer : MonoBehaviour
{
    public SaveLoadJSONPlayer playerJSONData;
    
    public LocalizeStringEvent localizeStringEvent;

    // public TableReference tableReference;
    public LocalizedString startGameStrID;  // Text that contains the start game text
    public LocalizedString loadGameStrID;  // text that contains load game text

    // Start is called before the first frame update
    void Start()
    {
        // todo: make it so the font changes when a localization event occurs.
        // localizeStringEvent.OnUpdateString.AddListener(OnStringChanged);
        UpdateString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        Debug.Log("woooot");
        UpdateString();
    }

    public void UpdateString()
    {
        // Debug.Log("current lang: " + LocalizationSettings.SelectedLocale.name);
        // Debug.Log("current lang: " + LocalizationSettings.SelectedLocale.LocaleName);
        // Debug.Log("current lang: " + LocalizationSettings.SelectedLocale.SortOrder);
        // Debug.Log("current lang: " + LocalizationSettings.SelectedLocale.Formatter);
        // Debug.Log(stringTableCollection.GetTable(LocalizationSettings.SelectedLocale.Identifier));
        // var table = LocalizationSettings.StringDatabase.GetTable(tableReference);
        Debug.Log(playerJSONData.hasSaveData());
        if (playerJSONData.hasSaveData())
        {
            gameObject.GetComponent<LocalizeStringEvent>().StringReference.SetReference(loadGameStrID.TableReference, loadGameStrID.TableEntryReference);
        }
        else
        {
            gameObject.GetComponent<LocalizeStringEvent>().StringReference.SetReference(startGameStrID.TableReference, startGameStrID.TableEntryReference);
        }
    }
}
