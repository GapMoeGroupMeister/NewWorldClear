using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMaskSecondSkill : BossSkill
{
    private Vector2 playerDir;
    private float attackRange;
    [SerializeField] private GameObject attackWarning;

    public override bool AttackDesire()
    {
        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy_TEST"));    //플레이어 레이어 추가하면 바꿔줘

        if (range != null && skillCoolTimeDown <= 0)
        {
            playerDir = (range.transform.position - transform.position).normalized;
            return true;
        }

        return false;
    }

    public override void UseSkill()
    {
        Instantiate(attackWarning,transform.position, Quaternion.identity);
    }

    public void Attack()
    {
        //대충 공격하는 애니메이션 실행시키고


    }
}
