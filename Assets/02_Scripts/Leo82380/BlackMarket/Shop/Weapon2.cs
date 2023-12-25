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

    [SerializeField] private Image[] frameImages;
    [SerializeField] private Sprite[] frameSprites;
    
    
    private int _randomIndex;
    private Image _image;
    private ShopSO nowShopSO;

    public GameObject SoldOut => soldOut;
    public WeaponDescription2[] WeaponDescription => weaponDescription;
    public int RandomIndex => _randomIndex;


    private void Start()
    {
        _randomIndex = Random.Range(0, WeaponDescription.Length);
        description.gameObject.transform.localScale = Vector3.zero;
        OnImageChanged();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (descriptionScript == null)
            descriptionScript = FindObjectOfType<Description>();
        if (descriptionScript.IsOn) return;

        description.SetActive(true);
        description.transform.DOScale(1f, 0.5f);

        nameText.text = weaponDescription[_randomIndex].shopSO.itemName;
        descriptionText.text = weaponDescription[_randomIndex].isSoldOut ? "이미 구매한 아이템입니다!" : weaponDescription[_randomIndex].shopSO.description;
        descriptionScript.Weapon2 = this;
        nowShopSO = weaponDescription[_randomIndex].shopSO;
    }
    
    public WeaponDescription2 GetWeaponDescription()
    {
        return weaponDescription[_randomIndex];
    }
    
    

    /**
     * <summary>
     * 무기를 랜덤으로 바꾸기 위한 메서드
     * </summary>
     */
    public void OnImageChanged()
    {
        _randomIndex = Random.Range(0, WeaponDescription.Length);

        weaponImage.sprite = weaponDescription[_randomIndex].shopSO.itemIcon;
        weaponImage.SetNativeSize();
        weaponNameText.text = weaponDescription[_randomIndex].shopSO.itemName + "\n<size=25>" + weaponDescription[_randomIndex].shopSO.price + "원</size>";

        switch (weaponDescription[_randomIndex].shopSO.grade)
        {
            case 1:
                foreach (var item in frameImages)
                {
                    item.sprite = frameSprites[0];
                }
                break;
            case 2:
                foreach (var item in frameImages)
                {
                    item.sprite = frameSprites[1];
                }
                break;
            case 3:
                foreach (var item in frameImages)
                {
                    item.sprite = frameSprites[2];
                }
                break;
            case 4:
                foreach (var item in frameImages)
                {
                    item.sprite = frameSprites[3];
                }
                break;
            default:
                throw new Exception("잘못된 등급입니다");
        }

        foreach (var item in weaponDescription)
        {
            item.isSoldOut = false;
        }
        soldOut.SetActive(false);
    }
}

/**
 * <summary>
 * 무기 설명 클래스
 * </summary>
 */
[Serializable]
public class WeaponDescription2
{
    public ShopSO shopSO;
    public bool isSoldOut;
}
