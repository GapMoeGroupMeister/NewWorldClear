using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartSceneToTakeInventory : MonoBehaviour, IPointerClickHandler
{
    public List<ItemSlot> inventory;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private RectTransform grid;

    public UnityEvent onClick;

    private GameObject outline;

    private void Awake()
    {
        outline = transform.Find("Outline").gameObject;
        outline.SetActive(false);
    }

    private void Update()
    {
        outline.SetActive(StartSceneStorageInventoryManager.instance.isReadyInventorySelect);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartSceneStorageInventoryManager.instance.ClickInventorySlot();
        onClick?.Invoke();
    }

    public void SetItem(ItemSlot itemSlot)
    {
        foreach (ItemSlot s in inventory)
        {
            if (s.item.id == itemSlot.item.id)                           //같은 아이템이 있고
            {
                int itemNum = s.amount + itemSlot.amount;
                if (itemNum > s.item.SlotSetAmount)      //그 아이템의 칸당 소지 수 보다 인벤토리의 아이템 수 
                {
                    s.amount = s.item.SlotSetAmount;

                    itemSlot.amount = itemNum - s.item.SlotSetAmount;
                    inventory.Add(itemSlot);
                    GameObject sl = Instantiate(slotPrefab, grid);

                    sl.GetComponent<Slot>().SetSlot(itemSlot);
                }
                else                                    //그렇지 않다면
                {
                    s.amount = itemNum;
                }

                Refresh();
                return;
            }
        }

        inventory.Add(itemSlot);
        GameObject slot = Instantiate(slotPrefab, grid);

        slot.GetComponent<Slot>().SetSlot(itemSlot);
        Refresh();
    }

    public void Delete_Inven()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnStartGame()
    {
        DBManager.Save_InGameInventory(inventory);
    }

    private void Refresh()
    {
        Delete_Inven();

        if (inventory == null || inventory.Count <= 0)
        {
            ItemManager.Instance.inventory = new List<ItemSlot>();
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

            GameObject slot = Instantiate(slotPrefab, grid);
            slot.GetComponent<Slot>().SetSlot(_slot);
        }
    }
}