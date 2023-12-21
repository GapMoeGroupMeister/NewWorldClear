using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_SatrtSceneButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private GameObject buttons;
    
    private bool isClicked = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClicked)
        {
            isClicked = true;
            buttons.transform.DOMoveY(0, duration).SetEase(easeType);
            transform.DOMoveY(-2.6f, duration).SetEase(easeType);
        }
        else
        {
            isClicked = false;
            buttons.transform.DOMoveY(-2.1f, duration).SetEase(easeType);
            transform.DOMoveY(-4.7f, duration).SetEase(easeType);
        }
    }
}
