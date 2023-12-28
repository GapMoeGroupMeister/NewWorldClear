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

    [SerializeField] private UIInfo itemTake;
    [SerializeField] private UIInfo itemTook;
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
        ItemManager.Instance.LoadInventoryFile();

        SceneStart();

    }

    public override void SceneStart()
    {
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
        List<ItemSlot> slotToRemove = new List<ItemSlot>();

        if (inventory == null || inventory.Count <= 0)
        {
            inventory = new List<ItemSlot>();
            return;
        }

        foreach (ItemSlot _slot in inventory)
        {
            Item thisItem = _slot.item;

            if (thisItem == null || _slot.amount <= 0)
            {
                slotToRemove.Add(_slot);
                continue;
            }

            if ((isReadyWeaponSelect && thisItem.Type != ItemType.Weapon) || isReadyInventorySelect == false)
            {
                return;
            }

            Set_Slot(_slot);
        }

        foreach(ItemSlot _slot in slotToRemove)
        {
            inventory.Remove(_slot);
        }
    }

    protected new void Refresh()
    {
        Delete_Inven();
        Set_AllSlot();
        ItemManager.Instance.SaveInventoryFile();
    }

    public void TakeItem(Item item, float durability, int itemAmount)
    {
        itemTake.MovePos();

        itemTake.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(item.itemName);
        itemTake.transform.Find("ItemType").GetComponent<TextMeshProUGUI>().SetText(Enum.GetName(item.Type.GetType(), item.Type));
        itemTake.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().SetText(item.description);
        itemTake.transform.Find("Divide").GetComponent<StartScene_ItemDivider>().SetDivide(itemAmount);

        float dur;
        if (item.maxDurability > 0)
        {
            dur = durability / item.maxDurability;
        }
        else dur = 1f;
        itemTake.transform.Find("DurabilityGaugeBG/DurabilityGauge").GetComponent<Image>().fillAmount = dur;
    }

    public void SetItem()
    {
        ItemSlot newSlot = new ItemSlot();
        newSlot.item = curSelectItemSlot.item;
        newSlot.amount = itemTake.transform.Find("Divide").GetComponent<StartScene_ItemDivider>().CurAmount;
        newSlot.durability = curSelectItemSlot.durability;

        ItemManager.Instance.SubItem(curSelectItemSlot.item, newSlot.amount);

        if (isReadyInventorySelect)
        {
            takeInventory.SetItem(newSlot);
        }
        else if (isReadyWeaponSelect)
        {
            weaponSlot.SetItem(newSlot);
        }

        itemTake.MoveOff();
        Refresh();
    }

    public void TookItem(Item item, float durability, int itemAmount)
    {
        itemTook.MovePos();

        itemTook.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(item.itemName);
        itemTook.transform.Find("ItemType").GetComponent<TextMeshProUGUI>().SetText(Enum.GetName(item.Type.GetType(), item.Type));
        itemTook.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().SetText(item.description);
        itemTook.transform.Find("Divide").GetComponent<StartScene_ItemDivider>().SetDivide(itemAmount);

        float dur;
        if (item.maxDurability > 0)
        {
            dur = durability / item.maxDurability;
        }
        else dur = 1f;
        itemTook.transform.Find("DurabilityGaugeBG/DurabilityGauge").GetComponent<Image>().fillAmount = dur;
    }

    public void SetItemInStorage()
    {
        ItemSlot newSlot = new ItemSlot();
        newSlot.item = curSelectItemSlot.item;
        newSlot.amount = itemTook.transform.Find("Divide").GetComponent<StartScene_ItemDivider>().CurAmount;
        newSlot.durability = curSelectItemSlot.durability;

        if (isReadyWeaponSelect)
        {
            weaponSlot.SubItem(newSlot.amount);
        }
        else if (isReadyInventorySelect)
        {
            takeInventory.SubItem(curSelectItemSlot, newSlot.amount);
        }

        ItemManager.Instance.AddItem(curSelectItemSlot, newSlot.amount);

        itemTook.MoveOff();
        Refresh();
    }
}
