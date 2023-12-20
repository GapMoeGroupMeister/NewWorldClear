using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfmoonSpaer : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        if (Random.Range(0, 101) <= 30)
            enemy.GetComponent<Enemy_TEST>().CriticalDamage(PlayerController.Instance.damage, 10);
    }

    public override void Passive()
    {
    }

    public override void Skill(Transform enemy)
    {
    }

}
