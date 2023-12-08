using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DropItemObject : MonoBehaviour
{
    [SerializeField] private DropItem _dropItem;

    private bool PlayerTargeted = false;

    [Range(0.5f, 5f)] [SerializeField] private float detectRange = 0.5f;

    private Transform followTarget;
    private Vector2 currentDirection;

    public void TracePlayer()
    {
        
        
    }


    private void DetectPlayer()
    {
        // 플레이어 레이어 추가되면 설정
        Collider2D[] hits =  Physics2D.OverlapCircleAll(transform.position, detectRange);

        foreach (Collider2D collider in hits)
        {
            if (collider.CompareTag("Player"))
            {
                followTarget = collider.transform;
            }
        }
    }

    private IEnumerator FollowRoutine()
    {
        
    }
    
}