using System;
using UnityEngine;

public class DropItemObject : MonoBehaviour
{
    [SerializeField] private DropItem _dropItem;

    private bool PlayerTargeted = false;

    [Range(0.5f, 5f)] [SerializeField] private float detectRange = 0.5f;


    public void TracePlayer()
    {
        
        
    }


    private void DetectPlayer()
    {
        // 플레이어 레이어 추가되면 설정
        Physics2D.OverlapCircleAll(transform.position, detectRange);
        
        
        
    }
}