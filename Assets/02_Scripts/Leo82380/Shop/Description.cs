using UnityEngine;
using DG.Tweening;

public class Description : MonoBehaviour
{
    /** 
     * <summary>
     * ���� ����â�� ���� ������ �Ѱ��ֱ� ���� ������Ƽ
     * </summary>
     */
    public Weapon2 Weapon2 { get; set; }
    /** <summary>
     * ����â�� �����ִ��� Ȯ���ϱ� ���� ������Ƽ
     * </summary>
     */
    public bool IsOn { get; private set; } = false;

    /**
     * <summary>
     * ���� ��ư�� ������ ȣ��Ǵ� �޼���
     * </summary>
     */
    public void Buy()
    {
        if (Weapon2.WeaponDescription[Weapon2.RandomIndex].isSoldOut) return;

        Weapon2.SoldOut.SetActive(true);
        Weapon2.WeaponDescription[Weapon2.RandomIndex].isSoldOut = true;

        Cancel();
    }

    /**
     * <summary>
     * ����â�� �ݴ� �޼���
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
