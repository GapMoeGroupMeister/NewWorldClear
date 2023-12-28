using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        Damageable d = enemy.GetComponent<Damageable>();
        d.DeleteBuffs(Buffs.None, Debuffs.Bleed);
        d.AddDebuff(Debuffs.Bleed, 5, 10);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {
    }

}
