using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSceneStoreageSlot : Slot, IPointerClickHandler
{
    //private bool isOn = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StartSceneStorageInventoryManager.instance.TakeItem(currentItem, currentSlot.durability, currentSlot.amount);
            StartSceneStorageInventoryManager.instance.curSelectItemSlot = currentSlot;
        }
    }

    public override void SetSlot(ItemSlot slotInfo)
    {
        currentSlot = slotInfo;
        currentItem = slotInfo.item;


        SetItemIcon();
        SetGauge();
        ItemAmount.text = currentSlot.amount.ToString();
    }

    protected override void SetGauge()
    {
        if (currentItem.isLimited)
        {
            GaugeFill.fillAmount = Mathf.Clamp((float)currentSlot.durability / currentItem.maxDurability, 0f, 1f);

        }
        else
        {
            GaugeFill.fillAmount = 1;
        }
    }

    protected override void SetItemIcon()
    {
        ItemImage.sprite = SpriteLoader.Instance.FindSprite(currentItem.itemSpriteName);
        ItemImage.SetNativeSize();
    }
}
