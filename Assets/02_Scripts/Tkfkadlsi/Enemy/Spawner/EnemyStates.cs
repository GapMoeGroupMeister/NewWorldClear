using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class IdleState : StateBase
    {
        public IdleState(Enemy initenemy) : base(initenemy) { }

        public override void OnStateEnter()
        {
            enemy.anim.SetTrigger("Idle");
        }

        public override void OnStateExit()
        {
            
        }

        public override void OnStateUpdate()
        {
            
        }
    }

    public class MoveState : StateBase
    {
        public MoveState(Enemy initenemy) : base(initenemy) { }

        public override void OnStateEnter()
        {
            enemy.anim.SetTrigger("Move");
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

    public class AttackState : StateBase
    {
        public AttackState(Enemy initenemy) : base(initenemy) { }

        private float cycle = 0f;

        public override void OnStateEnter()
        {
            enemy.anim.SetTrigger("Attack");
            cycle = 0f;
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
                playerController.Hit(enemy.atk);
                cycle = enemy.atkCycle;
            }
        }
    }
}