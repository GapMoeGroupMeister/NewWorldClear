using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public List<ItemSlot> inventory;

    
    /**
     * <param name="slot">
     * 더할 아이템 슬롯
     * </param>
     * <param name="amount">
     * 더할 아이템의 수
     * </param>
     * <returns>
     * 한 세트를 채우고 남은 아이템을 반환한다.
     * </returns>
     *
     */
    private int Add(ItemSlot slot, int amount)
    {
        int remain = 0;
        Item thisItem = slot.item;

        if (slot.amount+amount > thisItem.SlotSetAmount)
        {
            remain = (slot.amount + amount - thisItem.SlotSetAmount);
            slot.amount = thisItem.SlotSetAmount;
            
        }
        else
        {
            slot.amount += amount;
        }

        return remain;
    }

    /**
     * <summary>
     *
     * amount에서 입력 받은 수 만큼을 뺌
     * amount가 음수로 내려갈 시 false를 반환하며 연산은 취소 됨
     *
     * </summary>
     * <param name="amount">뺄 아이템의 수량</param>
     */
    private bool Sub(ItemSlot slot, int amount)
    {
        if (amount > slot.amount)
        {
            Debug.Log(slot.item +": 아이템의 수량은 음수가 될 수 없습니다");
            return false;
        }
            
        slot.amount -= amount;
        return true;
    }
    
    
    
    /**
     * <summary>
     * 아이템 추가 메서드 
     * </summary>
     * <param name="itemSlot"> 아이템 개체, amount를 개체 내부에서 반영함</param>
     */
    public void AddItem(ItemSlot itemSlot)
    {
        AddItem(itemSlot, itemSlot.amount);
        
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
        // 내구도 제한이 있는 아이템을 추가 할 시 그냥 싹다 하나하나 더함
        if (itemSlot.item.isLimited)
        {
            for (int i = 0; i < amount; i++)
            {
                inventory.Add(new ItemSlot(itemSlot.item, 1, itemSlot.durability));
            }

            return;
        }

        print("CountItem : "+CountItem(itemSlot.item));
        if (CountItem(itemSlot.item) <= 0)
        {
            print("Null임");
            inventory.Add(new ItemSlot(itemSlot.item));
            amount--;
        }

        if (amount <= 0)
        {
            return;
        }

        
        itemSlot = FindItem(itemSlot.item);
        do
        {
            amount = Add(itemSlot, amount);
            if (amount > 0)
            {
                inventory.Add(new ItemSlot(itemSlot.item));
                amount--;
            }
        
        } while (amount > 0);

        print("이까지 오긴 오냐?");
    }

    /**
     * <param name="item">
     * 인벤에서 뺄 아이템
     * </param>
     * <param name="amount">
     * 뺄 아이템 개수
     * </param>
     * <summary>
     * 아이템 빼는 메서드
     * </summary>
     * <returns>
     * 성공적으로 아이템을 뺐는지 Bool 값으로 반환
     * </returns>
     */
    public bool SubItem(Item item, int amount)
    {

        if (item.isLimited)
        {
            if (CountItem(item) < amount)
            {
                Debug.Log("아이템이 작어서 뺄수 없습니다");
                return false;
            }
            int count = amount / item.SlotSetAmount;
            amount %= item.SlotSetAmount;
            
            for (int i = 0; i < count; i++)
            {
                ItemSlot? slot = FindItem(item);
                Sub(slot, item.SlotSetAmount);
            }
            ItemSlot? slot_ = FindItem(item);
            if (slot_ == null)
            {
                Debug.Log("아이템이 인벤토리에 존재하지 않아 뺄수 없습니다");
                return false;
            }
            return Sub(slot_, amount);
        }
        else
        {
            ItemSlot? slot = FindItem(item);
            if (slot == null)
            {
                Debug.Log("아이템이 인벤토리에 존재하지 않아 뺄수 없습니다");
                return false;
            }

            return Sub(slot, amount);
        }


    }

    /**
     * <summary>
     * 인벤토리에서 아이템을 찾아주는 메서드
     * </summary>
     * <param name="item">
     * 찾을 아이템
     * </param>
     * <returns>
     * 해당하는 아이템 슬롯을 반환함
     * </returns>
     */
    [CanBeNull]
    public ItemSlot FindItem(Item item)
    {
       
        for (int i = 0; i < inventory.Count; i++)
        {
            ItemSlot slot = inventory[i];

            if (slot.amount <= 0)
            {
                inventory.Remove(slot);
                i--;
                continue;
            }
            

            if (slot.item == item)
            {
                return slot;
                break;
            }
            
        }
        
        return null;
    }
    
    /**
     * <summary>
     * 인벤토리에서 아이템을 찾아주는 메서드
     * </summary>
     * <param name="itemSlot">
     * 찾을 아이템 슬롯
     * </param>
     * <returns>
     * 해당하는 아이템 슬롯을 반환함
     * </returns>
     */
    [CanBeNull]
    public ItemSlot FindItem(ItemSlot itemSlot)
    {
        ItemSlot targetItem = new ItemSlot(){amount = -1};
        for (int i = 0; i < inventory.Count; i++)
        {
            ItemSlot slot = inventory[i];

            if (slot.amount <= 0)
            {
                inventory.Remove(slot);
                i--;
                continue;
            }
            

            if (slot.item == itemSlot.item && slot.durability == itemSlot.durability)
            {
                return slot;
                break;
            }
            
        }
        
        return null;
    }

    /**
     * <param name="itemSlot">
     * 찾을 아이템 슬롯 (durability를 반영함)
     * </param>
     * <summary>
     * 아이템의 개수를 세어 반환합니다
     * </summary>
     * <returns>
     * 찾은 조건에 해당하는 아이템의 개수
     * </returns>
     */
    public int CountItem(ItemSlot itemSlot)
    {
        int amount = 0;
        foreach (ItemSlot slot in inventory)
        {
            if (slot.item == itemSlot.item && slot.durability == itemSlot.durability)
            {
                amount += slot.amount;
            }
        }

        return amount;
    }
    
    
    /**
     * <param name="item">
     * 찾을 아이템
     * </param>
     * <summary>
     * 아이템의 개수를 세어 반환합니다
     * </summary>
     * <returns>
     * 찾은 조건에 해당하는 아이템의 개수
     * </returns>
     */
    public int CountItem(Item item)
    {
        int amount = 0;
        foreach (ItemSlot slot in inventory)
        {
            if (item == slot.item)
            {
                amount += slot.amount;
            }
        }

        return amount;
    }

    
    
    /**
     * <summary>
     * json에서 창고 인벤토리 파일을 불러옴
     * </summary>
     */
    public void LoadInventoryFile()
    {
        inventory = DBManager.Get_Inventory();
    }
    /**
     * <summary>
     * 창고 인벤토리 파일을 json으로 저장함
     * </summary>
     */
    public void SaveInventoryFile()
    {
        DBManager.Save_Inventory(inventory);
    }
    
    
    /**
     * <summary>
     * json에서 인게임 인벤토리 파일을 불러옴
     * </summary>
     */
    public void LoadInGameInventoryFile()
    {
        inventory = DBManager.Get_InGameInventory();
    }
    /**
     * <summary>
     * 인게임 인벤토리 파일을 json으로 저장함
     * </summary>
     */
    public void SaveInGameInventoryFile()
    {
        DBManager.Save_InGameInventory(inventory);
    }
}
