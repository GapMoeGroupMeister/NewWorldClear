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
    [SerializeField]
    PlayerInputReader _inputReader;


    public float attackDelay;
    public float attackDamage = 10;
    public float dashCooltime;
    public float dashElapsedTime = 0f;
    

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

        _inputReader.MoveEvent += Move;
        _inputReader.DashEvent += Dash;
        _weaponTrm = transform.Find("Weapon");
        _moveSpeed = 5f;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= Move;
        _inputReader.DashEvent -= Dash;
    }

    private void Update()
    {
        WeaponRotate();
    }

    public void Move(Vector2 value)
    {
        if (isStun) return;
        _rigidbody.velocity = value.normalized * _moveSpeed;
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

    public void Dash()
    {
        if (dashElapsedTime > 0 || _rigidbody.velocity == Vector2.zero) return;
        StartCoroutine(IEDash());
    }

    IEnumerator IEDash()
    {
        Vector2 prevDir = _rigidbody.velocity.normalized;
        _rigidbody.velocity = prevDir * _moveSpeed * 5f;
        isStun = true;
        while (dashElapsedTime > 0)
        {
            dashElapsedTime -= Time.deltaTime;
            yield return null;
        }
        isStun = false;
        _rigidbody.velocity = Vector2.zero;
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
