using UnityEngine;

public class BattleDroneFlashBang : BossSkill
{
    [SerializeField] private GameObject flashBangObj;
    [SerializeField] private float attackRange = 7f;
    private FlashBang flashBang;

    private Vector2 playerDir;

    public override bool AttackDesire()
    {
        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.NameToLayer("Water"));    //플레이어 레이어 추가하면 바꿔줘

        if (range != null && skillCoolTimeDown <= 0)
        {
            playerDir = (range.transform.position - transform.position).normalized;
            return true;
        }

        return false;
    }

    public override void UseSkill()
    {
        Collider2D range = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.NameToLayer("Water"));    //플레이어 레이어 추가하면 바꿔줘

        if (range != null)
        {
            playerDir = (range.transform.position - transform.position).normalized;
        }

        flashBang = Instantiate(flashBangObj).GetComponent<FlashBang>();

        flashBang.gameObject.transform.position = transform.position;
        flashBang.Init(transform.position, playerDir);

        skillCoolTimeDown = skillCoolTime;
    }
}
