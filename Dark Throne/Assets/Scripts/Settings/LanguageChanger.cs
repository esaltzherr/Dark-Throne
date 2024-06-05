using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChanger : MonoBehaviour
{
    public static int CurrentLang;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delay", 0.1f);     
    }

    private void Delay(){
        //
        // int number = PlayerPrefs.GetInt("Lang");
        // ChangLang(number);
    }

    // Update is called once per frame
    public void ChangLang(int lang)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lang];
        CurrentLang = lang;
        // PlayerPrefs.SetInt("Lang", lang);
        // Debug.Log("changlang" + PlayerPrefs.GetInt("Lang"));
    }
}
