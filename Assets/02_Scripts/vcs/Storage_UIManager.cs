using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Tkfkadlsi;
using UnityEditor;


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
        Text_ItemType.text = slot.item.Type.ToString();
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
    }

    
}
