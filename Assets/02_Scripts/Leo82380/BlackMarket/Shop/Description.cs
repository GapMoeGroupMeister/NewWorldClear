using UnityEngine;
using DG.Tweening;

public class Description : MonoBehaviour
{
    /** 
     * <summary>
     * 무기 설명창에 무기 정보를 넘겨주기 위한 프로퍼티
     * </summary>
     */
    public Weapon2 Weapon2 { get; set; }
    /** <summary>
     * 설명창이 켜져있는지 확인하기 위한 프로퍼티
     * </summary>
     */
    public bool IsOn { get; private set; } = false;
    /** <summary>
     * money 가져옴
     * </summary>
     */
    [SerializeField] private Item money;

    /**
     * <summary>
     * 구매 버튼을 누르면 호출되는 메서드
     * </summary>
     */
    public void Buy()
    {
        if (Weapon2.GetWeaponDescription().isSoldOut) return;
        if (ItemManager.Instance.FindItem(money) == null) return;
        if (ItemManager.Instance.FindItem(money).amount < Weapon2.GetWeaponDescription().shopSO.price) return;

        Weapon2.SoldOut.SetActive(true);
        Weapon2.GetWeaponDescription().isSoldOut = true;
        
        ItemManager.Instance.FindItem(money).amount -= Weapon2.GetWeaponDescription().shopSO.price;
        ItemManager.Instance.AddItem(Weapon2.GetWeaponDescription().shopSO.item, 1);

        Weapon2.MoneyTextSetup();
        Cancel();
    }

    /**
     * <summary>
     * 설명창을 닫는 메서드
     * </summary>
     */
    public void Cancel()
    {
        transform.DOScale(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
        Weapon2 = null;
        IsOn = false;
    }
    
    private void OnEnable()
    {
        IsOn = true;
    }
    
    private void OnDisable()
    {
        IsOn = false;
    }
}
