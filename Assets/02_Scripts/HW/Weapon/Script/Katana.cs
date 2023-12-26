using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Bleed, 3, 10);
        if(Random.Range(0, 101) <= 30)
            enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Slow, 1, 50);
    }

    public override void Passive()
    {

    }

    public override void Skill(Transform enemy)
    {

    }
}