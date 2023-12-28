using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetRange : MonoBehaviour
{
    [SerializeField] private CircleCollider2D RangeCollider;
    [SerializeField] private float defaultRange = 1;
    private void Awake()
    {
        RangeCollider = GetComponent<CircleCollider2D>();
        RangeCollider.radius = defaultRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * <summary>
     * amount만큼 획득 범위를 증가시킵니다
     * </summary>
     */
    public void ExpandRange(float amount)
    {
        RangeCollider.radius += amount;
    }
    

    /**
     * <summary>
     * 근방 일대의 모든 드롭 아이템을 끌고옵니다
     * </summary>
     */
    public void GetAll()
    {
        float before = RangeCollider.radius;
        RangeCollider.radius = 200;
        RangeCollider.radius = before;

    }
}
