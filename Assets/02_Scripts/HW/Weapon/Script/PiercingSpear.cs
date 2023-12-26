using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingSpear : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Slow, 3, 10);
        enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Bleed, 7, 20);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {

    }

}
