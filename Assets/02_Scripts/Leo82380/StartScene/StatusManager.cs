using EasyJson;
using JetBrains.Annotations;
using UnityEngine;

public class StatusManager : MonoSingleton<StatusManager>
{
    [SerializeField] [CanBeNull] private PlayerStatus playerStatus;

    public PlayerStatus PlayerStatus;
    public void SavePlayerStatus()
    {
        EasyToJson.ToJson(playerStatus, "playerStatus", true);
    }
    
    public void LoadPlayerStatus()
    {
        playerStatus = EasyToJson.FromJson<PlayerStatus>("playerStatus");
        if (playerStatus == null)
        {
            playerStatus = new PlayerStatus();
            SavePlayerStatus();
        }
    }
}
