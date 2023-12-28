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
        inventory.Add(itemSlot);
        GameObject slot = Instantiate(slotPrefab, grid);

        slot.GetComponent<Slot>().SetSlot(itemSlot);
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
}