using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float _originalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = gameObject.GetComponent<RectTransform>().position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // MoveRight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // MoveLeft();
    }

    public void MoveRight()
    {
        var rectTransform = this.gameObject.GetComponent<RectTransform>();
        var currentX = rectTransform.position.x;
        rectTransform.DOMoveX(_originalPos + 5, 1);
    }

    public void MoveLeft()
    {
        var rectTransform = this.gameObject.GetComponent<RectTransform>();
        var currentX = rectTransform.position.x;
        rectTransform.DOMoveX(_originalPos, 1);
    }
}
