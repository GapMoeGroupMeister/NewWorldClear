using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using EasyJson;

public class DBManager : MonoBehaviour
{

    private string _inventoryPath;
    private string _userInfoPath;


    private void Awake()
    {
        _userInfoPath = Path.Combine(Application.dataPath+"00_Database/Json/", "userInfo.json");
    }
    
    

    
    
    /**
     * <summary>
     * json으로 저장된 인벤토리 정보를 불러와 반환해줌
     * </summary>
     */
    public static List<ItemSlot> Get_Inventory()
    {
        return EasyToJson.ListFromJson<ItemSlot>("inventory");
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
    public SaveInfo Get_UserInfo()
    {
        if (File.Exists(_userInfoPath))
        {
            string loadJson = File.ReadAllText(_userInfoPath);
            SaveInfo saveData = JsonUtility.FromJson<SaveInfo>(loadJson);

            if (saveData != null)
            {
                return saveData;
            }
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
    public void Save_userInfo(SaveInfo saveInfo)
    {
        string json = JsonUtility.ToJson(saveInfo, true);

        File.WriteAllText(_userInfoPath, json);
    }
    
    
    
}
