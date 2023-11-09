using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlot 
{
    // SO의 값은 변하지 않을 값들을 넣어놓은 것임
    // 그 외에 변경될수 있는 요소들은 전부 ItemSlot으로 뺌
    
    /**
     * <summary>
     * 아이템 SO 정보
     * </summary>
     */
    public Item item;
    /**
     * <summary>
     * 아이템의 수량
     * </summary>
     */
    public int amount;
    /**
     * <summary>
     * 아이템 내구도
     * </summary>
     */
    public float durability;

    public ItemSlot()
    {
        
    }
    public ItemSlot(Item item)
    {
        this.item = item;
        amount = 1;
        durability = item.maxDurability;
    }
    
    public ItemSlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
        durability = item.maxDurability;
    }
    /**
     * <summary>
     * 반환값은남은 
     *
     * amount에서 입력 받은 수 만큼을 더함
     * amount가 슬롯 보관 
     * </summary>
     */
    public int Add(int _amount)
    {
        Debug.Log("아이템을 추가함");
        int remain = 0;
        amount += _amount;

        if (amount > item.SlotSetAmount)
        {
            amount = item.SlotSetAmount;
            remain = amount - item.SlotSetAmount;
            Debug.Log("아이템이 "+remain+"개 남음");
            return remain;
        }

        return 0;
    }

    /**
     * <summary>
     * Default 반환은 true.
     *  
     * amount에서 입력 받은 수 만큼을 뺌
     * amount가 음수로 내려갈 시 false를 반환하며 연산은 취소 됨
     * </summary>
     */
    public bool Sub(int _amount)
    {
        if (_amount > this.amount)
        {
            Debug.Log(item.itemName+": 아이템의 수량은 음수가 될 수 없습니다");
            return false;
        }
            
        this.amount -= _amount;
        return true;
    }
}
