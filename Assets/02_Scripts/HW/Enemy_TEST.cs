using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TEST : Damageable
{
    [SerializeField]
    Transform _playerTrm;

    public float speed = 1;

    float hp = 10;
    private void Awake()
    {
        _playerTrm = FindObjectOfType<PlayerController>().transform;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerTrm.position, Time.deltaTime * speed);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void HitDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void BleedDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void PoisonDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
