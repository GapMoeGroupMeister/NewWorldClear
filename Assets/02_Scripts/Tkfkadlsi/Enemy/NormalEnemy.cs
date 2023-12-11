using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class NormalEnemy : Enemy
    {
        public override void Awake()
        {
            base.Awake();
        }

        public override void Update()
        {
            ChoiceState();
        }

        public override void Dead()
        {
            Destroy(gameObject);
        }

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp < 0)
                Dead();
        }

        public override void SetState()
        {
            idleState = new NormalEnemyIdleState(this);
            moveState = new NormalEnemyMoveState(this);
            attackState = new NormalEnemyAttackState(this);
        }

        public class NormalEnemyIdleState : StateBase
        {
            public NormalEnemyIdleState(Enemy initenemy) : base(initenemy) { }

            public override void OnStateEnter()
            {
                
            }

            public override void OnStateExit()
            {
                
            }

            public override void OnStateUpdate()
            {
                
            }
        }

        public class NormalEnemyMoveState : StateBase
        {
            public NormalEnemyMoveState(Enemy initenemy) : base(initenemy) { }

            public override void OnStateEnter()
            {
                
            }

            public override void OnStateExit()
            {
                
            }

            public override void OnStateUpdate()
            {
                Vector3 moveDir = Vector3.zero;
                moveDir = enemy.target.transform.position - enemy.transform.position;
                moveDir = moveDir.normalized;
                enemy.transform.position += moveDir * enemy.spd * Time.deltaTime;
            }
        }

        public class NormalEnemyAttackState : StateBase
        {
            public NormalEnemyAttackState(Enemy initenemy) : base(initenemy) { }

            private float cycle;

            public override void OnStateEnter()
            {
                cycle = enemy.atkCycle;
            }

            public override void OnStateExit()
            {
                
            }

            public override void OnStateUpdate()
            {
                cycle -= Time.deltaTime;



                if (cycle < 0)
                {
                    PlayerController playerController = enemy.target.GetComponent<PlayerController>();
                    playerController.HitDamage(enemy.atk);
                    cycle = enemy.atkCycle;
                }
            }
        }
    }
}