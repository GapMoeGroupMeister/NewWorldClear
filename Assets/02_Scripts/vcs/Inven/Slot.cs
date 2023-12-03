using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler
{
    public ItemSlot thisSlot;
    
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
        currentItem = ItemSOManager.Instance.GetItem(currentSlot.itemId);

        
        print(currentItem.itemName);
        SetItemIcon();
        SetGuage();
        ItemAmount.text = currentSlot.amount.ToString();
    }

    private void SetItemIcon()
    {
        if (ItemImage == null) return;
        ItemImage.sprite = currentItem.itemSprite;
        ItemImage.SetNativeSize();
        
        
    }

    private void SetGuage()
    {
        if (currentItem.isLimited)
        {
            GuageFill.fillAmount = Mathf.Clamp(currentSlot.durability / currentItem.maxDurability, 0f, 1f);

        }
        else
        {
            GuageFill.fillAmount = 1;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Storage_UIManager.Instance.On_DescriptionUI();
        Storage_UIManager.Instance.Refresh_DescriptionUI(currentItem);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Storage_UIManager.Instance.Off_DescriptionUI();
    }
    
}
