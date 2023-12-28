using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneStorageInventoryManager : InventoryManager
{
    public static StartSceneStorageInventoryManager instance;

    public bool isEditMode = false;
    public ItemSlot curSelectItemSlot;

    [SerializeField] private UIInfo uiInfo;
    [SerializeField] private StartSceneWeaponSlot weaponSlot;
    [SerializeField] private StartSceneToTakeInventory takeInventory;

    public bool isReadyWeaponSelect { private set; get; }
    public bool isReadyInventorySelect { private set; get; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {
        SceneStart();
    }

    public override void SceneStart()
    {
        ItemManager.Instance.LoadInventoryFile();
        Set_AllSlot();
    }

    public override void SceneExit()
    {
        ItemManager.Instance.SaveInventoryFile();
    }

    public void ClickWeaponSlot()
    {
        isReadyWeaponSelect = !isReadyWeaponSelect;
        isReadyInventorySelect = false;
        SceneStart();
    }
    public void ClickInventorySlot()
    {
        isReadyWeaponSelect = false;
        isReadyInventorySelect = !isReadyInventorySelect;
        SceneStart();
    }

    protected new void Set_AllSlot()
    {
        Delete_Inven();
        List<ItemSlot> inventory = ItemManager.Instance.inventory;

        if (inventory == null || inventory.Count <= 0)
        {
            inventory = new List<ItemSlot>();
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

            if ((isReadyWeaponSelect && thisItem.Type != ItemType.Weapon) || isReadyInventorySelect == false)
            {
                return;
            }

            Set_Slot(_slot);
        }
    }

    protected new void Refresh()
    {
        Delete_Inven();
        Set_AllSlot();
    }

    public void TakeItem(Item item, float durability)
    {
        uiInfo.MovePos();

        uiInfo.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(item.itemName);
        uiInfo.transform.Find("ItemType").GetComponent<TextMeshProUGUI>().SetText(Enum.GetName(item.Type.GetType(), item.Type));
        uiInfo.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().SetText(item.description);
        uiInfo.transform.Find("DurabilityGaugeBG/DurabilityGauge").GetComponent<Image>().fillAmount = durability / item.maxDurability;
    }

    public void SetItem()
    {
       ItemSlot newSlot = curSelectItemSlot;
        int itemAmount = curSelectItemSlot.amount;

        ItemManager.Instance.SubItem(curSelectItemSlot.item, itemAmount);

        newSlot.amount = itemAmount;
        if (isReadyInventorySelect)
        {
            takeInventory.SetItem(newSlot);
        }
        else if (isReadyWeaponSelect)
        {
            weaponSlot.SetItem(newSlot);
        }

        uiInfo.MoveOff();
        Refresh();
    }
}
