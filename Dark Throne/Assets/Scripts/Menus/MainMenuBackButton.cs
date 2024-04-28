using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackButton : MonoBehaviour
{
    public GameObject HideMe { get; set; }
    public GameObject ShowMe { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack()
    {
        HideMe.SetActive(false);
        ShowMe.SetActive(true);
    }
}
