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
    
    /**
     * <summary>
     * 아이템 정보를 에셋 창에서 가져오는 메서드
     * </summary>
     */
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
     * <returns>
     * 해당하는 Id의 아이템을 찾아 반환
     * </returns>
     */
    [CanBeNull]
    public static Item GetItem(int _id)
    {
        print(_id);
        if (itemSOBase == null)
        {
            LoadItem();
        }

        if (itemSOBase == null)
        {
            print("null임");
        }
        foreach (Item item in itemSOBase)
        {
            print(item.itemName +" > "+item.id);
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
     * <returns>
     * 해당하는 이름의 아이템을 찾아 반환
     * </returns>
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
