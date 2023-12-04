using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDronePoisonPool : BossSkill
{
    public override bool AttackDesire()
    {
        return true;
    }

    public override void UseSkill()
    {
        Debug.Log("µ¶!");
        skillCoolTimeDown = skillCoolTime;
    }
}
