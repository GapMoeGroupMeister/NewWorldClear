using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class DBManager : MonoBehaviour
{

    private string _inventoryPath;
    private string _userInfoPath;


    private void Awake()
    {
        _inventoryPath = Path.Combine(Application.dataPath, "00_Database/Json/inventory.json");
        _userInfoPath = Path.Combine(Application.dataPath, "00_Database/Json/userInfo.json");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<ItemSlot> Get_Inventory()
    {   
        if (File.Exists(_inventoryPath))
        {

            string loadJson = File.ReadAllText(_inventoryPath);
            List<ItemSlot> saveData = JsonUtility.FromJson<List<ItemSlot>>(loadJson);

            if (saveData != null)
            {
                return saveData;
                
            }
        }

        List<ItemSlot> inven = new List<ItemSlot>();
        
        Save_Inventory(inven);
        return inven;
    }
    /**
     * <summary>
     * 
     * </summary>
     */
    public void Save_Inventory(List<ItemSlot> inven)
    {
        string json = JsonUtility.ToJson(inven, true);

        File.WriteAllText(_inventoryPath, json);
    }

    public SaveInfo Get_UserInfo()
    {
        if (!File.Exists(_userInfoPath))
        {
            
            JsonSave();
        }else
        {

            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<AccountStatus>(loadJson);

            if (saveData != null)
            {
                AccountManager.Instance.account = saveData;
                
            }
        }
    }
    
    public void JsonLoad()
    {
        AccountStatus saveData = new AccountStatus();
        if (!File.Exists(path))
        {
            AccountManager.Instance.UserInformReset();

            JsonSave();
        }
        else
        {

            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<AccountStatus>(loadJson);

            if (saveData != null)
            {
                AccountManager.Instance.account = saveData;
                
            }
        }
    }

    public void JsonSave()
    {
        AccountStatus saveData = new AccountStatus();


        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }

    
}
