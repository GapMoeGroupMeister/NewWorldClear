using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMaskHideSkill : BossSkill
{
    private Animator anim;
    private SpriteRenderer sr;

    private float invisibleTime = 3;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public override bool AttackDesire()
    { 
        if (skillCoolTimeDown > 0) return false;
        return true;
    }

    public override void UseSkill()
    {
        StartCoroutine("InvisibleRoutine");

        skillCoolTimeDown = skillCoolTime;
    }

    public void BeInvisible()
    {
        sr.DOFade(0, 1f);
    }

    public void NotInvisible()
    {
        sr.DOFade(1, 1f);
    }

    IEnumerator InvisibleRoutine()
    {
        //은신 애니메이션 실행하고
        BeInvisible();
        yield return new WaitForSeconds(invisibleTime);
        //은신 해제 애니메이션 실행하고
        NotInvisible();
    }
}
