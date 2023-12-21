using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAttack : MonoBehaviour
{
    [SerializeField] private AnimationCurve moveCurve;
    private Dust dust;
    private DustsCenter dustsCenter;

    private void Awake()
    {
        dust = this.GetComponent<Dust>();
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
        yield return StartCoroutine(AttackMoveLerpEnter(dust.transform, 0.75f));

        Attack();
        yield return StartCoroutine(AttackMoveLerpFinish(dust.transform, 1.0f));

        FinishAttack();
    }

    private void Attack()
    {

    }

    private void FinishAttack()
    {
        dust.currentState = Dust.DustState.Idle;
        dust.AttackCount++;
    }

    private IEnumerator AttackMoveLerpEnter(Transform trm,float moveTime)
    {
        float t = 0;
        Vector3 startpos = new Vector3(0, 4);
        Vector3 endpos = new Vector3(0, 1);

        while (t < moveTime)
        {
            trm.localPosition = Vector3.Lerp(startpos, endpos, moveCurve.Evaluate(t / moveTime));

            t += Time.deltaTime;
            yield return null;
        }

        trm.localPosition = endpos;
    }

    private IEnumerator AttackMoveLerpFinish(Transform trm,float moveTime)
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

            t += Time.deltaTime;
            yield return null;
        }

        dustsCenter.transform.position = endPos;
    }
}