using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class BossSkill : MonoBehaviour
{
    [SerializeField] protected float skillCoolTime = 60f;
    protected float skillCoolTimeDown = 0f;

    public bool isAttacking { get; protected set; }

    public abstract void UseSkill();

    public abstract bool AttackDesire();

    private void Start()
    {
        isAttacking = false;
    }

    protected virtual void Update()
    {
        if (skillCoolTimeDown > 0f)
        {
            skillCoolTimeDown -= Time.deltaTime;
        }
    }
}
