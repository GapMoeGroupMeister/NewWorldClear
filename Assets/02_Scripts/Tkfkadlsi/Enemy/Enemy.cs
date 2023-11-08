using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class Enemy : MonoBehaviour
    {
        private enum State
        {
            Idle,
            Move,
            Attack
        }

        public EnemyData data;
        public Animator anim;
        public GameObject target;

        private State currentState;
        private FSM fsm;

        private float hp;
        private float atk;
        private float def;
        private float spd;
        private float range;

        private bool CanAttackPlayer = false;

        private void Start()
        {
            target = FindObjectOfType<EnemySpawner>().gameObject;
            anim = this.GetComponent<Animator>();
            currentState = State.Idle;
            fsm = new FSM(new IdleState(this));

            SetStatus();
        }

        private void SetStatus()
        {
            hp = data.DefaultHP;
            atk = data.DefaultATK;
            def = data.DefaultDEF;
            spd = data.DefaultSPD;
            range = data.DetectRange;
        }

        private void Update()
        {
            switch (currentState)
            {
                case State.Idle:
                    if (CanSeePlayer())
                    {
                        if (CanAttackPlayer)
                            ChangeState(State.Attack);
                        else
                            ChangeState(State.Move);
                    }
                    break;
                case State.Move:
                    if (CanSeePlayer())
                    {
                        if (CanAttackPlayer)
                            ChangeState(State.Attack);
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;
                case State.Attack:
                    if (CanSeePlayer())
                    {
                        if (!CanAttackPlayer)
                            ChangeState(State.Move);
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;
            }

            fsm.UpdateState();
        }

        private void ChangeState(State nextState)
        {
            currentState = nextState;
            switch (currentState)
            {
                case State.Idle:
                    fsm.ChangeState(new IdleState(this));
                    break;
                case State.Move:
                    fsm.ChangeState(new MoveState(this));
                    break;
                case State.Attack:
                    fsm.ChangeState(new AttackState(this));
                    break;
            }
        }

        private bool CanSeePlayer()
        {
            if(Vector2.Distance(target.transform.position, this.transform.position) <= data.DetectRange)
            {
                return true;
            }
            return false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.gameObject == target)
            {
                CanAttackPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.collider.gameObject == target)
            {
                CanAttackPlayer = false;
            }
        }

        public void Hit(float damage)
        {
            hp -= damage;


            if (hp < 0) Dead();
        }

        private void Dead()
        {
            Destroy(gameObject);
        }
    }
}
