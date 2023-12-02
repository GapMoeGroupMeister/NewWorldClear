using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tkfkadlsi;

public class Dust : MonoBehaviour
{
    private PlayerController target;
    private Rigidbody2D rigid;
    private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;
    private GameObject skill2WarningObject;
    [SerializeField] private GameObject skill3Blind;
    [SerializeField] private float dashM;
    [Range(0f, 360f)] [SerializeField] private float skill2Angle;

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
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        skill2WarningObject = transform.GetChild(0).gameObject;
        SetStatus();

        skill2WarningObject.SetActive(false);
        skill3Blind.SetActive(false);
        trailRenderer.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(SkillStart());
    }

    private Vector2 AngleToDir(float angle)
    {
        float radian = (angle - 90) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
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
        if (isSkillActive) return;

        Vector2 dir = target.transform.position - transform.position;

        rigid.MovePosition(rigid.position += dir.normalized * spd * Time.deltaTime);


        float angle = Mathf.Atan2(target._rigidbody.position.y - rigid.position.y, target._rigidbody.position.x - rigid.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
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
            dir = dir.normalized * dashM;
            yield return StartCoroutine(MoveLerp(0.125f, rigid.position + dir));
            yield return new WaitForSeconds(0.25f);
        }
        trailRenderer.enabled = false;
        SkillExit();
    }

    private IEnumerator Skill2()
    {
        float lookingAngle = transform.rotation.eulerAngles.z;  //캐릭터가 바라보는 방향의 각도
        Vector3 rightDir = AngleToDir(lookingAngle + skill2Angle * 0.5f);
        Vector3 leftDir = AngleToDir(lookingAngle - skill2Angle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(transform.position, rightDir * 5, Color.blue);
        Debug.DrawRay(transform.position, leftDir * 5, Color.blue);
        Debug.DrawRay(transform.position, lookDir * 5, Color.cyan);

        StartCoroutine(Skill2_DustShieldOn());
        yield return new WaitForSeconds(0.25f);
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
        skill3Blind.SetActive(true);
        skill3Blind.transform.position = transform.position + (Vector3.down * 2);
        yield return StartCoroutine(MoveLerp(1.5f, rigid.position + Vector2.down * 2));
        spriteRenderer.enabled = false;
        skill3Blind.SetActive(false);
        yield return new WaitForSeconds(1);
        rigid.position = target._rigidbody.position;
        yield return new WaitForSeconds(1);
        skill3Blind.SetActive(true);
        skill3Blind.transform.position = transform.position;
        spriteRenderer.enabled = true;
        yield return StartCoroutine(MoveLerp(0.5f, rigid.position + Vector2.up * 2));
        skill3Blind.SetActive(false);
        SkillExit();
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
        SpriteRenderer skill2WarningObjectSpriteRenderer = skill2WarningObject.GetComponent<SpriteRenderer>();

        while(t < lerpTime)
        {
            skill2WarningObjectSpriteRenderer.color = Color.Lerp(startColor, endColor, t / lerpTime);

            t += Time.deltaTime;
            yield return null;
        }

        skill2WarningObjectSpriteRenderer.color = endColor;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
