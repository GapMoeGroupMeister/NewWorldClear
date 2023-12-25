using UnityEngine;

[CreateAssetMenu(fileName = "RequestSO", menuName = "SO/RequestSO")]
public class RequestSO : ScriptableObject
{
    public string request;
    public int requestID;
    public ItemSlot[] requests;
    public ItemSlot reward;
}
