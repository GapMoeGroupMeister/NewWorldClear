using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingSpear : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        enemy.GetComponent<Enemy_TEST>().AddDebuff(Debuffs.Bleed, 0.5f);
        if (Random.Range(0, 101) <= 50)
            enemy.GetComponent<Enemy_TEST>().CriticalDamage(PlayerController.Instance.damage, 10);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {
    }

}
