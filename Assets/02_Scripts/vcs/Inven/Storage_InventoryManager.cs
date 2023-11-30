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
            if (_slot.item == null)
            {
                inventory.Remove(_slot);
                return;
            }
            // 아이템에 저장되지 못한 Sprite정보를 보충
            _slot.item = ItemSOManager.GetItem(_slot.item.id);
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

        //amount += _amount;

        // 최대치 적용 코드 : 안씀
        
        if (slot.amount+amount > slot.item.SlotSetAmount)
        {
            remain = (slot.amount + amount - slot.item.SlotSetAmount);
            slot.amount = slot.item.SlotSetAmount;
            
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
            Debug.Log(slot.item.itemName+": 아이템의 수량은 음수가 될 수 없습니다");
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
        int _amount = itemSlot.amount;
        if (FindItem(itemSlot.item.itemName) == null)
        {
            print("Null임");
            inventory.Add(NewItemSlot(itemSlot.item));
            _amount--;
        }
        
        itemSlot = FindItem(itemSlot.item.itemName);
        do
        {
            _amount = Add(itemSlot,_amount);
            if (_amount > 0)
            {
                inventory.Add(NewItemSlot(itemSlot.item));
                _amount--;
            }
        
        } while (_amount > 0);
        
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
        if (FindItem(itemSlot.item.itemName) == null)
        {
            print("Null임");
            inventory.Add(NewItemSlot(itemSlot.item));
            amount--;
        }
        
        itemSlot = FindItem(itemSlot.item.itemName);
        do
        {
            amount = Add(itemSlot, amount);
            if (amount > 0)
            {
                inventory.Add(NewItemSlot(itemSlot.item));
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
    public ItemSlot FindItem(string itemName)
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
        AddItem(NewItemSlot(defaultItem.item), 1);
    }
    
    [ContextMenu("AddAnyItemSoup")]
    private void Debug_AddItemCan()
    {
        AddItem(NewItemSlot(canSoup.item), 1);
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
    public ItemSlot NewItemSlot(Item _item)
    {
        return new ItemSlot()
        {
            item = _item,
            amount = 1,
            durability = _item.maxDurability
        };
    }
    
    /**
     * <summary>
     * 생성자를 흉내내는 무언가
     * </summary>
     */
    public ItemSlot NewItemSlot(Item _item, int _amount)
    {
        return new ItemSlot()
        {
            item = _item,
            amount = _amount,
            durability = _item.maxDurability
        };

    }
    
}
