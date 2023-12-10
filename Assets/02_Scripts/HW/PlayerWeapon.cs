using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private string _name;
    private float _attackDelay;
    private WeaponType _weaponType;
    private AttackMotion _attackMotion;
    private Vector2 _attackRange;
    private WeaponEvent _weaponEvent;

    public GameObject effect;

    Transform _nearestEnemy;
    Transform _weaponPivot;

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
        if (Input.GetKeyDown(KeyCode.F) && testWeapon != null && !_playerController.isStun)
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
        GameObject weapon = Instantiate(weaponSO.weaponPrefab, transform);
        _weaponPivot = weapon.transform.GetChild(0);
        _playerController.damage = weaponSO.damage;
        _attackDelay = weaponSO.attackDelay;
        _attackRange = weaponSO.attackRange;
        _attackMotion = weaponSO.attackMotion;
        _weaponEvent = (WeaponEvent)WeaponEventManager.Instance.GetComponent(weapon.name);

        if (_weaponType.Equals(WeaponType.ShortRange))
        {
            StopCoroutine("ShortWeaponAttack");
        }
        else if (_weaponType.Equals(WeaponType.LongRange))
        {
            StopCoroutine("LongWeaponAttack");
            _lineRenderer.enabled = false;
        }

        _weaponType = weaponSO.weaponType;

        if (_weaponType.Equals(WeaponType.ShortRange))
        {
            StartCoroutine("ShortWeaponAttack", _attackDelay);
        }
        else if (_weaponType.Equals(WeaponType.LongRange))
        {
            StartCoroutine("LongWeaponAttack", _attackDelay);
            _lineRenderer.enabled = true;
        }
    }

    public void AttackMotionPlay()
    {
        switch (_attackMotion)
        {
            case AttackMotion.Swing:
                if (!(_weaponPivot.localRotation.eulerAngles.z >= 50f))
                    _weaponPivot.DOLocalRotate(new Vector3(0, 0, 181f), 0.2f);
                else
                    _weaponPivot.DOLocalRotate(new Vector3(0, 0, 0f), 0.2f);
                break;
            case AttackMotion.Shake:
                _weaponPivot.DOShakePosition(_attackDelay, 0.1f, 30, 90, false, false);
                break;
            case AttackMotion.Poke:
                _weaponPivot.DOLocalMoveX(2f, 0.1f).OnComplete(() => _weaponPivot.DOLocalMoveX(0, _attackDelay / 2 - 0.1f));
                break;
        }
    }

    IEnumerator ShortWeaponAttack(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            yield return new WaitUntil(() => !_playerController.isStun);
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 attackRange = transform.position + ((Vector3)dir.normalized * (_attackRange.x / 2));
            float angle = Mathf.Atan2(dir.y, dir.x);
            AttackMotionPlay();
            Collider2D[] enemies = Physics2D.OverlapBoxAll(attackRange, _attackRange, angle * Mathf.Rad2Deg, _enemyMask);
            //GameObject obj = Instantiate(effect, attackRange, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
            //obj.transform.localScale = _attackRange;
            //Destroy(obj, 0.5f);
            if (enemies.Length > 0)
            {
                foreach (Collider2D col in enemies)
                {
                    col.GetComponent<Enemy_TEST>().Damage(_playerController.damage);
                    if (_weaponEvent != null)
                    {
                        _weaponEvent.OnHit(col.transform);
                    }
                }
            }
        }
    }

    IEnumerator LongWeaponAttack(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            yield return new WaitUntil(() => _playerController.isStun);
            if (_nearestEnemy != null)
                _nearestEnemy.GetComponent<Enemy_TEST>().Damage(_playerController.damage);
        }
    }
}
