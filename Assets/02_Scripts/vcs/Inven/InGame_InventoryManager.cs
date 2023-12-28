using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame_InventoryManager : InventoryManager
{
    
    [SerializeField] private ItemSlot defaultItem;
    [SerializeField] private ItemSlot canSoup;

    [SerializeField] private UIInfo InventoryUI;

    [SerializeField] private int MaxSlots = 10;

    private void Update()
    {
        RefreshForFrame();
    }

    public void OnInventory()
    {
        InventoryUI.MovePos();
        
    }
    

    private void RefreshForFrame()
    {
        if (InventoryUI.onOff)
        {
            Refresh();
        }
    }
    


    public override void SceneStart()
    {
        ItemManager.Instance.LoadInGameInventoryFile();
        Set_AllSlot();
    }
    
    

    public override void SceneExit()
    {
        // 인게임은 배낭이라는 독자적인 인벤토리를 가지고 있어야함
        ItemManager.Instance.SaveInGameInventoryFile();
    }
    
    [ContextMenu("Debug_Refresh")]
    private void DebugRefresh()
    {
        Refresh();
    }

    [ContextMenu("Debug_AddItem")]
    private void DebugAddDefaultItem()
    {
        ItemManager.Instance.AddItem(defaultItem);
    }
    
    [ContextMenu("Debug_AddCanSoupItem")]
    private void DebugAddCanSoupItem()
    {
        ItemManager.Instance.AddItem(new ItemSlot(canSoup.item, canSoup.amount, canSoup.durability));
    }

}
