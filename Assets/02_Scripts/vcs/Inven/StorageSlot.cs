using UnityEngine;
using UnityEngine.EventSystems;


public class StorageSlot : Slot, IPointerEnterHandler,  IPointerExitHandler
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
        if (ItemImage == null) return;
        
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Storage_UIManager.Instance.On_DescriptionUI();
        Storage_UIManager.Instance.Refresh_DescriptionUI(currentSlot);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Storage_UIManager.Instance.Off_DescriptionUI();
    }
    
}
