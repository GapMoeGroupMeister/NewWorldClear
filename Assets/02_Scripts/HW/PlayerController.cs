using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;



public class PlayerController : Damageable
{
    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    Transform _weaponTrm;
    Transform _weaponVisualTrm;

    public Debuffs currentDebuffs = Debuffs.None;
    public Buffs currentBuffs = Buffs.None;

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
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _weaponTrm = transform.Find("Weapon");
        _weaponVisualTrm = _weaponTrm.Find("Visual");
        currentBuffs |= Buffs.Generation;
        AddBuff(Buffs.Generation, 5f);
    }

    private void Update()
    {
        Move();
        WeaponRotate();
    }

    private void Move()
    {
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

    public override void HitDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public override void BleedDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public override void PoisonDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("죽은");
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
