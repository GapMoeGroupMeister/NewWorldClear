using System.Collections;
using System.Collections.Generic;
using EasyJson;
using UnityEngine;

public class StatusManager : MonoSingleton<StatusManager>
{
    private PlayerStatus playerStatus;
    
    public PlayerStatus PlayerStatus => playerStatus;

    public void SavePlayerStatus()
    {
        EasyToJson.ToJson(playerStatus, "playerStatus");
    }
    
    public void LoadPlayerStatus()
    {
        playerStatus = EasyToJson.FromJson<PlayerStatus>("playerStatus");
    }
}
