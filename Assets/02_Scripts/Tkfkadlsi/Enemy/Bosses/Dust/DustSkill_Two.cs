using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class DustSkill_Two : MonoBehaviour
    {
        [SerializeField] private float skill2ChargeTime;
        [SerializeField] private GameObject skill2AttackRangeObject;
        [SerializeField] private GameObject skill2WarningObject;

        private Dust dust;

        private SpriteRenderer skill2WarningSpriteRenderer;

        private void Awake()
        {
            skill2WarningObject.SetActive(false);
            skill2AttackRangeObject.SetActive(false);

            dust = this.GetComponent<Dust>();
            skill2WarningSpriteRenderer = skill2WarningObject.GetComponent<SpriteRenderer>();
        }

        public void StartSkill_Two()
        {
            dust.SkillOneCount = 0;
            dust.currentState = Dust.DustState.Skill_2;
            StartCoroutine(SkillTwo());
        }

        private IEnumerator SkillTwo()
        {
            yield return StartCoroutine(ColorLerp(Color.clear, Color.red));
            skill2AttackRangeObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            skill2AttackRangeObject.SetActive(false);
            FinishSKillTwo();
        }
        
        private IEnumerator ColorLerp(Color startColor, Color endColor)
        {
            float t = 0;

            skill2WarningObject.SetActive(true);
            while(t < skill2ChargeTime)
            {
                skill2WarningSpriteRenderer.color = Color.Lerp(startColor, endColor, t / skill2ChargeTime);

                t += Time.deltaTime;
                yield return null;
            }

            skill2WarningSpriteRenderer.color = endColor;
            skill2WarningObject.SetActive(false);
        }

        private void FinishSKillTwo()
        {
            dust.currentState = Dust.DustState.Idle;
        }
    }
}
