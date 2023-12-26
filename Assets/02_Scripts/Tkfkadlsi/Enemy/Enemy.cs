using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public abstract class Enemy : Damageable
    {
        public enum EnemyState
        {
            Idle,
            Move,
            Attack
        }

        public EnemyData data;
        public GameObject target;

        [SerializeField] protected GameObject attackObj;
        protected EnemyState currentState;
        protected bool canSeePlayer = false;
        protected bool canAttackPlayer = false;
        protected bool isAttacking = false;

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
            if (CanAttackPlayer())
                currentState = EnemyState.Attack;
            else if (CanSeePlayer())
                currentState = EnemyState.Move;
            else
                currentState = EnemyState.Idle;
        }

        public void RunState()
        {
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
        public abstract void Hit(float damage);
        public abstract void Dead();
    }
}
