using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{
    private string _name;
    private string _desc;
    private Sprite _weaponSprite;
    private float _damage;
    private float _attackDelay;
    private WeaponType _weaponType;
    private Vector2 _attackRange;
    private WeaponEvent _weaponEvent;

    public GameObject effect;

    Transform _nearestEnemy;

    SpriteRenderer _spriteRenderer;
    LineRenderer _lineRenderer;

    [SerializeField]
    LayerMask _enemyMask;

    public WeaponSO testWeapon;


    private void Awake()
    {
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && testWeapon != null)
        {
            WeaponChange(testWeapon);
        }
        if (_weaponType == WeaponType.LongRange)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _attackRange.x, _enemyMask);
            if (enemies.Length > 0)
            {
                float distance = float.MaxValue;
                foreach (Collider2D col in enemies)
                {
                    if (Vector2.Distance(transform.position, col.transform.position) < distance)
                    {
                        distance = Vector2.Distance(transform.position, col.transform.position);
                        _nearestEnemy = col.transform;
                    }
                }
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _nearestEnemy.position);
            }
            else
            {
                _nearestEnemy = null;
            }
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
        _weaponEvent = (WeaponEvent)WeaponEventManager.Instance.GetComponent(_name);

        _spriteRenderer.sprite = _weaponSprite;

        if (_weaponType.Equals(WeaponType.ShortRange))
        {
            CancelInvoke("ShortRangeAttack");
        }
        else if (_weaponType.Equals(WeaponType.LongRange))
        {
            CancelInvoke("LongRangeAttack");
            _lineRenderer.enabled = false;
        }

        _weaponType = weaponSO.weaponType;

        if (_weaponType.Equals(WeaponType.ShortRange))
        {
            InvokeRepeating("ShortRangeAttack", _attackDelay, _attackDelay);
        }
        else if (_weaponType.Equals(WeaponType.LongRange))
        {
            InvokeRepeating("LongRangeAttack", _attackDelay, _attackDelay);
            _lineRenderer.enabled = true;
        }
    }

    private void ShortRangeAttack()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 attackRange = transform.position + ((Vector3)dir.normalized * (_attackRange.x / 2));
        float angle = Mathf.Atan2(dir.y, dir.x);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackRange, _attackRange, angle * Mathf.Rad2Deg, _enemyMask);
        GameObject obj = Instantiate(effect, attackRange, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
        obj.transform.localScale = _attackRange;
        Destroy(obj, 0.5f);
        if (enemies.Length > 0)
        {
            Debug.Log($"공격에 맞은 적의 수 : {enemies.Length}");
            foreach (Collider2D col in enemies)
            {
                Debug.Log($"공격에 맞은 놈 : {col.name}");
                col.GetComponent<Enemy_TEST>().Damage(_damage);
                if(_weaponEvent != null)
                {
                    _weaponEvent.OnHit(col.transform);
                }
            }
        }
    }

    private void LongRangeAttack()
    {
        if (_nearestEnemy == null) return;
        _nearestEnemy.GetComponent<Enemy_TEST>().Damage(_damage);
    }


}
