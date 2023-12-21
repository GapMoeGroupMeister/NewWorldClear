using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine.Utility;

public class BattleDronePoisonPool : BossSkill
{
    private Sequence seq;

    [SerializeField] private GameObject pfPosisionPool;
    [SerializeField] private float attackRange = 8;

    private Vector2 playerPos;

    private SpriteRenderer skillWarningLight;
    private BattleDroneBrain battleDroneBrain;
    private PoisionPool poisionPool;

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
            playerPos = range.transform.position;
            return true;
        }

        return false;
    }

    public override void UseSkill()
    {
        StartCoroutine("ThrowPoisionRoutine");
        skillCoolTimeDown = skillCoolTime;
    }

    IEnumerator ThrowPoisionRoutine()
    {
        battleDroneBrain.State = BattleDroneState.ATTACKING;
        seq.Append(skillWarningLight.DOFade(255, 0.4f))
            .AppendInterval(0.1f)
            .Append(skillWarningLight.DOFade(0, 0.4f));
        yield return new WaitForSeconds(1);

        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy_TEST"));    //플레이어 레이어 추가하면 바꿔줘

        if (range != null && skillCoolTimeDown <= 0)
        {
            playerPos = range.transform.position;
        }

        poisionPool = Instantiate(pfPosisionPool).GetComponent<PoisionPool>();

        poisionPool.transform.position = transform.position;
        poisionPool.transform.DOJump(playerPos, 5, 1, 1.5f).SetEase(Ease.Linear)
            .OnComplete(() => poisionPool.FieldPoision());

        battleDroneBrain.State = BattleDroneState.EXIT;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
#endif
}
