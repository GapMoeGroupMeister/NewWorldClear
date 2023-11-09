using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [SerializeField] private WeaponDescription2[] weaponDescription;
    
    [SerializeField] private Image weaponImage;
    
    private int _randomIndex;

    public GameObject SoldOut => soldOut;
    public WeaponDescription2[] WeaponDescription => weaponDescription;
    public int RandomIndex => _randomIndex;

    

    private void Start()
    {
        _randomIndex = Random.Range(0, WeaponDescription.Length);
        description.gameObject.transform.localScale = Vector3.zero;
        WeaponSetup();
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

        nameText.text = weaponDescription[_randomIndex].name;
        descriptionText.text = weaponDescription[_randomIndex].isSoldOut ? "이미 구매한 무기입니다!" : weaponDescription[_randomIndex].description;
        descriptionScript.Weapon2 = this;
    }

    [ContextMenu("Test/Change Image")]
    private void OnImageChanged()
    {
        _randomIndex = Random.Range(0, WeaponDescription.Length);
        WeaponSetup();
        weaponImage.SetNativeSize();
        foreach (var item in weaponDescription)
        {
            item.isSoldOut = false;
        }
        soldOut.SetActive(false);
    }

    private void WeaponSetup()
    {
        weaponImage.sprite = weaponDescription[_randomIndex].weaponIcon;
        weaponNameText.text = weaponDescription[_randomIndex].name + "\n<size=25>" + weaponDescription[_randomIndex].price + "원</size>";
    }
}

/// <summary>
/// 무기 설명
/// </summary>
[Serializable]
public class WeaponDescription2
{
    public string name;
    [TextArea(3, 5)]
    public string description;
    public int price;
    public bool isSoldOut;
    public Sprite weaponIcon;
}
