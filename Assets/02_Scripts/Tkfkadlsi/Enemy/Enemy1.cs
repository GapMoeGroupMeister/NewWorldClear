using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class Enemy1 : Enemy
    {
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
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Idle")) return;
            animator.SetTrigger("Idle");
        }

        public override void On_Move()
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.position += direction.normalized * _moveSpeed * Time.deltaTime;

            if (!animator) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Move")) return;
            animator.SetTrigger("Move");
        }

        public override void On_Attack()
        {
            StartCoroutine(Attack_Delay());

            GameObject obj = Instantiate(attackObj, transform);
            obj.transform.position = transform.position;

            if (!animator) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Attack")) return;
            animator.SetTrigger("Attack");
        }

        public override void Dead()
        {
            LootManager.Instance.GenerateReward(data.Reward, transform.position, 1);
            Destroy(gameObject);
        }

    }
}
