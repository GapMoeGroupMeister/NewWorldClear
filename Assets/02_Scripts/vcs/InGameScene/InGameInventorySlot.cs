using UnityEngine;

public class InGameInventorySlot : Slot
{
    public override void SetSlot(ItemSlot slotInfo)
    {
        currentSlot = slotInfo;
        currentItem = slotInfo.item;

        
        SetItemIcon();
        SetGauge();
        ItemAmount.text = currentSlot.amount.ToString();
    }

    protected override void SetItemIcon()
    {
        ItemImage.sprite = SpriteLoader.FindSprite(currentItem.itemSpriteName);
        ItemImage.SetNativeSize();
        
        
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
}
