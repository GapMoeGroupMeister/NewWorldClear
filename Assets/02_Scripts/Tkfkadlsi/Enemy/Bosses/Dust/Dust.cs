using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tkfkadlsi;

public class Dust : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D rigid;
    private EnemyData data;

    private bool checkTarget = false;
    private bool isSkillActive = false;

    public float hp;
    public float atk;
    public float def;
    public float spd;
    public float atkCycle;
    public float range;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
        rigid = this.GetComponent<Rigidbody2D>();
        SetStatus();
    }

    private void Start()
    {
        StartCoroutine(SkillStart());
    }

    private void Update()
    {
        if (isSkillActive) return;

        if (checkTarget)
            Moveing();
        else if (!checkTarget)
            DetectTarget();
    }

    private void SetStatus()
    {
        hp = data.DefaultHP;
        atk = data.DefaultATK;
        def = data.DefaultDEF;
        spd = data.DefaultSPD;
        atkCycle = data.AttackCycle;
        range = data.DetectRange;
    }

    private void Moveing()
    {
        Vector2 dir = target.transform.position - transform.position;

        rigid.MovePosition(rigid.position += dir.normalized * spd * Time.deltaTime);


    }

    private void DetectTarget()
    {
        if(data.DetectRange >= Vector2.Distance(transform.position, target.transform.position))
        {
            checkTarget = true;
        }
    }

    private IEnumerator SkillStart()
    {
        yield return new WaitUntil(() => checkTarget == true);
        StartCoroutine(SkillDelay());
    }

    private IEnumerator SkillDelay()
    {
        float t = Random.Range(1.50f, 3.50f);
        yield return new WaitForSeconds(t);
        float sk = Random.Range(1, 4);

        switch (sk)
        {
            case 1:
                StartCoroutine(Skill1());
                break;
            case 2:
                StartCoroutine(Skill2());
                break;
            case 3:
                StartCoroutine(Skill3());
                break;
        }

        isSkillActive = true;
    }

    private IEnumerator Skill1()
    {
        for(int i = 0; i < 3; i++)
        {

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Skill2()
    {
        yield return null;
    }

    private IEnumerator Skill3()
    {
        yield return null;
    }

    private void SkillExit()
    {
        isSkillActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == target)
        {
            PlayerController playerController = target.GetComponent<PlayerController>();
            playerController.HitDamage(atk);
        }
    }
}
