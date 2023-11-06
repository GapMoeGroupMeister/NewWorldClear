using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class Weapon2 : MonoBehaviour, IPointerClickHandler
{
    [Header("GameObjects")]
    [SerializeField] private GameObject soldOut;
    [SerializeField] private GameObject description;
    [Space]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [Space]
    [Header("Weapon Description")]
    [SerializeField] private Description descriptionScript;
    [SerializeField] private WeaponDescription2 weaponDescription;

    public GameObject SoldOut => soldOut;
    public WeaponDescription2 WeaponDescription => weaponDescription;

    private void Start()
    {
        description.gameObject.transform.localScale = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (descriptionScript == null)
            descriptionScript = FindObjectOfType<Description>();
        if (descriptionScript.IsOn) return;

        description.SetActive(true);
        description.transform.DOScale(1f, 0.5f);

        nameText.text = weaponDescription.name;
        descriptionText.text = weaponDescription.isSoldOut ? "�̹� ������ �����Դϴ�!" : weaponDescription.description;
        
        descriptionScript.Weapon2 = this;
    }
}

/// <summary>
/// ���� ����
/// </summary>
[System.Serializable]
public class WeaponDescription2
{
    public string name;
    [TextArea(3, 5)]
    public string description;
    public bool isSoldOut;
}
