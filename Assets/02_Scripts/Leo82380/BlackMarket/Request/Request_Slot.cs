using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Request_Slot : Slot
{
    private void Awake()
    {
        ItemImage = transform.GetChild(0).GetComponent<Image>();
        GaugeFill = transform.Find("ConditionGuage").transform.Find("GuageFill").GetComponent<Image>();
        ItemAmount = transform.Find("AmountBG").transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
    }


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
        Sprite sprite = SpriteLoader.FindSprite(currentSlot.item.itemSpriteName);
        ItemImage.sprite = sprite;
        if(sprite == null)
            print("null임");
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
