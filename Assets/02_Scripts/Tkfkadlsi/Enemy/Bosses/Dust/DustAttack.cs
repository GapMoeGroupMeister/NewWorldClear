using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class DustAttack : MonoBehaviour
    {
        [SerializeField] private AnimationCurve moveCurve;
        private Dust dust;
        private DustsCenter dustsCenter;

        private void Awake()
        {
            dust = this.GetComponent<Dust>();
        }

        private void Start()
        {
            dustsCenter = dust.dustsCenter;
        }

        public void AttackStart()
        {
            dust.currentState = Dust.DustState.Attack;
            StartCoroutine(AttackMove());
        }

        private IEnumerator AttackMove()
        {
            StartCoroutine(CenterMoveLerp(0.75f));
            yield return StartCoroutine(AttackMoveLerpEnter(dust.transform, 0.1f));

            yield return StartCoroutine(Attack());

            yield return StartCoroutine(AttackMoveLerpFinish(dust.transform, 0.75f));

            FinishAttack();
        }

        private IEnumerator Attack()
        {
            dust.animator.SetTrigger("Attack");
            Instantiate(dust.dustAttackObject, transform);
            yield return new WaitForSeconds(0.875f);
        }

        private void FinishAttack()
        {
            //dustsCenter.z = Random.Range(-180.0f, 180.0f);
            //dustsCenter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, dust.dustsCenter.z));
            dust.animator.SetTrigger("Move");
            dust.currentState = Dust.DustState.Idle;
            dust.AttackCount++;
        }

        private IEnumerator AttackMoveLerpEnter(Transform trm, float moveTime)
        {
            float t = 0;
            Vector3 startpos = new Vector3(0, 4);
            Vector3 endpos = new Vector3(0, 1);

            if (dust.target.transform.position.x < dust.transform.position.x)
            {
                dust.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            else
            {
                dust.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            while (t < moveTime)
            {
                trm.localPosition = Vector3.Lerp(startpos, endpos, moveCurve.Evaluate(t / moveTime));


                t += Time.deltaTime;
                yield return null;
            }

            trm.localPosition = endpos;
        }

        private IEnumerator AttackMoveLerpFinish(Transform trm, float moveTime)
        {
            float t = 0;
            Vector3 startpos = new Vector3(0, 1);
            Vector3 endpos = new Vector3(0, 4);

            while (t < moveTime)
            {
                trm.localPosition = Vector3.Lerp(startpos, endpos, moveCurve.Evaluate(t / moveTime));

                t += Time.deltaTime;
                yield return null;
            }

            trm.localPosition = endpos;
        }

        private IEnumerator CenterMoveLerp(float moveTime)
        {
            float t = 0;
            Vector3 startPos = dustsCenter.transform.position;
            Vector3 endPos = dust.target.transform.position;

            while (t < moveTime)
            {
                dustsCenter.transform.position = Vector3.Lerp(startPos, endPos, moveCurve.Evaluate(t / moveTime));

                if(t < moveTime/2)
                    endPos = dust.target.transform.position;

                t += Time.deltaTime;
                yield return null;
            }

            dustsCenter.transform.position = endPos;
        }
    }
}