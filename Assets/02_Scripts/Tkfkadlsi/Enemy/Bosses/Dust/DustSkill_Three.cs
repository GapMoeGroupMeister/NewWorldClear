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

            dust.animator.SetTrigger("Skill3Down");
            yield return new WaitForSeconds(0.875f);

            if (dust.target.transform.localScale.x == 1)
            {
                transform.position = dust.target.transform.position + Vector3.left;
            }
            else if (dust.target.transform.localScale.x == -1)
            {
                transform.position = dust.target.transform.position + Vector3.right;
            }

            dust.transform.rotation = Quaternion.Euler(0, 0, 0);

            dust.animator.SetTrigger("Skill3Up");
            yield return new WaitForSeconds(1.0f);

            yield return StartCoroutine(Attack());

            dust.animator.SetTrigger("Skill3Down");
            yield return new WaitForSeconds(0.875f);

            transform.localPosition = localPos;
            dust.transform.rotation = Quaternion.Euler(0, 0, 0);
            dustsCenter.transform.position = dust.target.transform.position;
            dust.animator.SetTrigger("Skill3Up");
            yield return new WaitForSeconds(1.0f);
            Finish_SkillThree();
        }

        private IEnumerator Attack()
        {
            dust.animator.SetTrigger("Attack");
            Instantiate(dust.dustAttackObject, transform);
            yield return new WaitForSeconds(0.875f);
        }

        private void Finish_SkillThree()
        {
            dust.transform.localPosition = new Vector3(0, 4);
            dust.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            dust.animator.SetTrigger("Move");
            dust.currentState = Dust.DustState.Idle;
        }
    }
}