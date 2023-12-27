using UnityEngine;

public class Storage_InventoryManager : InventoryManager
{
    
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

    [ContextMenu("Debug_Refresh")]
    private void DebugRefresh()
    {
        Refresh();
    }

    [ContextMenu("Debug_AddItem")]
    private void DebugAddDefaultItem()
    {
        ItemManager.Instance.AddItem(defaultItem);
    }
    
    [ContextMenu("Debug_AddCanSoupItem")]
    private void DebugAddCanSoupItem()
    {
        ItemManager.Instance.AddItem(new ItemSlot(canSoup.item, canSoup.amount, canSoup.durability));
    }
    
        
    /**
     * <summary>
     * 아이템 티어 순서대로 자동 정렬
     * </summary>
     */
    public void SortByTier()
    {
        
    }
}
