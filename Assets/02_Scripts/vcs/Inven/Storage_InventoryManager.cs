using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Storage_InventoryManager : MonoBehaviour
{
    [Tooltip("인벤슬롯 프리팹")]
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private Transform grid;
    private static Transform _grid;

    private static List<ItemSlot> inventory;

    [SerializeField] private ItemSlot defaultItem;

    private void Awake()
    {
        _grid = grid;
    }

    void Start()
    {
        LoadInventoryFile();
        Set_AllSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Set_AllSlot()
    {
        if (inventory == null)
            return;
        
        foreach (ItemSlot _slot in inventory)
        {
            Set_Slot(_slot);
        }
    }
    
    private void Set_Slot(ItemSlot itemSlot)
    {
        GameObject slot = Instantiate(slotPrefab, grid);
        slot.GetComponent<Slot>().SetSlot(itemSlot);

    }

    [MenuItem("Debug/Inventory/SlotsRefresh")]
    private static void Debug_Refresh()
    {
        
    }

    [MenuItem("Debug/Inventory/SlotsRefresh")]
    private static void Debug_Delete()
    {
    }
    
    [MenuItem("Debug/Inventory/SlotsRefresh")]
    private static void Debug_AddItem()
    {
        AddItem(new ItemSlot());
    }
    
    /**
     * <summary>
     * inventory 전체를 지웠다가 다시 만듬
     * </summary>
     */
    private void Refresh_Setting()
    {
            
    }
    /**
     * <summary>
     * inventory 전체를 지움
     * </summary>
     */
    private static void Delete_Inven()
    {
        foreach (Transform child in _grid)
        {
            Destroy(child.gameObject) ;
        }
    }

    public static void AddItem(ItemSlot itemSlot)
    {
        
        
    }

    [CanBeNull]
    public static ItemSlot Finditem(string itemName)
    {
        ItemSlot targetItem = new ItemSlot(){amount = -10};

        for (int i = 0; i < inventory.Count; i++)
        {
            ItemSlot slot = inventory[i];
            if (targetItem.amount <= 0)
            {
                
            }
        }
        foreach (ItemSlot item in inventory)
        {
            
            targetItem = item;
        }

        if (targetItem.amount <= 0)
        {
            return null;
        }
        //int index = inventory.IndexOf(targetItem);
        return targetItem;
    }

    
    /**
     * <summary>
     * json에서 인벤토리 파일을 불러옴
     * </summary>
     */
    private void LoadInventoryFile()
    {
        inventory = DBManager.Instance.Get_Inventory();
    }
    /**
     * <summary>
     * 인벤토리 파일을 json으로 저장함
     * </summary>
     */
    private void SaveInventoryFile()
    {
        DBManager.Instance.Save_Inventory(inventory);
    }

    private void SlotChange()
    {
        
    }

    public void SceneExit()
    {
        SaveInventoryFile();
    }
    
}
