using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Storage_InventoryManager : MonoBehaviour
{
    [Tooltip("인벤슬롯 프리팹")]
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private Transform grid;

    [SerializeField] private ItemSlot defaultItem;
    [SerializeField] private ItemSlot canSoup;


    public bool isEditMode = false;
    private ItemSlot MoveSlot;
    private ItemSlot targetSlot;
    private void Awake()
    {
        
    }

    void Start()
    {
        ItemManager.Instance.LoadInventoryFile();
        Set_AllSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /**
     * <summary>
     * inventory 전체를 지웠다가 다시 만듬
     * </summary>
     */
    private void Refresh()
    {
        Delete_Inven();
        Set_AllSlot();
        
    }
    private void Set_AllSlot()
    {
        List<ItemSlot> inventory = ItemManager.Instance.inventory;
        if (inventory.Count <= 0)
        {
            return;

        }
        
        foreach (ItemSlot _slot in inventory)
        {
            Item thisItem = _slot.item;

            if (thisItem == null)
            {
                inventory.Remove(_slot);
                return;
            }
            
            Set_Slot(_slot);
        }
    }
    private void Set_Slot(ItemSlot itemSlot)
    {
        GameObject slot = Instantiate(slotPrefab, grid);
        slot.GetComponent<Slot>().SetSlot(itemSlot);

    }

    
    


    
    private void Refresh_Setting()
    {
            
    }
    /**
     * <summary>
     * inventory 전체를 지움
     * </summary>
     */
    private void Delete_Inven()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject) ;
        }
    }
    
    public void SceneExit()
    {
        ItemManager.Instance.SaveInventoryFile();
        
    }


}
