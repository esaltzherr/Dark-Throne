using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public GameObject logo;
    
    // Start is called before the first frame update
    void Start()
    {
        Tween();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tween()
    {
        var spriteRender = logo.GetComponent<SpriteRenderer>();
        spriteRender.DOFade(0, 5);
    }
}
