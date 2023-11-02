using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField]
    float _moveSpeed = 5f;

    public WeaponSO weaponSO;

    Transform _weaponTrm;
    Transform _weaponVisualTrm;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weaponTrm = transform.Find("Weapon");
        _weaponVisualTrm = _weaponTrm.Find("Visual");
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * _moveSpeed;
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _weaponTrm.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        if(Mathf.Abs(_weaponTrm.rotation.z) >= 0.5f)
        {
            _weaponVisualTrm.localScale = new Vector2(-0.2f, -1f);
        }
        else
        {
            _weaponVisualTrm.localScale = new Vector2(0.2f, 1f);
        }
    }
}
