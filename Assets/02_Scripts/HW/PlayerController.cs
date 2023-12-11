using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
public class PlayerController : Damageable
{
    public static PlayerController Instance;
    public Rigidbody2D _rigidbody;
    Animator _animator;

    Transform _weaponTrm;

    public float attackDelay;
    public float attackDamage = 10;
    

    #region 얘네로 뭐하는지 정확히 알 수 없음
    private float _maxStemina = 200f;
    private float _currentStemina = 200f;

    private float _maxFullness = 100f;
    private float _currentFullness = 100f;

    private float _maxThirstiness = 100f;
    private float _currentThirstiness = 100f;
    #endregion

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _weaponTrm = transform.Find("Weapon");
        _moveSpeed = 5f;
    }

    private void Update()
    {
        Move();
        WeaponRotate();
    }

    private void Move()
    {
        if (isStun) return;
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * _moveSpeed;
        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
    }

    private void WeaponRotate()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _weaponTrm.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        if (Mathf.Abs(_weaponTrm.rotation.z) > 0.7f)
        {
            transform.localScale = new Vector2(-1, 1);
            _weaponTrm.localScale = -Vector2.one;
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            _weaponTrm.localScale = Vector2.one;
        }
    }


    #region Stat Change functions
    public void ChangeHP(float amount)
    {
        _currentHp += amount;
    }

    public void ChangeStemina(float amount)
    {
        _currentStemina += amount;
    }

    public void ChangeFullness(float amount)
    {
        _currentStemina += amount;
    }

    public void ChangeThirstiness(float amount)
    {
        _currentThirstiness += amount;
    }
    #endregion
}
