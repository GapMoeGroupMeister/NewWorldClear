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
        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy_TEST"));    //�÷��̾� ���̾� �߰��ϸ� �ٲ���

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
        //���� �����ϴ� �ִϸ��̼� �����Ű��


    }
}
