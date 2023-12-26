using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tkfkadlsi;

public class AlphaMaskStatus : Enemy
{
    [SerializeField] private float maxHp;
    [SerializeField] private float attack;
    private float curHp;

    private void Start()
    {
        curHp = maxHp;
    }


    public override void Hit(float damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            Dead();
        }
    }
    public override void Dead()
    {
        gameObject.SetActive(false);
    }
    public override void SetState()
    {

    }

    public override void Update()
    {

    }
}
