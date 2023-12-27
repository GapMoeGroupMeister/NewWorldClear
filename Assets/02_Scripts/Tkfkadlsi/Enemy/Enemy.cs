using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tkfkadlsi;

public abstract class Enemy : Damageable
{
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Hit
    }

    public EnemyData data;
    public GameObject target;

    [SerializeField] protected GameObject attackObj;
    protected Animator animator;
    protected EnemyState currentState;
    protected bool canSeePlayer = false;
    protected bool canAttackPlayer = false;
    protected bool isAttacking = false;

    private void OnEnable()
    {
        SetStat();
    }

    public void SetStat()
    {
        _maxHp = data.DefaultHP;
        _currentHp = data.DefaultHP;
        damage = data.DefaultATK;
        _moveSpeed = data.DefaultSPD;
    }

    public void FindTarget()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    public void SetState()
    {
        if(target == null)
        {
            currentState = EnemyState.Idle;
            return;
        }

        if (isAttacking) return;
        if (currentState == EnemyState.Hit) return;

        if (CanAttackPlayer())
            currentState = EnemyState.Attack;
        else if (CanSeePlayer())
            currentState = EnemyState.Move;
        else
            currentState = EnemyState.Idle;
    }

    public void RunState()
    {
        if (isAttacking) return;
        if (currentState == EnemyState.Hit) return;

        switch (currentState)
        {
            case EnemyState.Idle:
                On_Idle();
                break;
            case EnemyState.Move:
                On_Move();
                break;
            case EnemyState.Attack:
                On_Attack();
                break;
        }
    }

    public bool CanSeePlayer()
    {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance <= data.DetectRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanAttackPlayer()
    {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance <= data.AttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract void On_Idle();
    public abstract void On_Move();
    public abstract void On_Attack();

    public IEnumerator Attack_Delay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(data.AttackCycle);
        isAttacking = false;
    }

    public IEnumerator Hit_Delay()
    {
        isAttacking = false;
        currentState = EnemyState.Hit;
        yield return new WaitForSeconds(0.5f);
        currentState = EnemyState.Idle;
    }

    public override void HitDamage(float damage)
    {
        StartCoroutine(Hit_Delay());

        base.HitDamage(damage);

        if (!animator) return;
        animator.SetTrigger("Hit");
        if (_currentHp <= 0) Die();
    }

    public override void BleedDamage(float damage)
    {
        base.BleedDamage(damage);
        if (_currentHp <= 0) Die();
    }

    public override void CriticalDamage(float damage, float percent)
    {
        base.CriticalDamage(damage, percent);
        if (_currentHp <= 0) Die();
    }

    public abstract void Dead();

    public override void Die()
    {
        LootManager.Instance.GenerateReward(data.Reward, transform.position, 2);
        PoolManager.Release(gameObject);
    }
}
