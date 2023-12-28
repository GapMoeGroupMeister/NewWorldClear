using System.Collections.Generic;
using UnityEngine;
using EasyJson;

public class DBManager : MonoBehaviour
{
    private string _inventoryPath;
    
    /**
     * <summary>
     * json으로 저장된 인벤토리 정보를 불러와 반환해줌
     * </summary>
     */
    public static List<ItemSlot> Get_Inventory()
    {
        List<ItemSlot> inven = EasyToJson.ListFromJson<ItemSlot>("inventory");
        if (inven == null)
        {
            inven = new List<ItemSlot>();
            Save_Inventory(inven);
        }
        return inven;
    }
    
    /**
     * <summary>
     * 인게임 인벤토리 정보를 받아와 json파일로 저장함
     * </summary>
     */
    public static void Save_InGameInventory(List<ItemSlot> inven)
    {
        EasyToJson.ListToJson(inven, "InGameInventory", true);
    }
    
    /**
     * <summary>
     * json으로 저장된 인게임 인벤토리 정보를 불러와 반환해줌
     * </summary>
     */
    public static List<ItemSlot> Get_InGameInventory()
    {
        
        List<ItemSlot> inven = EasyToJson.ListFromJson<ItemSlot>("InGameInventory");
        if (inven == null)
        {
            inven = new List<ItemSlot>();
            Save_InGameInventory(inven);
        }

        return inven;
    }
    
    /**
     * <summary>
     * 인벤토리 정보를 받아와 json파일로 저장함
     * </summary>
     */
    public static void Save_Inventory(List<ItemSlot> inven)
    {
        EasyToJson.ListToJson(inven, "inventory", true);
    }
    

    /**
     * <summary>
     * json으로 저장된 유저 정보를 불러와 반환해줌
     * </summary>
     */
    public static SaveInfo Get_UserInfo()
    {
        // if (!File.Exists(_userInfoPath))
        // {
        //     SaveInfo userInfo = new SaveInfo();
        //     Save_userInfo(userInfo);
        //     return userInfo;
        //     
        // }

        
        SaveInfo saveData = EasyToJson.FromJson<SaveInfo>("userInfo");

        if (saveData != null)
        {
            return saveData;
        }

        SaveInfo userInfo = new SaveInfo();
        Save_userInfo(userInfo);
        return userInfo;

    }
    
    /**
     * <summary>
     * 유저 정보를 받아와 json파일로 저장함
     * </summary>
     */
    public static void Save_userInfo(SaveInfo saveInfo)
    {
        EasyToJson.ToJson(saveInfo, "userInfo", true);
    }
    
    
    /**
     * <summary>
     * 오디오 세팅 정보를 받아와 Json으로 저장함
     * </summary>
     */
    public static void Save_AudioSetting(AudioSetting audioSetting)
    {
        EasyToJson.ToJson(audioSetting, "AudioSetting", true);
    }
    
    /**
     * <summary>
     * json으로 저장된 오디오 세팅 정보를 불러와 반환해줌
     * </summary>
     */
    public static AudioSetting Get_AudioSetting()
    {
        AudioSetting audioSetting = EasyToJson.FromJson<AudioSetting>("AudioSetting");
        if (audioSetting == null)
        {
            audioSetting = new AudioSetting();
            Save_AudioSetting(audioSetting);
        }

        return audioSetting;
    }
    
}
