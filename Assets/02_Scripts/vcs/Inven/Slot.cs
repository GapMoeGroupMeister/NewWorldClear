using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Slot : MonoBehaviour
{
    private Image ItemImage;
    private Image GuageFill;
    private TextMeshProUGUI ItemAmount;

    private void Awake()
    {
        ItemImage = transform.Find("ItemImage").GetComponent<Image>();
        GuageFill = transform.Find("ConditionGuage").transform.Find("GuageFill").GetComponent<Image>();
        ItemAmount = transform.Find("AmountBG").transform.Find("AmountText").GetComponent<TextMeshProUGUI>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlot(ItemSlot slotInfo)
    {
        Item item = slotInfo.item;
        ItemImage.sprite = item.itemSprite;
        GuageFill.fillAmount = Mathf.Clamp(item.durability / item.maxDurability, 0f, 1f);
    }
}
