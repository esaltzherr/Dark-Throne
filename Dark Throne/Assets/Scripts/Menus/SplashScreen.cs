using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public GameObject logo;
    
    // Start is called before the first frame update
    void Start()
    {
        FadeLogoIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeLogoIn()
    {
        var spriteRender = logo.GetComponent<SpriteRenderer>();
        spriteRender.DOFade(1, 2).OnComplete(FadeLogoOut);
    }

    private void FadeLogoOut()
    {
        var spriteRender = logo.GetComponent<SpriteRenderer>();
        spriteRender.DOFade(0, 2).OnComplete(ChangeScenes);
    }

    private void ChangeScenes()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
