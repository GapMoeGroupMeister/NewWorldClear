using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public enum Debuffs
{
    None = 0,
    Slow = 1,
    Stun = 2,
    Subdue = 4,
    Poison = 8
}

public enum Buffs
{
    None = 0,
    Fast = 1,
    Generation = 2,
    PowerUp = 4,
    ThinSheild = 8,
}

public class PlayerController : MonoBehaviour , IDamageable
{
    public Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    Transform _weaponTrm;
    Transform _weaponVisualTrm;

    private float _moveSpeed = 5f;

    private float _maxHp = 100f;
    private float _currentHp = 100f;

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
        if(Buffs.Generation == (currentBuffs & Buffs.Generation))
        {
            Debug.Log("재생중");
        }
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

    public void HitDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void BleedDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void PoisonDamage(float damage)
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
