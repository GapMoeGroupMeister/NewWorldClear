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

    private bool isSoldOut = false;

    public GameObject SoldOut { get { return soldOut; } }
    public bool IsSoldOut { get { return isSoldOut; } set { isSoldOut = value; } }
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
        if (isSoldOut)
            descriptionText.text = "이미 구매한 무기입니다!";
        else
            descriptionText.text = weaponDescription.description;
        descriptionScript.Weapon2 = this;
    }
}

[System.Serializable]
public class WeaponDescription2
{
    public string name;
    [TextArea(3, 5)]
    public string description;
}
