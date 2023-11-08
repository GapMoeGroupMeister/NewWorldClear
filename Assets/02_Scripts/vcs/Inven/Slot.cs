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
    private Image ItemImage;
    private Image GuageFill;
    private TextMeshProUGUI ItemAmount;

    [SerializeField]
    private ItemSlot currentSlot;

    private void Awake()
    {
        
        
        ItemImage = transform.Find("ItemImage").GetComponent<Image>();
        GuageFill = transform.Find("ConditionGuage").transform.Find("GuageFill").GetComponent<Image>();
        ItemAmount = transform.Find("AmountBG").transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
    }


    public void SetSlot(ItemSlot slotInfo)
    {
        currentSlot = slotInfo;
        Item item = currentSlot.item;
        SetItemIcon();
        
        ItemAmount.text = currentSlot.amount.ToString();
    }

    private void SetItemIcon()
    {
        
        ItemImage.sprite = currentSlot.item.itemSprite;
        SetGuage();
        
    }

    private void SetGuage()
    {
        GuageFill.fillAmount = Mathf.Clamp(currentSlot.durability / currentSlot.item.maxDurability, 0f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Storage_UIManager.Instance.On_DescriptionUI();
        Storage_UIManager.Instance.Refresh_DescriptionUI(thisSlot.item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Storage_UIManager.Instance.Off_DescriptionUI();
    }
    
}
