using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfmoonSpaer : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        Damageable d = enemy.GetComponent<Damageable>();
        d.DeleteBuffs(Buffs.None, Debuffs.Bleed);
        d.AddDebuff(Debuffs.Bleed, 3, 10);
        d.AddDebuff(Debuffs.Slow, 3, 5);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {
    }

}
