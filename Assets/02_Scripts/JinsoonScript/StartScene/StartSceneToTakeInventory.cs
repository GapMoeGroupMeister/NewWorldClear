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
            if (s.item.id == itemSlot.item.id)                           //���� �������� �ְ�
            {
                int itemNum = s.amount + itemSlot.amount;
                if (itemNum > s.item.SlotSetAmount)      //�� �������� ĭ�� ���� �� ���� �κ��丮�� ������ �� 
                {
                    s.amount = s.item.SlotSetAmount;

                    itemSlot.amount = itemNum - s.item.SlotSetAmount;
                    inventory.Add(itemSlot);
                    GameObject sl = Instantiate(slotPrefab, grid);

                    sl.GetComponent<Slot>().SetSlot(itemSlot);
                }
                else                                    //�׷��� �ʴٸ�
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
        List<ItemSlot> slotToRemove = new List<ItemSlot>();
        Delete_Inven();

        if (inventory == null || inventory.Count <= 0)
        {
            ItemManager.Instance.inventory = new List<ItemSlot>();
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

            GameObject slot = Instantiate(slotPrefab, grid);
            slot.GetComponent<Slot>().SetSlot(_slot);
        }

        foreach (ItemSlot _slot in slotToRemove)
        {
            inventory.Remove(_slot);
        }

        DBManager.Save_InGameInventory(inventory);
    }

    internal void SubItem(ItemSlot curSelectItemSlot, int amount)
    {
        foreach (ItemSlot _slot in inventory)
        {
            if(_slot == curSelectItemSlot)
            {
                _slot.amount -= amount;
            }
        }
        Refresh();
    }
}