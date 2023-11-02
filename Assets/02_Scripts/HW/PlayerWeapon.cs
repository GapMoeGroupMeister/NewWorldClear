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

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void WeaponChange(WeaponSO weaponSO)
    {
        _name = weaponSO.name;
        _desc = weaponSO.desc;
        _weaponSprite = weaponSO.weaponSprite;
        _damage = weaponSO.damage;
        _attackDelay = weaponSO.attackDelay;
        _weaponType = weaponSO.weaponType;
        _attackRange = weaponSO.attackRange;

        _spriteRenderer.sprite = _weaponSprite;
    }

    private void Attack()
    {

    }
}
