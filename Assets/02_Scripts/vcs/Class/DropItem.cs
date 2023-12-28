
[System.Serializable]
public class DropItem
{
    // 딱히 안쓸 예정
    public string itemName;

    /**
     * <summary>
     * 얻을 아이템
     * </summary>
     */
    public Item item;

    /**
     * <summary>
     * 얻을 아이템 개수
     * </summary>
     */
    public int amount;

    /**
     * <summary>
     * 드롭 확률
     * </summary>
     */
    public float percentage = 100f;

}