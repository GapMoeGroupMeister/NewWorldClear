using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class TestEnemy : Enemy
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
            Reward();
            Destroy(gameObject);
        }

        public override void Hit(float damage)
        {
            _currentHp -= damage;

            if (_currentHp < 0)
                Dead();
        }

        public override void SetState()
        {
            idleState = new TestEnemtIdleState(this);
            moveState = new TestEnemyMoveState(this);
            attackState = new TestEnemyAttackState(this);

        }

        public class TestEnemtIdleState : StateBase
        {
            public TestEnemtIdleState(Enemy initenemy) : base(initenemy) { }

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

        public class TestEnemyMoveState : StateBase
        {
            public TestEnemyMoveState(Enemy initenemy) : base(initenemy) { }

            public override void OnStateEnter()
            {

            }

            public override void OnStateExit()
            {

            }

            public override void OnStateUpdate()
            {
                Vector3 direction = new Vector3();

                direction = enemy.target.transform.position - enemy.transform.position;
                enemy.transform.position += direction * enemy._moveSpeed * Time.deltaTime;
            }
        }

        public class TestEnemyAttackState : StateBase
        {
            public TestEnemyAttackState(Enemy initenemy) : base(initenemy) { }

            private float cycle;

            public override void OnStateEnter()
            {
                cycle = enemy.atkDelay;
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
                    playerController.HitDamage(enemy.damage);
                    cycle = enemy.atkDelay;
                }
            }
        }
    }
}