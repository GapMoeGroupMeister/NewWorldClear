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
        OnImageChanged();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (descriptionScript == null)
            descriptionScript = FindObjectOfType<Description>();
        if (descriptionScript.IsOn) return;

        description.SetActive(true);
        description.transform.DOScale(1f, 0.5f);

        nameText.text = weaponDescription[_randomIndex].name;
        descriptionText.text = weaponDescription[_randomIndex].isSoldOut ? "�̹� ������ �����Դϴ�!" : weaponDescription[_randomIndex].description;
        descriptionScript.Weapon2 = this;
    }

    /**
     * <summary>
     * ���⸦ �������� �ٲٱ� ���� �޼���
     * </summary>
     */
    public void OnImageChanged()
    {
        _randomIndex = Random.Range(0, WeaponDescription.Length);
        
        weaponImage.sprite = weaponDescription[_randomIndex].weaponIcon;
        weaponImage.SetNativeSize();
        weaponNameText.text = weaponDescription[_randomIndex].name + "\n<size=25>" + weaponDescription[_randomIndex].price + "��</size>";

        foreach (var item in weaponDescription)
        {
            item.isSoldOut = false;
        }
        soldOut.SetActive(false);


    }
}

/** <summary>
 * ���� ���� Ŭ����
 * </summary>
 */
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