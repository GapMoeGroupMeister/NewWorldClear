using System.Collections;
using System.Collections.Generic;
using Tkfkadlsi;
using UnityEngine;


public class BattleDroneStatus : Damageable
{
    [SerializeField] private BossStatusSO bossStatusSO;

    private void Awake()
    {
        _maxHp = bossStatusSO.bossHp;
        _moveSpeed = bossStatusSO.bossSpeed;
        damage = bossStatusSO.bossDamage;
    }
}
