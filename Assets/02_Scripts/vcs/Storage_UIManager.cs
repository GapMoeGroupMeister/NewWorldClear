using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Tkfkadlsi;
using UnityEditor;
using UnityEngine.Serialization;


public class Storage_UIManager : MonoSingleton<Storage_UIManager>
{
    private Sequence seq;
    [SerializeField] private UIInfo UI_Description;

    [SerializeField] private TextMeshProUGUI Text_ItemType;
    [SerializeField] private TextMeshProUGUI Text_ItemName;
    [SerializeField] private GameObject durabilityGaugeObject;
    [SerializeField] private Image DurabilityGauge;
    [SerializeField] private TextMeshProUGUI Text_Description;
    [SerializeField] private Gradient DurabilityColorGradient;
    
    [SerializeField] private GameObject eatButton;
    [SerializeField] private GameObject foodDescription;

    public void On_DescriptionUI()
    {
        UI_Description.MoveOn();

    }

    public void Off_DescriptionUI()
    {
        UI_Description.MoveOff();

    }

    public void Refresh_DescriptionUI(ItemSlot slot)
    {
        Text_ItemType.text = "["+ItemTypeConvert(slot.item.Type)+"]";
        Text_ItemName.text = slot.item.itemName;
        Text_Description.text = slot.item.description;
        if (slot.item.isLimited)
        {
            durabilityGaugeObject.SetActive(true);
            float t = Mathf.Clamp(slot.durability / slot.item.maxDurability, 0f, 1f);

            DurabilityGauge.fillAmount = t;
            DurabilityGauge.color = DurabilityColorGradient.Evaluate(1-t);
        }
        else
        {
            durabilityGaugeObject.SetActive(false);
        }
        
        eatButton.SetActive(slot.item.Type is ItemType.Grocery or ItemType.Consumables);
        foodDescription.SetActive(slot.item.Type is ItemType.Grocery or ItemType.Consumables);
    }

    private string ItemTypeConvert(ItemType type)
    {
        string result = "";
        switch (type)
        {
            case ItemType.Weapon:
                result = "무기";
                break;
            case ItemType.Gear:
                result = "기어";
                break;
            case ItemType.Consumables:
                result = "소모품";
                break;
            case ItemType.Grocery:
                result = "식료품";
                break;
            case ItemType.Material:
                result = "재료";
                break;
            case ItemType.Money:
                result = "재화";
                break;
        }

        return result;
    }
}
