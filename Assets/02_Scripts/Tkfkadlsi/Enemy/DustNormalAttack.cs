using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class DustNormalAttack : MonoBehaviour
    {
        Dust dust;

        private void Awake()
        {
            dust = GetComponentInParent<Dust>();
            StartCoroutine(LifeTime());
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
                dust.target.HitDamage(dust.attackDamage);
            }
        }
    }
}
