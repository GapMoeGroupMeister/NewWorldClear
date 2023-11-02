using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponShop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject soldOut;
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Shop shop;
    [SerializeField] private Ease ease;

    [SerializeField] private float duration;
    [SerializeField] private string weaponName;

    bool isSoldOut = false;

    private void Start()
    {
        description.gameObject.transform.localScale = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSoldOut) return;

        soldOut.SetActive(true);
        isSoldOut = true;
        print("Sold Out");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(shop.IsMoving) return;

        description.SetActive(true);
        description.transform.DOScale(1f, duration);
        descriptionText.text = weaponName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(shop.IsMoving) return;

        description.transform.DOScale(0f, duration).SetEase(ease).OnComplete(() => description.SetActive(false));
    }
}
