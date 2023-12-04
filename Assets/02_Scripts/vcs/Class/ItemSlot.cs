
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
    
}
