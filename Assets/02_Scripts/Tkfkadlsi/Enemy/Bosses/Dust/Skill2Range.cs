using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Range : MonoBehaviour
{
    private Dust dust;

    private void Awake()
    {
        dust = transform.root.GetComponent<Dust>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dust.playerController.HitDamage(dust.skill2Damage);
        }
    }
}
