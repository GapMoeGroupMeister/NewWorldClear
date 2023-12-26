using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class Enemy1 : Enemy
    {
        Animator animator;

        private void Awake()
        {
            FindTarget();
            SetStat();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetState();
            RunState();
        }

        public override void On_Idle()
        {
            if (!animator) return;
            animator.SetTrigger("Idle");
        }

        public override void On_Move()
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.position += direction.normalized * _moveSpeed * Time.deltaTime;

            if (!animator) return;
            animator.SetTrigger("Move");
        }

        public override void On_Attack()
        {
            if (isAttacking) return;
            StartCoroutine(Attack_Delay());

            GameObject obj = Instantiate(attackObj, transform);
            obj.transform.position = transform.position;

            if (!animator) return;
            animator.SetTrigger("Attack");
        }

        public override void Hit(float damage)
        {
            _currentHp -= damage;

            if (_currentHp <= 0)
                Dead();
        }

        public override void Dead()
        {
            LootManager.Instance.GenerateReward(data.Reward, transform.position, 1);
            Destroy(gameObject);
        }
    }
}
