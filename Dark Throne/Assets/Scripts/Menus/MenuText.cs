using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;


public class MenuText : MonoBehaviour
{
    public LocalizeStringEvent localizeStringEvent;
    public TMP_FontAsset[] tmpFontAssets;
    
    // Start is called before the first frame update
    void Start()
    {
        // todo: make it so the font changes when a localization event occurs.
        // localizeStringEvent.OnUpdateString.AddListener(OnStringChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStringChanged(string s)
    {
        var tmp = gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(PlayerPrefs.GetInt("Lang"));
        tmp.font = tmpFontAssets[LanguageChanger.CurrentLang];
    }

    public void UpdateFont()
    {
        var tmp = gameObject.GetComponent<TextMeshProUGUI>();
        // Debug.Log(PlayerPrefs.GetInt("Lang"));
        tmp.font = tmpFontAssets[LanguageChanger.CurrentLang];
    }
}
