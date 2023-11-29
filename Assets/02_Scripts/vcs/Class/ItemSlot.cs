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
    public int Add(int amount)
    {
        int remain = 0;

        //amount += _amount;

        // 최대치 적용 코드 : 안씀
        
        if (this.amount+amount > item.SlotSetAmount)
        {
            remain = (this.amount + amount - item.SlotSetAmount);
            this.amount = item.SlotSetAmount;
            
        }
        else
        {
            this.amount += amount;
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
    public bool Sub(int amount)
    {
        if (amount > this.amount)
        {
            Debug.Log(item.itemName+": 아이템의 수량은 음수가 될 수 없습니다");
            return false;
        }
            
        this.amount -= amount;
        return true;
    }
}
