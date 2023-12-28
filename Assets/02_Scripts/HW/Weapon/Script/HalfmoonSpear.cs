using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfmoonSpaer : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        enemy.GetComponent<Damageable>().AddDebuff(Debuffs.Slow, 3, 5);
        enemy.GetComponent<Damageable>().AddDebuff(Debuffs.Bleed, 3, 10);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {
    }

}
