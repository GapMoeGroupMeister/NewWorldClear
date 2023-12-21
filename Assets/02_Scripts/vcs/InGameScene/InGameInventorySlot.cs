using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameInventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image ItemImage;
    [SerializeField]
    private Image GuageFill;
    [SerializeField]
    private TextMeshProUGUI ItemAmount;

    [SerializeField]
    private ItemSlot currentSlot;

    private Item currentItem;

    private void Awake()
    {
        
        
        ItemImage = transform.GetChild(0).GetComponent<Image>();
        GuageFill = transform.Find("ConditionGuage").transform.Find("GuageFill").GetComponent<Image>();
        ItemAmount = transform.Find("AmountBG").transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
    }
    
    public void SetSlot(ItemSlot slotInfo)
    {
        currentSlot = slotInfo;
        currentItem = slotInfo.item;

        
        SetItemIcon();
        SetGauge();
        ItemAmount.text = currentSlot.amount.ToString();
    }

    private void SetItemIcon()
    {
        if (ItemImage == null) return;
        //ItemImage.sprite = ;
        ItemImage.SetNativeSize();
        
        
    }

    private void SetGauge()
    {
        if (currentItem.isLimited)
        {
            GuageFill.fillAmount = Mathf.Clamp((float)currentSlot.durability / currentItem.maxDurability, 0f, 1f);

        }
        else
        {
            GuageFill.fillAmount = 1;
        }
    }
}
