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


    public bool isEditMode = false;
    private ItemSlot MoveSlot;
    private ItemSlot targetSlot;
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
            Item thisItem = ItemSOManager.GetItem(_slot.itemId);

            if (thisItem == null)
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

    
    /**
     * <summary>
     *
     *
     * </summary>
     * <param name="amount">
     * 더할 아이템의 수
     * </param>
     * <returns>
     * 한 세트를 채우고 남은 아이템을 반환한다.
     * </returns>
     *
     */
    public int Add(ItemSlot slot, int amount)
    {
        int remain = 0;
        Item thisItem = ItemSOManager.GetItem(slot.itemId);

        //amount += _amount;

        // 최대치 적용 코드 : 안씀
        
        if (slot.amount+amount > thisItem.SlotSetAmount)
        {
            remain = (slot.amount + amount - thisItem.SlotSetAmount);
            slot.amount = thisItem.SlotSetAmount;
            
        }
        else
        {
            slot.amount += amount;
        }

        Debug.Log("Remain : "+remain);
        return remain;
    }

    /**
     * <summary>
     *
     * amount에서 입력 받은 수 만큼을 뺌
     * amount가 음수로 내려갈 시 false를 반환하며 연산은 취소 됨
     *
     *
     * </summary>
     * <param name="_amount">뺄 아이템의 수량</param>
     */
    public bool Sub(ItemSlot slot, int amount)
    {
        if (amount > slot.amount)
        {
            Debug.Log(ItemSOManager.GetItem(slot.itemId) +": 아이템의 수량은 음수가 될 수 없습니다");
            return false;
        }
            
        slot.amount -= amount;
        return true;
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
     * 아이템 추가 메서드 
     * </summary>
     * <param name="itemSlot"> 아이템 개체, amount를 개체 내부에서 반영함</param>
     */
    public void AddItem(ItemSlot itemSlot)
    {
        AddItem(NewItemSlot(itemSlot.itemId, itemSlot.amount));
        
    }
    
    /**
     * <summary>
     * 아이템 추가 메서드
     * <param name="itemSlot"> 아이템 개체, item속성만 반영함</param>
     * <param name="amount"> 아이템 개수, itemSlot에서 반영하지 않고 따로 입력받음 </param>
     * </summary>
     */
    public void AddItem(ItemSlot itemSlot, int amount)
    {
        if (FindItem(itemSlot.itemId) == null)
        {
            print("Null임");
            inventory.Add(NewItemSlot(itemSlot.itemId));
            amount--;
        }
        
        itemSlot = FindItem(itemSlot.itemId);
        do
        {
            amount = Add(itemSlot, amount);
            if (amount > 0)
            {
                inventory.Add(NewItemSlot(itemSlot.itemId));
                amount--;
            }
        
        } while (amount > 0);
        
    }

    /**
     * <summary>
     * 인벤토리에서 아이템을 찾아주는 메서드
     * </summary>
     * <param name="itemName">
     * 찾을 아이템의 이름
     * </param>
     * <returns>
     * 해당하는 아이템 슬롯을 반환함
     * </returns>
     */
    [CanBeNull]
    public ItemSlot FindItem(int itemId)
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
            

            if (slot.itemId == itemId)
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
        inventory = DBManager.Get_Inventory();
    }
    /**
     * <summary>
     * 인벤토리 파일을 json으로 저장함
     * </summary>
     */
    private void SaveInventoryFile()
    {
        DBManager.Save_Inventory(inventory);
    }

    private void SlotMove()
    {
        
    }

    public void SceneExit()
    {
        SaveInventoryFile();
    }

    #region  Debug

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
        AddItem(NewItemSlot(defaultItem.itemId, 1));
    }
    
    [ContextMenu("AddAnyItemSoup")]
    private void Debug_AddItemCan()
    {
        AddItem(NewItemSlot(canSoup.itemId, 1));
    }

    [ContextMenu("SaveInven")]
    private void Debug_SaveInven()
    {
        SaveInventoryFile();
    }

    [ContextMenu("LoadInven")]
    private void Debug_LoadInven()
    {
        LoadInventoryFile();
    }
    #endregion

    /**
     * <summary>
     * 생성자를 흉내내는 무언가
     * </summary>
     */
    public ItemSlot NewItemSlot(int _itemId)
    {
        return NewItemSlot(_itemId, 1);
    }
    
    /**
     * <summary>
     * 생성자를 흉내내는 무언가
     * </summary>
     */
    public ItemSlot NewItemSlot(int _itemId, int _amount)
    {
        Item item = ItemSOManager.GetItem(_itemId);
        if (item == null)
        {
            Debug.Log($"<color='red'>Error: NewItemSlot, {_itemId} is not exist</color>");
            return defaultItem;

        }
        return new ItemSlot()
        {
            itemId = _itemId,
            amount = _amount,
            durability = item.maxDurability
        };

    }
    
}
