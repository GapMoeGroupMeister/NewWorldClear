using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AluminiumBat : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        if (Random.Range(1, 101) <= 5)
            enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Stun, 1);
    }

    public override void Passive()
    {

    }

    public override void Skill(Transform enemy)
    {

    }
}