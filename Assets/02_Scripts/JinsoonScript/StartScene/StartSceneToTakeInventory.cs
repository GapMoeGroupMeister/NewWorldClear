using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSceneToTakeInventory : MonoBehaviour, IPointerClickHandler
{
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
    }
}
