using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Storage_InventoryManager : MonoBehaviour
{
    [Tooltip("인벤슬롯 프리팹")]
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private Transform grid;

    [SerializeField]
    private List<ItemSlot> inventory;

    [SerializeField] private ItemSlot defaultItem;
    [SerializeField] private ItemSlot canSoup;

    private void Awake()
    {
    }

    void Start()
    {
        LoadInventoryFile();
        Set_AllSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /**
     * <summary>
     * inventory 전체를 지웠다가 다시 만듬
     * </summary>
     */
    private void Refresh()
    {
        Delete_Inven();
        Set_AllSlot();
        
    }
    private void Set_AllSlot()
    {
        if (inventory.Count <= 0)
        {
            return;

        }
        
        
        foreach (ItemSlot _slot in inventory)
        {
            if (_slot.item == null)
            {
                inventory.Remove(_slot);
                return;
            }
            
            Set_Slot(_slot);
        }
    }
    private void Set_Slot(ItemSlot itemSlot)
    {
        GameObject slot = Instantiate(slotPrefab, grid);
        slot.GetComponent<Slot>().SetSlot(itemSlot);

    }

    [ContextMenu("SlotsRefresh")]
    private void Debug_Refresh()
    {
        Refresh();

    }

    [ContextMenu("SlotsDelete")]
    private void Debug_Delete()
    {
        inventory = new List<ItemSlot>();
        Delete_Inven();
    }
    
    [ContextMenu("AddAnyItem")]
    private void Debug_AddItem()
    {
        AddItem(defaultItem, 1);
    }
    
    [ContextMenu("AddAnyItemSoup")]
    private void Debug_AddItemCan()
    {
        AddItem(canSoup, 1);
    }
    
    private void Refresh_Setting()
    {
            
    }
    /**
     * <summary>
     * inventory 전체를 지움
     * </summary>
     */
    private void Delete_Inven()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject) ;
        }
    }

    public void AddItem(ItemSlot itemSlot, int _amount)
    {
        ItemSlot? slot = Finditem(itemSlot.item.itemName);
        if (slot == null)
        {
            print("현재 인벤에 존재하지 않는 아이템");
            inventory.Add(itemSlot);
            _amount--;
        }
        
        while (_amount > 0)
        {
            _amount = itemSlot.Add(_amount);
        }
        
    }

    [CanBeNull]
    public ItemSlot Finditem(string itemName)
    {
        ItemSlot targetItem = new ItemSlot(){amount = -10};

        for (int i = 0; i < inventory.Count; i++)
        {
            
            ItemSlot slot = inventory[i];
            if (slot.amount <= 0)
            {
                inventory.Remove(slot);
                i--;
                continue;
            }
            

            if (slot.item.itemName == itemName)
            {
                targetItem = slot;
                break;
            }
            
        }
        
        if (targetItem.amount <= 0)
        {
            return null;
        }
        //int index = inventory.IndexOf(targetItem);
        return targetItem;
    }

    
    /**
     * <summary>
     * json에서 인벤토리 파일을 불러옴
     * </summary>
     */
    private void LoadInventoryFile()
    {
        inventory = DBManager.Instance.Get_Inventory();
    }
    /**
     * <summary>
     * 인벤토리 파일을 json으로 저장함
     * </summary>
     */
    private void SaveInventoryFile()
    {
        DBManager.Instance.Save_Inventory(inventory);
    }

    private void SlotChange()
    {
        
    }

    public void SceneExit()
    {
        SaveInventoryFile();
    }
    
}
