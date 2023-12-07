using UnityEngine;

[CreateAssetMenu(fileName = "RequestSO", menuName = "SO/RequestSO")]
public class RequestSO : ScriptableObject
{
    public string request;
    public Item item;
    public int amount;
    public ItemSlot reward;
}
