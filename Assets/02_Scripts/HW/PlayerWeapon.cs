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

    [SerializeField]
    LayerMask _enemyMask;

    public WeaponSO testWeapon;

    private void Awake()
    {
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && testWeapon != null)
        {
            WeaponChange(testWeapon);
        }
        
    }

    public void WeaponChange(WeaponSO weaponSO)
    {
        _name = weaponSO.name;
        _desc = weaponSO.desc;
        _weaponSprite = weaponSO.weaponSprite;
        _damage = weaponSO.damage;
        _attackDelay = weaponSO.attackDelay;
        _attackRange = weaponSO.attackRange;

        _spriteRenderer.sprite = _weaponSprite;

        if(_weaponType.Equals(WeaponType.ShortRange)) 
        {
            CancelInvoke("ShortRangeAttack");
        }
        else if(_weaponType.Equals(WeaponType.LongRange))
        {
            CancelInvoke("LongRangeAttack");
        }

        _weaponType = weaponSO.weaponType;

        if (_weaponType.Equals(WeaponType.ShortRange))
        {
            InvokeRepeating("ShortRangeAttack", _attackDelay, _attackDelay);
        }
        else if (_weaponType.Equals(WeaponType.LongRange))
        {
            InvokeRepeating("LongRangeAttack", _attackDelay, _attackDelay);
        }
    }

    private void ShortRangeAttack()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position + ((Vector3)dir.normalized * (_attackRange.x / 2)), _attackRange, Mathf.Atan2(dir.x, dir.x), _enemyMask);
         if (enemies.Length > 0) 
        {
            Debug.Log($"공격에 맞은 적의 수 : {enemies.Length}");
            foreach(Collider2D col in enemies)
            {
                Debug.Log($"공격에 맞은 놈 : {col.name}");
            }
        }
        else
        {
            Debug.Log("공격에 아무도 맞지 않음");
        }
    }

    private void LongRangeAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _attackRange.x, _enemyMask);
        if(enemies.Length > 0) 
        {
            Debug.Log($"겨냥된 놈 : {enemies[0].name}"); 
        }
        else
        {
            Debug.Log("사거리 내에 아무도 없음");
        }
    }
}
