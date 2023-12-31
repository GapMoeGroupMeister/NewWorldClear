using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : Damageable
{
    public static PlayerController Instance;
    public Rigidbody2D _rigidbody;
    Collider2D _strikeColl;
    Animator _animator;

    Transform _weaponTrm;

    public float levelUpDelay;
    public float levelUpDamage = 10;

    public float dashCooltime;
    public float dashElapsedTime = 0f;

    public bool isThirsty = false;
    public bool isHungry = false;
    public bool isDie = false;
    public bool isDash = false;

    Item _gasMask;

    [SerializeField]
    GameObject _trail;
    SpriteRenderer _spriteRenderer;

    

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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _strikeColl = transform.Find("StrikeColl").GetComponent<Collider2D>();

        _weaponTrm = transform.Find("Weapon");
        _moveSpeed = 5f;
    }
    private void Update()
    {
        if (isStun) return;
        WeaponRotate();
    }

    private void OnMovement(InputValue value)
    {
        if (!isStun)
        {
            _rigidbody.velocity = value.Get<Vector2>() * _moveSpeed;
            _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
        }
    }

    private void OnDash()
    {
        if ((dashElapsedTime > 0 || _rigidbody.velocity == Vector2.zero) || isStun || isDash) return;
        StartCoroutine(IEDash());
    }

    IEnumerator IEDash()
    {
        Vector2 prevDir = _rigidbody.velocity.normalized;
        _strikeColl.enabled = false;
        dashElapsedTime = 0.1f;
        isStun = true;
        isDash = true;
        _rigidbody.velocity = prevDir * _moveSpeed * 5f;
        while (dashElapsedTime > 0)
        {
            dashElapsedTime -= Time.deltaTime;
            MakeTrail();
            yield return null;
        }
        _strikeColl.enabled = true;
        isStun = false;
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _moveSpeed;
        yield return new WaitForSeconds(2f);
        isDash = false;
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

    private void MakeTrail()
    {
        GameObject trail = Instantiate(_trail, transform.position, Quaternion.identity);
        Sprite cs = _spriteRenderer.sprite;
        SpriteRenderer trailSr = trail.GetComponent<SpriteRenderer>();
        trailSr.sprite = cs;
        trailSr.color = Color.cyan * new Color(1,1,1, 0.5f);
        trailSr.DOFade(0, 1f);
        Destroy(trail, 1f);
    }

    public void FalloutDamage(float damageAmount)
    {
        if (ItemManager.Instance.FindItem(_gasMask) != null) return;
        PoisonDamage(damageAmount);
    }

    public void RecoveryHP(float amount)
    {
        _currentHp = amount * (1f -  painfulAmount / 100f);
    }

    public override void HitDamage(float damage)
    {
        base.HitDamage(damage);
        if (_currentHp <= 0) Die();
    }

    public override void PoisonDamage(float damage)
    {
        base.HitDamage(damage);
        if (_currentHp <= 0) Die();
    }

    public override void BleedDamage(float damage)
    {
        base.HitDamage(damage);  
        if (_currentHp <= 0) Die();
    }

    public override void CriticalDamage(float damage, float percent)
    {
        base.CriticalDamage(damage, percent);
        if (_currentHp <= 0) Die();
    }

    public override void Die()
    {
        if (isDie) return;
        isStun = true;
        _animator.SetTrigger("Dead");
        isDie = true;
        GameManager.Instance.GameForcedExit();
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
