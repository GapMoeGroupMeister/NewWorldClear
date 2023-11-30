using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class ItemSOManager : MonoBehaviour
{
    /**
     * <summary>
     *
     * 아이템 SO 뭉탱이
     * </summary>
     */
    public static List<Item> itemSOBase;
    

    private void Awake()
    {
        
    }

    public static void LoadItem()
    {
        itemSOBase = Resources.LoadAll<Item>("SO/Item").ToList();

        object[] objectArr = AssetDatabase.LoadAllAssetsAtPath(Application.dataPath + "/00_Database/SO/Item");
        for (int i = 0; i < objectArr.Length; i++)
        {
            itemSOBase[i] = (Item)objectArr[i];
        }
    }
        
    /**
     * <summary>
     * 아이템 아이디로 찾기
     * </summary>
     */
    [CanBeNull]
    public static Item GetItem(int _id)
    {
        if (itemSOBase == null)
        {
            LoadItem();
        }
        foreach (Item item in itemSOBase)
        {
            if (item.id == _id)
            {
                return item;

            }
        }

        return null;
    }
    
    
    /**
     * <summary>
     * 아이템 이름으로 찾기
     * </summary>
     */
    [CanBeNull]
    public static Item GetItem(string _name)
    {
        if (itemSOBase == null)
        {
            LoadItem();
        }
        foreach (Item item in itemSOBase)
        {
            if (item.itemName == _name)
            {
                return item;

            }
        }

        return null;
    }
}
