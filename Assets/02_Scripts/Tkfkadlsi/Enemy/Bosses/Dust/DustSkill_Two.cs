using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class DustSkill_Two : MonoBehaviour
    {
        public float skill2Damage;
        [SerializeField] private GameObject skill2AttackObject;

        private Dust dust;

        private SpriteRenderer skill2WarningSpriteRenderer;

        private void Awake()
        {

            dust = this.GetComponent<Dust>();
        }

        public void StartSkill_Two()
        {
            dust.SkillOneCount = 0;
            dust.currentState = Dust.DustState.Skill_2;
            StartCoroutine(SkillTwo());
        }

        private IEnumerator SkillTwo()
        {
            dust.animator.SetTrigger("Skill2");
            Attack();
            yield return new WaitForSeconds(1.125f);
            FinishSKillTwo();
        }

        private void Attack()
        {
            Instantiate(skill2AttackObject, transform);
        }

        private void FinishSKillTwo()
        {
            dust.currentState = Dust.DustState.Idle;
            dust.animator.SetTrigger("Move");
        }
    }
}
