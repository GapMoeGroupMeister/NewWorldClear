using UnityEngine;

public class DropExpObject : DropObject
{
    [SerializeField] private int ExpAmount;
    
    public override void Get()
    {
        GetExp();
    }

    private void GetExp()
    {
        
    }
}