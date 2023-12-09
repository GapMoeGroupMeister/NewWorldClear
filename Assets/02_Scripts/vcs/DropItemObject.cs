using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DropItemObject : DropObject
{
    [SerializeField] private DropItem _dropItem;

    private void Update()
    {
        Update_Check();
    }

    public override void Get()
    {
        
    }


    
    
    
}