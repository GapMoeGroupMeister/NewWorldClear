using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "SO/ShopItemSO")]

public class ShopSO : ScriptableObject
{
    public string itemName;
    [TextArea(3, 5)]
    public string description;
    public int price;
    public Sprite itemIcon;
    public int grade;
}

