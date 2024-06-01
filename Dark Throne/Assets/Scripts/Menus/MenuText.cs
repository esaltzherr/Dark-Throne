using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class MenuText : MonoBehaviour
{
    public LocalizeStringEvent localizeStringEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        // todo: make it so the font changes when a localization event occurs.
        localizeStringEvent.OnUpdateString.AddListener(OnStringChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStringChanged(string s)
    {
        
    }
}
