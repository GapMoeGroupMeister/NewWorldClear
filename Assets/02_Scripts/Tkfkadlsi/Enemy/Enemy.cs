using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public abstract class Enemy : Damageable
    {
        public EnemyData data;
        public Animator anim;
        public GameObject target;

        public State currentState;
        public FSM fsm;


        public float def;
        public float atkDelay;
        public float range;

        public bool CanAttackPlayer = false;

        public StateBase idleState;
        public StateBase moveState;
        public StateBase attackState;

        public virtual void Awake()
        {
            target = FindObjectOfType<PlayerController>().gameObject;
            currentState = State.Idle;
            fsm = new FSM(idleState);

            SetStatus();
        }

        private void SetStatus()
        {
            _maxHp = data.DefaultHP;
            _currentHp = data.DefaultHP;
            damage = data.DefaultATK;
            def = data.DefaultDEF;
            _moveSpeed = data.DefaultSPD;
            atkDelay = data.AttackCycle;
            range = data.DetectRange;
        }

        public abstract void SetState();

        public abstract void Update();

        public virtual void ChoiceState()
        {
            switch (currentState)
            {
                case State.Idle:
                    if (CanSeePlayer())
                    {
                        if (CanAttackPlayer)
                            ChangeState(State.Attack, attackState);
                        else
                            ChangeState(State.Move, moveState);
                    }
                    break;
                case State.Move:
                    if (CanSeePlayer())
                    {
                        if (CanAttackPlayer)
                            ChangeState(State.Attack, attackState);
                    }
                    else
                    {
                        ChangeState(State.Idle, idleState);
                    }
                    break;
                case State.Attack:
                    if (CanSeePlayer())
                    {
                        if (!CanAttackPlayer)
                            ChangeState(State.Move, moveState);
                    }
                    else
                    {
                        ChangeState(State.Idle, idleState);
                    }
                    break;
            }
            fsm.UpdateState();
        }

        private void ChangeState(State nextState, StateBase state)
        {
            currentState = nextState;
            switch (currentState)
            {
                case State.Idle:
                    fsm.ChangeState(state);
                    break;
                case State.Move:
                    fsm.ChangeState(state);
                    break;
                case State.Attack:
                    fsm.ChangeState(state);
                    break;
            }
        }

        protected bool CanSeePlayer()
        {
            if (Vector2.Distance(target.transform.position, this.transform.position) <= data.DetectRange)
            {
                return true;
            }
            return false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                CanAttackPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                CanAttackPlayer = false;
            }
        }

        public abstract void Hit(float damage);

        public abstract void Dead();

        public void Reward()
        {
            LootManager.Instance.GenerateReward(data.Reward, transform.position, 1);
        }
    }
}