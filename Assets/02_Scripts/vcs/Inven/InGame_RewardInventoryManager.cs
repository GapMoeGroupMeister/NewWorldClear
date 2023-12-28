using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_RewardInventoryManager : InventoryManager
{
    /**
     * <summary>
     * InGame배낭을 차지하던 InGameInventory List
     * </summary>
     */
    [SerializeField] private List<ItemSlot> inGameInventory;
    
    [SerializeField] private List<ItemSlot> totalInventory;

    [ContextMenu("AddInGameToStorage")]
    public void AddInGameInventoryToStorage()
    {
        foreach (ItemSlot slot in inGameInventory)
        {
            ItemManager.Instance.AddItem(slot, slot.amount);
        }
        Set_AllSlot();
        SaveInventory();
    }

    public void SetSlots()
    {
        Set_AllSlot();
    }
    
    

    public override void SceneStart()
    {
        // InventoryManager가 인게임 인벤토리를 관리 함
    }

    public override void SceneExit()
    {
        throw new System.NotImplementedException();
    }

    [ContextMenu("debugLoadStorageInventory")]
    public void LoadStorageInventory()
    {
        // 아이템 연산을 위해 +
        
        inGameInventory = ItemManager.Instance.inventory;
        totalInventory = DBManager.Get_Inventory();
        ItemManager.Instance.inventory = totalInventory;
    }
    
    [ContextMenu("debugSaveInventory")]
    public void SaveInventory()
    {
        // StorageInventory폴더로 저장
        ItemManager.Instance.SaveInventoryFile();
    }
    
}
