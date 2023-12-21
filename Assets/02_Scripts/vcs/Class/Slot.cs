using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    [SerializeField]
    protected Image ItemImage;
    [SerializeField]
    protected Image GaugeFill; 
    [SerializeField]
    protected TextMeshProUGUI ItemAmount;

    [SerializeField]
    protected ItemSlot currentSlot;

    protected Item currentItem;
    
    private void Awake()
    {
        if (ItemImage == null)
        {
           ItemImage = transform.GetChild(0).GetComponent<Image>();

        }

        if (GaugeFill == null)
        {
            GaugeFill = transform.Find("ConditionGauge").transform.Find("GaugeFill").GetComponent<Image>();
        }

        if (ItemAmount == null)
        {
            ItemAmount = transform.Find("AmountBG").transform.Find("AmountText").GetComponent<TextMeshProUGUI>();

        }
        
    }

    /**
     * <param name="slotInfo">
     * 이 슬롯에 들어갈 ItemSlot정보
     * </param>
     * <summary>
     * 슬롯을 설정해주는 메서드
     * </summary>
     */
    public abstract void SetSlot(ItemSlot slotInfo);
    
    /**
     * <summary>
     * 슬롯 아이콘을 생성해주는 메서드
     * </summary>
     */
    protected abstract void SetItemIcon();
    
    /**
     * <summary>
     * 슬롯 아이템 내구도 게이지를 설정해주는 메서드
     * </summary>
     */
    protected abstract void SetGauge();

}