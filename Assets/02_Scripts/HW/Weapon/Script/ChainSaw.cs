using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Bleed, 5, 50);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {
    }

}
