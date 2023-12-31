using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private string _name;
    private float _attackDelay;
    private WeaponType _weaponType;
    private AttackMotion _attackMotion;
    private Vector2 _attackRange;
    private WeaponEvent _weaponEvent;

    public GameObject _attackEffect;
    public GameObject bulletPrefab;
    private GameObject _currentWeapon;

    Transform _weaponPivot;
    WeaponSO _currentWeaponSO;
    LineRenderer _lineRenderer;
    PlayerController _playerController;

    [SerializeField]
    LayerMask _enemyMask;


    public WeaponSO testWeapon;


    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        WeaponChange(testWeapon);
    }

    private void Update()
    {
        LevelUp();
        if (Input.GetKeyDown(KeyCode.F) && testWeapon != null && !_playerController.isStun)
        {
            WeaponChange(testWeapon);
        }
    }

    public void LevelUp()
    {
        _playerController.attackDamage = _currentWeaponSO.baseDamage + _playerController.levelUpDamage;
        _playerController.attackDamage += _currentWeaponSO.baseDamage / 100 * _currentWeaponSO.damage;
        _attackDelay = _currentWeaponSO.attackDelay * _playerController.levelUpDelay;
    }

    public void WeaponChange(WeaponSO weaponSO)
    {
        if (_currentWeapon != null) Destroy(_currentWeapon);
        _currentWeaponSO = weaponSO;
        _currentWeapon = Instantiate(weaponSO.weaponPrefab, transform);
        _weaponPivot = _currentWeapon.transform.GetChild(0);
        _playerController.attackDamage = weaponSO.baseDamage + _playerController.levelUpDamage;
        _playerController.attackDamage += weaponSO.baseDamage / 100 * weaponSO.damage;
        _attackDelay = weaponSO.attackDelay * _playerController.levelUpDelay;
        _attackRange = weaponSO.attackRange;
        _attackMotion = weaponSO.attackMotion;
        _attackEffect = weaponSO.attackEffect;
        _weaponEvent = _currentWeapon.GetComponent<WeaponEvent>();

        if (_weaponType.Equals(WeaponType.Else))
        {
            StopCoroutine(ShortWeaponAttack());
        }
        else if (_weaponType.Equals(WeaponType.Gun))
        {
            StopCoroutine(LongWeaponAttack());
            _lineRenderer.enabled = false;
        }

        _weaponType = weaponSO.weaponType;

        if (_weaponType.Equals(WeaponType.Else))
        {
            StartCoroutine(ShortWeaponAttack());
        }
        else if (_weaponType.Equals(WeaponType.Gun))
        {
            StartCoroutine(LongWeaponAttack());
            _lineRenderer.enabled = true;
        }
    }
    
    public void AttackMotionPlay(Vector2 attackRange, float angle)
    {
        switch (_attackMotion)
        {
            case AttackMotion.Swing:
                if (!(_weaponPivot.localRotation.eulerAngles.z >= 50f))
                {
                    _weaponPivot.DOLocalRotate(new Vector3(0, 0, 181f), 0.2f);
                    GameObject obj = Instantiate(_attackEffect, attackRange, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
                    if(Mathf.Abs(angle) < 1.55f)
                        obj.transform.localScale = _attackRange;
                    else
                        obj.transform.localScale = new Vector3(_attackRange.x, -_attackRange.y);
                    Destroy(obj, 0.25f);
                }
                else
                {
                    _weaponPivot.DOLocalRotate(new Vector3(0, 0, 0f), 0.2f);
                    GameObject obj = Instantiate(_attackEffect, attackRange, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
                    if (Mathf.Abs(angle) >= 1.55f)
                        obj.transform.localScale = _attackRange;
                    else
                        obj.transform.localScale = new Vector3(_attackRange.x, -_attackRange.y);
                    Destroy(obj, 0.25f);
                }
                break;
            case AttackMotion.Shake:
                _weaponPivot.DOShakePosition(_attackDelay, 0.1f, 30, 90, false, false);
                break;
            case AttackMotion.Poke:
                _weaponPivot.DOLocalMoveX(2f, 0.1f).OnComplete(() => _weaponPivot.DOLocalMoveX(0, _attackDelay / 2 - 0.1f));
                break;
        }
    }

    IEnumerator ShortWeaponAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            yield return new WaitUntil(() => !_playerController.isStun);
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 attackRange = transform.position + ((Vector3)dir.normalized * (_attackRange.x / 2));
            float angle = Mathf.Atan2(dir.y, dir.x);
            AttackMotionPlay(attackRange, angle);
            Collider2D[] enemies = Physics2D.OverlapBoxAll(attackRange, _attackRange, angle * Mathf.Rad2Deg, _enemyMask);
            if (enemies.Length > 0)
            {
                if(enemies.Length >= 3)
                {
                    CameraManager.Instance.Shake(4, 1, 0.15f);
                    TimeController.Instance.SetTimeFreeze(0.2f, 0, 0.1f);
                }
                else if(enemies.Length >= 6)
                {
                    CameraManager.Instance.Shake(4, 1, 0.2f);
                    TimeController.Instance.SetTimeFreeze(0.1f, 0, 0.15f);
                }
                foreach (Collider2D col in enemies)
                {
                    col.GetComponent<Damageable>().HitDamage(_playerController.attackDamage);
                    if (_weaponEvent != null)
                    {
                        _weaponEvent.OnHit(col.transform);
                    }
                }
            }
        }
    }

    IEnumerator LongWeaponAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            yield return new WaitUntil(() => !_playerController.isStun);
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,angle)));
            bullet.GetComponent<Bullet>().dir = dir;
        }
    }
}
