using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Weapon2 : MonoBehaviour, IPointerClickHandler
{
    [Header("GameObjects")]
    [SerializeField] private GameObject soldOut;
    [SerializeField] private GameObject description;
    [Space]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [Space]
    [Header("Weapon Description")]
    [SerializeField] private Description descriptionScript;
    [SerializeField] private WeaponDescription2 weaponDescription;
    
    [SerializeField] private Image weaponImage;

    public GameObject SoldOut => soldOut;
    public WeaponDescription2 WeaponDescription => weaponDescription;

    private void Start()
    {
        description.gameObject.transform.localScale = Vector3.zero;
        weaponNameText.text = weaponDescription.name + "\n<size=25>" + weaponDescription.price + "원</size>";
    }

    private void Update()
    {
        weaponImage.SetNativeSize();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (descriptionScript == null)
            descriptionScript = FindObjectOfType<Description>();
        if (descriptionScript.IsOn) return;

        description.SetActive(true);
        description.transform.DOScale(1f, 0.5f);

        nameText.text = weaponDescription.name;
        descriptionText.text = weaponDescription.isSoldOut ? "이미 구매한 무기입니다!" : weaponDescription.description;
        
        descriptionScript.Weapon2 = this;
    }
}

/// <summary>
/// 무기 설명
/// </summary>
[System.Serializable]
public class WeaponDescription2
{
    public string name;
    [TextArea(3, 5)]
    public string description;
    public int price;
    public bool isSoldOut;
}
