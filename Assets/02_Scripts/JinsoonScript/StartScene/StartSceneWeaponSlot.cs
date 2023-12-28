using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using EasyJson;
using System.IO;
using TMPro;

public class StartSceneWeaponSlot : MonoBehaviour, IPointerClickHandler
{
    private string path = "InGameWeapon";

    public ItemSlot itemSlot;
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
        outline.SetActive(StartSceneStorageInventoryManager.instance.isReadyWeaponSelect);
    }

    public void SetItem(ItemSlot itemSlot)
    {
        this.itemSlot = itemSlot;
        GameObject slot = Instantiate(slotPrefab, grid);
        transform.Find("WeaponName").GetComponent<TextMeshProUGUI>().SetText(itemSlot.item.itemName);

        slot.GetComponent<Slot>().SetSlot(itemSlot);
        SaveWeapon();
    }

    public void Delete_Inven()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }
    }

    public void SubItem(int amount)
    {
        itemSlot.amount -= amount;
        if(itemSlot.amount <= 0)
        {
            for(int i = 0; i < grid.childCount; i++)
            {
                Destroy(grid.GetChild(i).gameObject);
            }
        }
        SaveWeapon();
    }

    private void SaveWeapon()
    {
        EasyToJson.ToJson(itemSlot, path, true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartSceneStorageInventoryManager.instance.ClickWeaponSlot();
        onClick?.Invoke();
    }
}
