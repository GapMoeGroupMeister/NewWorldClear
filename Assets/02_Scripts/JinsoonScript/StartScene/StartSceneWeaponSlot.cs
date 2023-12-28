using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartSceneWeaponSlot : MonoBehaviour, IPointerClickHandler
{
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

        slot.GetComponent<Slot>().SetSlot(itemSlot);
    }

    public void Delete_Inven()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartSceneStorageInventoryManager.instance.ClickWeaponSlot();
        onClick?.Invoke();
    }
}
