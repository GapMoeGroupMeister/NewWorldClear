using UnityEngine;

[CreateAssetMenu(fileName = "Request", menuName = "SO/Request")]
public class RequestSO : ScriptableObject
{
    public string request;
    public Item item;
    public int amount;
    public ItemSlot reward;
}
