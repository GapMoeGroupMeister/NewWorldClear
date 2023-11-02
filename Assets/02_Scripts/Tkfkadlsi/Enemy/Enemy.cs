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

        private State currentState;
        private FSM fsm;
        private GameObject target;

        private bool CanAttackPlayer = false;

        private void Start()
        {
            currentState = State.Idle;
            fsm = new FSM(new IdleState(this));
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
            switch (nextState)
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
    }

    public class IdleState : StateBase
    {
        public IdleState(Enemy initenemy) : base(initenemy){ }

        public override void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }

    public class MoveState : StateBase
    {
        public MoveState(Enemy initenemy) : base(initenemy) { }

        public override void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }

    public class AttackState : StateBase
    {
        public AttackState(Enemy initenemy) : base(initenemy) { }

        public override void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
