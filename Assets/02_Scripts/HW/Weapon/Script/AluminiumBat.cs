using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AluminiumBat : WeaponEvent
{
    public override void OnHit(Transform enemy)
    {
        if(Random.Range(1, 101) <= 5)
            StartCoroutine(IEOnHit(enemy));
    }

    IEnumerator IEOnHit(Transform enemy)
    {
        Enemy_TEST enemyTest = enemy.GetComponent<Enemy_TEST>();
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float beforeSpeed = enemyTest.speed;
        enemyTest.speed = 0;
        yield return new WaitForSeconds(1);
        enemyTest.speed = beforeSpeed;
    }

    public override void Passive()
    {

    }

    public override void Skill(Transform enemy)
    {

    }
}