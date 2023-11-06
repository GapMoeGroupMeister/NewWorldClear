using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class Weapon2 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject soldOut;
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Description descriptionScript;
    [SerializeField] private WeaponDescription2 weaponDescription;

    public GameObject SoldOut { get { return soldOut; } }
    public WeaponDescription2 WeaponDescription { get { return weaponDescription; } }
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

        if (weaponDescription.isSoldOut)
            descriptionText.text = "�̹� ������ �����Դϴ�!";
        else
            descriptionText.text = weaponDescription.description;
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
