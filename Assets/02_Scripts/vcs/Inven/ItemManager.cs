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
        AddItem(new ItemSlot(itemSlot.item, itemSlot.amount), itemSlot.amount);
        
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
        if (FindItem(itemSlot.item) == null)
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

        ItemSlot? slot = FindItem(item);
        if (slot == null)
        {
            Debug.Log("아이템이 인벤토리에 존재하지 않아 뺄수 없습니다");
            return false;
        }

        return Sub(slot, amount);
        
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
    public ItemSlot FindItem(Item item)
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
            

            if (slot.item == item)
            {
                targetItem = slot;
                break;
            }
            
        }
        
        return targetItem;
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
}
