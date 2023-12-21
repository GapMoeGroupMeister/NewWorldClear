using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleDroneThirdSkill : BossSkill
{
    private Sequence seq;
    [SerializeField] private GameObject attackWarnig;

    private int attackCtn = 0;
    private float attackRange = 9;

    private Vector2 attackPosition;
    private Vector2 playerPosition;
    private BattleDroneBrain battleDroneBrain;
    private SpriteRenderer skillWarningLight;

    private void Awake()
    {
        seq = DOTween.Sequence();
        battleDroneBrain = GetComponent<BattleDroneBrain>();
        skillWarningLight = transform.Find("Light").GetComponent<SpriteRenderer>();
        skillWarningLight.color = new Color(skillWarningLight.color.r, skillWarningLight.color.g, skillWarningLight.color.b, 0);
    }

    public override bool AttackDesire()
    {
        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy_TEST"));    //플레이어 레이어 추가하면 바꿔줘

        if (range != null && skillCoolTimeDown <= 0)
        {
            playerPosition = range.transform.position;
            return true;
        }

        return false;
    }

    public override void UseSkill()
    {
        StartCoroutine("Delay");
    }

    IEnumerator Delay()
    {
        battleDroneBrain.State = BattleDroneState.ATTACKING;
        seq.Append(skillWarningLight.DOFade(255, 0.2f))
            .AppendInterval(0.1f)
            .Append(skillWarningLight.DOFade(0, 0.2f))
            .Append(skillWarningLight.DOFade(255, 0.2f))
            .AppendInterval(0.1f)
            .Append(skillWarningLight.DOFade(0, 0.2f))
            .Append(skillWarningLight.DOFade(255, 0.2f))
            .AppendInterval(0.1f)
            .Append(skillWarningLight.DOFade(0, 0.2f));

        yield return new WaitForSeconds(1.5f);
        Vector3[] v = new Vector3[3];

        for (int i = 0; i < 3; i++)
        {
        a:
            Vector3 position = new Vector2(Random.Range(-attackRange, attackRange), Random.Range(-attackRange, attackRange));
            for (int j = 0; j < i; j++)
            {
                if (v[j] == position)    //이전 위치랑 위치가 같거나 너무 가까운곳을 공격하려할때
                {
                    goto a;
                }
            }
            v[i] = position;
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(attackWarnig, transform.position + v[i], Quaternion.identity);
        }
        skillCoolTimeDown = skillCoolTime;
        battleDroneBrain.State = BattleDroneState.EXIT;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
#endif
}
