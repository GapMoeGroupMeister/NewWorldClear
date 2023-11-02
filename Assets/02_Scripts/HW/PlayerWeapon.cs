using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private string _name;
    private string _desc;
    private Sprite _weaponSprite;
    private float _damage;
    private float _attackDelay;
    private WeaponType _weaponType;
    private Vector2 _attackRange;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
    }

    public void WeaponChange(WeaponSO weaponSO)
    {
        _name = weaponSO.name;
        _desc = weaponSO.desc;
        _weaponSprite = weaponSO.weaponSprite;
        _damage = weaponSO.damage;
        _attackDelay = weaponSO.attackDelay;
        _weaponType = weaponSO.weaponType;
        _attackRange = weaponSO.attackRange;

        _spriteRenderer.sprite = _weaponSprite;

        CancelInvoke("Weapon");
        InvokeRepeating("Weapon", _attackDelay, _attackDelay);
    }

    private void Attack()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, _attackRange, Mathf.Atan2(dir.x, dir.x));
        if(enemies.Length > 0) 
        {
            print(enemies.Length);
            Debug.Log($"공격에 맞은 적의 수 : {enemies.Length}");
        }
        else
        {
            Debug.Log("공격에 아무도 맞지 않음");
        }
    }

    private void LongRangeAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _attackRange.x);
        if(enemies.Length > 0) 
        {
            Debug.Log($"겨냥된 놈 : {enemies[0].name}"); 
        }
    }
}
