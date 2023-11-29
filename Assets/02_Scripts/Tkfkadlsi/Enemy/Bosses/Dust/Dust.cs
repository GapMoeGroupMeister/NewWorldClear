using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tkfkadlsi;

public class Dust : MonoBehaviour
{
    private PlayerController target;
    private Rigidbody2D rigid;
    private TrailRenderer trailRenderer;
    private GameObject skill2WarningObject;

    public EnemyData data;

    private bool checkTarget = false;
    private bool isSkillActive = false;
    private bool dustShield = false;

    public float hp;
    public float atk;
    public float def;
    public float spd;
    public float atkCycle;
    public float range;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>();
        rigid = this.GetComponent<Rigidbody2D>();
        trailRenderer = this.GetComponent<TrailRenderer>();
        skill2WarningObject = transform.GetChild(0).gameObject;
        SetStatus();

        skill2WarningObject.SetActive(false);
        trailRenderer.enabled = false;
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

    public void Hit(float damage)
    {
        if (dustShield)
        {
            int rand = Random.Range(0, 10);
            if(rand < 3)
            {
                return;
            }
        }

        hp -= damage;
        if (hp < 0) Dead();
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
        float t = Random.Range(atkCycle, atkCycle*3);
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
        trailRenderer.enabled = true;
        for(int i = 0; i < 3; i++)
        {
            Vector2 dir = target._rigidbody.position - rigid.position;
            dir = dir.normalized * 10f;
            yield return StartCoroutine(MoveLerp(0.125f, rigid.position + dir));
            yield return new WaitForSeconds(0.25f);
        }
        trailRenderer.enabled = false;
        SkillExit();
    }

    private IEnumerator Skill2()
    {
        skill2WarningObject.SetActive(true);
        yield return StartCoroutine(ColorLerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 1), 3));
        skill2WarningObject.SetActive(false);
        StartCoroutine(Skill2_DustShieldOn());
        SkillExit();
    }

    private IEnumerator Skill2_DustShieldOn()
    {
        dustShield = true;
        yield return new WaitForSeconds(20f);
        dustShield = false;
    }

    private IEnumerator Skill3()
    {
        yield return null;
    }

    private void SkillExit()
    {
        isSkillActive = false;
        StartCoroutine(SkillDelay());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == target)
        {
            PlayerController playerController = target.GetComponent<PlayerController>();
            playerController.HitDamage(atk);
        }
    }

    private IEnumerator MoveLerp(float lerpTime, Vector2 endPos)
    {
        float t = 0;
        Vector2 startPos = transform.position;

        while(t < lerpTime)
        {
            rigid.position = Vector2.Lerp(startPos, endPos, t / lerpTime);

            t += Time.deltaTime;
            yield return null;
        }

        rigid.position = endPos;
    }

    private IEnumerator ColorLerp(Color startColor, Color endColor, float lerpTime)
    {
        float t = 0;
        SpriteRenderer spriteRenderer = skill2WarningObject.GetComponent<SpriteRenderer>();

        while(t < lerpTime)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, t / lerpTime);

            t += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = endColor;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
