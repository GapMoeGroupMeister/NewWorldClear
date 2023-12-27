using EasyJson;
using UnityEngine;

public class StatusManager : MonoSingleton<StatusManager>
{
    [SerializeField] private PlayerStatus playerStatus;

    public PlayerStatus PlayerStatus
    {
        get => playerStatus;
        set => playerStatus = value;
    }

    public void SavePlayerStatus()
    {
        EasyToJson.ToJson(playerStatus, "playerStatus", true);
    }
    
    public void LoadPlayerStatus()
    {
        playerStatus = EasyToJson.FromJson<PlayerStatus>("playerStatus");
    }
}
