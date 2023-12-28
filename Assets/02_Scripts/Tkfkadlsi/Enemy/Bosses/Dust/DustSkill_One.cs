using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{

    public class DustSkill_One : MonoBehaviour
    {
        [SerializeField] private float skill_One_Damage;
        [SerializeField] private AnimationCurve skill_One_Curve;
        private TrailRenderer trailRenderer;
        private Dust dust;
        private DustsCenter dustsCenter;
        private CircleCollider2D circleCollider;

        private void Awake()
        {
            trailRenderer = this.GetComponent<TrailRenderer>();
            dust = this.GetComponent<Dust>();
            dustsCenter = this.GetComponentInParent<DustsCenter>();
            circleCollider = this.GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            trailRenderer.enabled = false;
        }

        public void StartSkill_One()
        {
            dust.AttackCount = 0;
            dust.currentState = Dust.DustState.Skill_1;
            circleCollider.radius = 0.25f;
            StartCoroutine(SkillOne());
        }

        private IEnumerator SkillOne()
        {
            yield return new WaitForSeconds(1.0f);
            dust.animator.SetTrigger("Skill1");
            yield return new WaitForSeconds(0.375f);
            Vector3 dir = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                dir = dust.target.transform.position - dust.transform.position;
                dir = dir.normalized;
                yield return new WaitForSeconds(0.125f);
                trailRenderer.enabled = true;
                circleCollider.isTrigger = true;
                dustsCenter.transform.position = dust.target.transform.position;
                yield return StartCoroutine(MoveLerp(dust.transform, dir * 8, 0.5f));
                trailRenderer.enabled = false;
                circleCollider.isTrigger = false;
                dustsCenter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, dustsCenter.z));
                dust.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            FinishSkill_One();
        }

        private void FinishSkill_One()
        {
            dust.transform.localPosition = new Vector3(0, 4);
            dust.animator.SetTrigger("Move");
            dust.currentState = Dust.DustState.Idle;
            circleCollider.radius = 0.5f;
            dust.SkillOneCount++;
        }

        private IEnumerator MoveLerp(Transform trm, Vector3 dir, float moveTime)
        {
            float t = 0;
            Vector3 startpos = trm.position;
            Vector3 endpos = startpos + dir;

            while (t < moveTime)
            {
                trm.position = Vector3.Lerp(startpos, endpos, skill_One_Curve.Evaluate(t / moveTime));

                t += Time.deltaTime;
                yield return null;
            }

            trm.position = endpos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && dust.currentState == Dust.DustState.Skill_1)
            {
                dust.target.HitDamage(skill_One_Damage);
            }
        }
    }
}