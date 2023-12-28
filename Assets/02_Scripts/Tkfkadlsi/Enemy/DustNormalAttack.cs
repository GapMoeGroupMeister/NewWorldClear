using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class DustNormalAttack : MonoBehaviour
    {
        PolygonCollider2D polygonCollider;
        Dust dust;
        IEnumerator enumerator;

        private void Awake()
        {
            dust = GetComponentInParent<Dust>();
            polygonCollider = this.GetComponent<PolygonCollider2D>();
            enumerator = LifeTime();
            StartCoroutine(enumerator);
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(0.875f);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                polygonCollider.enabled = false;
                dust.target.HitDamage(dust.attackDamage);
                dust.target.DeleteBuffs(Buffs.None, Debuffs.Bleed);
                dust.target.AddDebuff(Debuffs.Bleed, 3, 3);
            }
        }
    }
}
