using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DropItemObject : DropObject
{
    [SerializeField] private DropItem _dropItem;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = transform.Find("ItemImg").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetInfo(_dropItem);
    }

    private void Update()
    {
        Update_Check();
    }

    public override void Get()
    {
        
        GetItem();
    }

    public void SetInfo(DropItem dropItem)
    {
        _dropItem = dropItem;
        _spriteRenderer.sprite = SpriteLoader.FindSprite(_dropItem.item.itemSpriteName);

    }


    private void GetItem()
    {
        ItemManager.Instance.AddItem(new ItemSlot(_dropItem.item, _dropItem.amount));
        
        // 풀링 추가되면 넣어야함
        Destroy(gameObject);
        
    }
    
    
    
}