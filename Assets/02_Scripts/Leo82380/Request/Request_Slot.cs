using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Request_Slot : MonoBehaviour
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
        SetGuage();
        ItemAmount.text = currentSlot.amount.ToString();
    }

    private void SetItemIcon()
    {
        Sprite sprite = SpriteLoader.Instance.FindSprite(currentSlot.item.itemSpriteName);
        ItemImage.sprite = sprite;
        if(sprite == null)
            print("null임");
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
}
