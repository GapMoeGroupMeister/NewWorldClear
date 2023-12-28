using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tkfkadlsi
{
    public class DustSkill_Two_Attack : MonoBehaviour
    {
        private Dust dust;
        private DustSkill_Two dustSkill_Two;
        private CircleCollider2D circleCollider2D;

        private void Awake()
        {
            dust = this.GetComponentInParent<Dust>();
            dustSkill_Two = this.GetComponentInParent<DustSkill_Two>();
            circleCollider2D = this.GetComponent<CircleCollider2D>();
            StartCoroutine(Size());
        }

        private IEnumerator Size()
        {
            float t = 0f;
            float sizeUpTime = 1.125f;

            while(t < sizeUpTime)
            {
                circleCollider2D.radius = Mathf.Lerp(0.1f, 8.0f, t/sizeUpTime);

                t += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                dust.target.HitDamage(dustSkill_Two.skill2Damage);
            }
        }
    }
}
