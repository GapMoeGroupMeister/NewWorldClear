using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class Enemy1 : Enemy
    {
        [SerializeField] private bool spriteIsReverse;

        private float left;
        private float right;

        private void OnEnable()
        {
            FindTarget();
            SetStat();
            animator = GetComponent<Animator>();

            if (spriteIsReverse)
            {
                left = 1;
                right = -1;
            }
            else
            {
                left = -1;
                right = 1;
            }
        }

        private void Update()
        {
            SetState();
            RunState();
        }

        public override void On_Idle()
        {
            if (!animator) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"{EnemyName}Idle")) return;
            animator.SetTrigger("Idle");
        }

        public override void On_Move()
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.position += direction.normalized * _moveSpeed * Time.deltaTime;

            if(direction.x < 0)
            {
                transform.localScale = new Vector3(left, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(right, 1, 1);
            }

            if (!animator) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"{EnemyName}Move")) return;
            animator.SetTrigger("Move");
        }

        public override void On_Attack()
        {
            StartCoroutine(Attack_Delay());

            GameObject obj = Instantiate(attackObj, transform);
            obj.transform.position = transform.position;

            if (!animator) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"{EnemyName}Attack")) return;
            animator.SetTrigger("Attack");
        }

        public override void Dead()
        {
            LootManager.Instance.GenerateReward(data.Reward, transform.position, 1);
            Destroy(gameObject);
        }

    }
}
