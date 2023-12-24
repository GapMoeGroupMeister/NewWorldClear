using UnityEngine;

public class DropExpObject : DropObject
{
    [SerializeField] private int ExpAmount;
    
    private void Update()
    {
        Update_Check();
    }
    
    public override void Get()
    {
        GetExp();
    }

    private void GetExp()
    {
        LootManager.Instance._LevelSystem.AddExp(ExpAmount);
        Destroy(gameObject);
    }
}