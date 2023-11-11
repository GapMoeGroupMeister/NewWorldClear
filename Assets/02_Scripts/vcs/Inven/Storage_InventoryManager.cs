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

    public List<ItemSlot> inventory;

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
        AddItem(new ItemSlot(defaultItem.item), 1);
    }
    
    [ContextMenu("AddAnyItemSoup")]
    private void Debug_AddItemCan()
    {
        AddItem(new ItemSlot(canSoup.item), 1);
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
    /**
     * <summary>
     * 아이템 추가 메서드 <param name="itemSlot"> 아이템 개체, amount를 개체 내부에서 반영함</param>
     * </summary>
     */
    public void AddItem(ItemSlot itemSlot)
    {
        int _amount = itemSlot.amount;
        if (Finditem(itemSlot.item.itemName) == null)
        {
            print("Null임");
            inventory.Add(new ItemSlot(itemSlot.item));
            _amount--;
        }
        
        itemSlot = Finditem(itemSlot.item.itemName);
        do
        {
            _amount = itemSlot.Add(_amount);
            if (_amount > 0)
            {
                inventory.Add(new ItemSlot(itemSlot.item));
                _amount--;
            }
        
        } while (_amount > 0);
        
    }
    
    /**
     * <summary>
     * 아이템 추가 메서드
     * <param name="itemSlot"> 아이템 개체, item속성만 반영함</param>
     * <param name="_amount"> 아이템 개수, itemSlot에서 반영하지 않고 따로 입력받음 </param>
     * </summary>
     */
    public void AddItem(ItemSlot itemSlot, int _amount)
    {
        if (Finditem(itemSlot.item.itemName) == null)
        {
            print("Null임");
            inventory.Add(new ItemSlot(itemSlot.item));
            _amount--;
        }
        
        itemSlot = Finditem(itemSlot.item.itemName);
        do
        {
            _amount = itemSlot.Add(_amount);
            if (_amount > 0)
            {
                inventory.Add(new ItemSlot(itemSlot.item));
                _amount--;
            }
        
        } while (_amount > 0);
        
    }

    /**
     * <summary>
     * 인벤토리에서 아이템을 찾아주는 메서드
     * </summary>
     * <param name="itemName">
     * 찾을 아이템의 이름
     * </param>
     * <returns type = "ItemSlot">
     * 해당하는 아이템 슬롯을 반환함
     * </returns>
     */
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
