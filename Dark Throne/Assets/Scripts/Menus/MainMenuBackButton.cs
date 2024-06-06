using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackButton : MonoBehaviour
{
    public GameObject ObjectToHide { get; set; }
    public GameObject ObjectToShow { get; set; }

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
        ObjectToHide.SetActive(false);
        ObjectToShow.SetActive(true);
    }
}
