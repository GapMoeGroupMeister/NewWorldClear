using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request_InventoryManager : InventoryManager
{
    private void Start()
    {
        SceneStart();
    }

    public override void SceneStart()
    {
        ItemManager.Instance.LoadInventoryFile();
        Set_AllSlot();
    }

    public override void SceneExit()
    {
        ItemManager.Instance.SaveInventoryFile();
    }
    
    new protected void Set_AllSlot()
    {
        List<ItemSlot> inventory = ItemManager.Instance.inventory;
        if (inventory.Count <= 0)
        {
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
    new protected void Set_Slot(ItemSlot itemSlot)
    {
        GameObject slot = Instantiate(slotPrefab, grid);
        slot.GetComponent<Request_Slot>().SetSlot(itemSlot);

    }
}
