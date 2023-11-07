using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Storage_InventoryManager : MonoBehaviour
{
    [Tooltip("인벤슬롯 프리팹")]
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private Transform grid;
    private static Transform _grid;

    [SerializeField] private List<ItemSlot> inventory;

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
        foreach (ItemSlot _slot in inventory)
        {
            Set_Slot(_slot);
        }
    }
    
    private void Set_Slot(ItemSlot itemSlot)
    {
        GameObject slot = Instantiate(slotPrefab, _grid);
        slot.GetComponent<Slot>().SetSlot(itemSlot);

    }

    [MenuItem("Debug/Inventory/SlotsRefresh")]
    private static void Refresh_Setting()
    {
        
    }
    [MenuItem("Debug/Inventory/SlotsRefresh")]
    private static void Delete_Inven()
    {
        foreach (Transform child in _grid)
        {
            Destroy(child.gameObject) ;
        }
    }

    private void LoadInventoryFile()
    {
        inventory = DBManager.Instance.Get_Inventory();
    }

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
