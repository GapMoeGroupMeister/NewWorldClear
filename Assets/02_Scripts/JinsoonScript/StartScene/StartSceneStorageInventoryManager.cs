using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneStorageInventoryManager : InventoryManager
{
    public static StartSceneStorageInventoryManager instance;

    public bool isEditMode = false;
    public List<ItemSlot> inventory;

    [SerializeField] private UIInfo uiInfo;
    public bool isReadyWeaponSelect { private set; get;  }
    public bool isReadyInventorySelect {  private set; get; }

    private void Awake()
    {
        if(instance != null)
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
    }
    public void ClickInventorySlot()
    {
        isReadyWeaponSelect = false;
        isReadyInventorySelect = !isReadyInventorySelect;
    }

    public void TakeItem()
    {
        uiInfo.MovePos();
    }
}
