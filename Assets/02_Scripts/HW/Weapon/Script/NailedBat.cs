using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailedBat : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        enemy.GetComponent<Damageable>().AddDebuff(Debuffs.Bleed, 0.5f);
    }

    public override void Passive()
    {

    }

    public override void Skill(Transform enemy)
    {

    }
}