using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryManager : MonoBehaviour
{   
    [Tooltip("인벤슬롯 프리팹")]
    [SerializeField] protected GameObject slotPrefab;

    [SerializeField] protected Transform grid;

    /**
     * <summary>
     * 씬이 시작될때 실행해주는 함수
     * </summary>
     */
    public abstract void SceneStart();

    /**
     * <summary>
     * 씬을 나갈때 실행해주는 함수
     * </summary>
     */
    public abstract void SceneExit();
    
    /**
     * <summary>
     * inventory 전체를 지웠다가 다시 만듬
     * </summary>
     */
    protected void Refresh()
    {
        Delete_Inven();
        Set_AllSlot();
        
    }
    protected void Set_AllSlot()
    {
        List<ItemSlot> inventory = ItemManager.Instance.inventory;
        if (inventory == null || inventory.Count <= 0)
        {
            ItemManager.Instance.inventory = new List<ItemSlot>();
            return;

        }
        
        foreach (ItemSlot _slot in inventory)
        {
            Item thisItem = _slot.item;

            if (thisItem == null)
            {
                inventory.Remove(_slot);
                return;
            }
            
            Set_Slot(_slot);
        }
    }
    protected void Set_Slot(ItemSlot itemSlot)
    {
        GameObject slot = Instantiate(slotPrefab, grid);
        
        slot.GetComponent<Slot>().SetSlot(itemSlot);
        
        
        
    }
    
    
    /**
     * <summary>
     * inventory 전체를 지움
     * </summary>
     */
    protected void Delete_Inven()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject) ;
        }
    }
}
