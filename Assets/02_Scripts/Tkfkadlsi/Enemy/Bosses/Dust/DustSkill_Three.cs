using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{

    public class DustSkill_Three : MonoBehaviour
    {
        private Dust dust;
        private DustsCenter dustsCenter;
        private SpriteRenderer dustSpriteRenderer;

        private void Awake()
        {
            dust = this.GetComponent<Dust>();
            dustsCenter = this.GetComponentInParent<DustsCenter>();
            dustSpriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        private void Start()
        {

        }

        public void StartSkill_Three()
        {
            dust.currentState = Dust.DustState.Skill_3;
            StartCoroutine(SkillThree());
        }

        private IEnumerator SkillThree()
        {
            Vector3 localPos = new Vector3(0, 4);
            Quaternion localRot = Quaternion.Euler(0, 0, 0);
            yield return StartCoroutine(ColorLerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1.0f));

            if (dust.target.transform.localScale.x == 1)
            {
                transform.position = dust.target.transform.position + Vector3.left;
            }
            else if (dust.target.transform.localScale.x == -1)
            {
                transform.position = dust.target.transform.position + Vector3.right;
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return StartCoroutine(ColorLerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), 0.25f));
            Attack();
            yield return StartCoroutine(ColorLerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1.0f));
            transform.localPosition = localPos;
            transform.localRotation = localRot;
            dustsCenter.transform.position = dust.target.transform.position;
            yield return StartCoroutine(ColorLerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), 0.25f));
            Finish_SkillThree();
        }

        private void Attack()
        {

        }

        private void Finish_SkillThree()
        {
            dust.transform.localPosition = new Vector3(0, 4);
            dust.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            dust.AttackCount = 0;
            dust.currentState = Dust.DustState.Idle;
        }

        private IEnumerator ColorLerp(Color StartColor, Color EndColor, float lerpTime)
        {
            float t = 0;

            while (t < lerpTime)
            {
                dustSpriteRenderer.color = Color.Lerp(StartColor, EndColor, t / lerpTime);

                t += Time.deltaTime;
                yield return null;
            }

            dustSpriteRenderer.color = EndColor;
        }
    }
}