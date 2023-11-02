using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Transform _weaponTrm;
    Transform _weaponVisualTrm;

    private float _moveSpeed = 5f;

    private float _maxHp = 100f;
    private float _currentHp = 100f;

    #region 얘네로 뭐하는지 정확히 알 수 없음
    private float _maxStemina = 200f;
    private float _currentStemina = 200f;

    private float _maxFullness = 100f;
    private float _currentFullness = 100f;

    private float _maxThirstiness = 100f;
    private float _currentThirstiness = 100f;
    #endregion

    public WeaponSO weaponSO;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weaponTrm = transform.Find("Weapon");
        _weaponVisualTrm = _weaponTrm.Find("Visual");
    }

    private void Update()
    {
        Move();
        WeaponRotate();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * _moveSpeed;

    }

    private void WeaponRotate()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _weaponTrm.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        if (Mathf.Abs(_weaponTrm.rotation.z) >= 0.5f)
        {
            _weaponVisualTrm.localScale = -Vector2.one;
        }
        else
        {
            _weaponVisualTrm.localScale = Vector2.one;
        }
    }

    #region Stats Change
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
