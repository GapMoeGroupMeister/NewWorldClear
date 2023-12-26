using UnityEngine;
using UnityEngine.EventSystems;


public class StorageSlot : Slot, IPointerClickHandler//,  IPointerExitHandler
{
    private bool isOn;
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
        ItemImage.sprite = SpriteLoader.Instance.FindSprite(currentItem.itemSpriteName);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOn)
        {
            isOn = true;
            Storage_UIManager.Instance.On_DescriptionUI();
            Storage_UIManager.Instance.Refresh_DescriptionUI(currentSlot);
            FoodManager.Instance.Slot = currentSlot;
            FoodManager.Instance.FoodDescription_Update();
        }
        else
        {
            isOn = false;
            Storage_UIManager.Instance.Off_DescriptionUI();
        }
    }
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     Storage_UIManager.Instance.Off_DescriptionUI();
    // }
}
