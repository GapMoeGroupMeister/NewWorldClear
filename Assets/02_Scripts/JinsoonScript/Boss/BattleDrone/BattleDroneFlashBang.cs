using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BattleDroneFlashBang : BossSkill
{
    private Sequence seq;
    private BattleDroneBrain battleDroneBrain;

    [SerializeField] private GameObject flashBangObj;
    [SerializeField] private float attackRange = 7f;
    private FlashBang flashBang;
    private SpriteRenderer skillWarningLight;

    private Vector2 playerDir;

    private void Awake()
    {
        battleDroneBrain = GetComponent<BattleDroneBrain>();
        skillWarningLight = transform.Find("Light").GetComponent<SpriteRenderer>();
        skillWarningLight.color = new Color(skillWarningLight.color.r, skillWarningLight.color.g, skillWarningLight.color.b, 0);
        seq = DOTween.Sequence();

    }

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
        StartCoroutine("FlashDelay");
    }

    IEnumerator FlashDelay()
    {
        seq.Append(skillWarningLight.DOFade(255, 0.2f))
            .AppendInterval(0.1f)
            .Append(skillWarningLight.DOFade(0, 0.2f));

        battleDroneBrain.State = BattleDroneState.ATTACKING;
        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy_TEST"));    //플레이어 레이어 추가하면 바꿔줘

        if (range != null)
        {
            playerDir = (transform.position - range.transform.position).normalized;
        }
        yield return new WaitForSeconds(0.5f);
        //풀링하면 여기서 불러오는걸로
        //PoolManager.Instance.
        flashBang = Instantiate(flashBangObj).GetComponent<FlashBang>();

        flashBang.gameObject.transform.position = transform.position;
        flashBang.Init(transform.position, playerDir);

        skillCoolTimeDown = skillCoolTime;
        battleDroneBrain.State = BattleDroneState.EXIT;
        isAttacking = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
