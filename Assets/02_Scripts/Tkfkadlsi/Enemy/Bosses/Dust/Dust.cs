using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tkfkadlsi;

public class Dust : Enemy
{
    private Rigidbody2D rigid;
    private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;
    private GameObject skill2WarningObject;
    private GameObject skill2ShieldObject;
    private PolygonCollider2D skill2AreaCollider;
    private CircleCollider2D circleCollider2D;
    public PlayerController playerController;
    [SerializeField] private GameObject skill3Blind;
    [SerializeField] private float dashM;
    [Range(0f, 360f)] [SerializeField] private float skill2Angle;
    [SerializeField] private float skill1Damage;
    public float skill2Damage;

    private bool checkTarget = false;
    private bool isSkillActive = false;
    private bool dustShield = false;
    private bool isDashing = false;

    public override void Awake()
    {
        SetState();
        SetStatus();
        base.Awake();
        rigid = this.GetComponent<Rigidbody2D>();
        trailRenderer = this.GetComponent<TrailRenderer>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        circleCollider2D = this.GetComponent<CircleCollider2D>();
        skill2WarningObject = transform.GetChild(0).gameObject;
        skill2ShieldObject = transform.GetChild(1).gameObject;
        skill2AreaCollider = transform.GetChild(2).GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        playerController = target.GetComponent<PlayerController>();
        skill2WarningObject.SetActive(false);
        skill2ShieldObject.SetActive(false);
        skill2AreaCollider.enabled = false;
        skill3Blind.SetActive(false);
        trailRenderer.enabled = false;

        StartCoroutine(SkillDelay());
    }

    public override void Update()
    {
        if (isSkillActive) return;
        ChoiceState();
        if (currentState == State.Attack) return;

        if (currentState == State.Move)
            Moveing();
    }

    public override void Hit(float damage)
    {
        if (currentState == DustState.NotDetect) return;

        if(currentState == DustState.Idle && Time.deltaTime > Random.Range(0.000f, 3.000f))
        {
            int rand = Random.Range(0, 10);
            if (rand < 3)
            {
                return;
            }
        }
    }

    public override void SetState()
    {
        idleState = new DustIdleState(this);
        moveState = new DustMoveState(this);
        attackState = new DustAttackState(this);
    }

    private void SetStatus()
    {
        damage -= def;
        if (damage < 0) return;
        hp -= damage;

        if (hp < 0)
            Dead();
    }

    private void Dead()
    {
        if (isSkillActive) return;
        float angle = Mathf.Atan2(playerController._rigidbody.position.y - rigid.position.y, playerController._rigidbody.position.x - rigid.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private IEnumerator SkillDelay()
    {
        float t = Random.Range(atkCycle, atkCycle * 3);
        yield return new WaitForSeconds(t);
        float sk = Random.Range(0, 100);

        if (CanAttackPlayer)
        {
            yield return new WaitUntil(() => CanAttackPlayer == false);
        }

        if (sk < 10)
        {
            StartCoroutine(Skill2());
        }
        else if (sk < 50)
        {
            StartCoroutine(Skill1());
        }
        else
        {
            StartCoroutine(Skill3());
        }

        isSkillActive = true;
    }

    private IEnumerator Skill1()
    {
        trailRenderer.enabled = true;
        isDashing = true;
        for (int i = 0; i < 3; i++)
        {
            Vector2 dir = playerController._rigidbody.position - rigid.position;
            dir = dir.normalized * dashM;
            yield return StartCoroutine(MoveLerp(0.125f, rigid.position + dir));
            skill2AreaCollider.enabled = true;
            yield return new WaitForSeconds(0.25f);
            skill2AreaCollider.enabled = false;
        }
        isDashing = false;
        trailRenderer.enabled = false;
        SkillExit();
    }

    private IEnumerator Skill2()
    {
        skill2WarningObject.SetActive(true);
        yield return StartCoroutine(ColorLerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 1), 3));
        skill2WarningObject.SetActive(false);

        StartCoroutine(Skill2_DustShieldOn());
        skill2AreaCollider.enabled = true;
        yield return new WaitForSeconds(0.25f);
        skill2AreaCollider.enabled = false;
        SkillExit();
    }


    private IEnumerator Skill2_DustShieldOn()
    {
        dustShield = true;
        skill2ShieldObject.SetActive(true);
        yield return new WaitForSeconds(20f);
        skill2ShieldObject.SetActive(false);
        dustShield = false;
    }

    private IEnumerator Skill3()
    {
        skill3Blind.SetActive(true);
        skill3Blind.transform.position = transform.position + (Vector3.down * 2);
        circleCollider2D.isTrigger = true;
        yield return StartCoroutine(MoveLerp(1.5f, rigid.position + Vector2.down * 2));
        spriteRenderer.enabled = false;
        skill3Blind.SetActive(false);
        yield return new WaitForSeconds(1);
        rigid.position = playerController._rigidbody.position;
        yield return new WaitForSeconds(1);
        skill3Blind.SetActive(true);
        skill3Blind.transform.position = transform.position;
        spriteRenderer.enabled = true;
        yield return StartCoroutine(MoveLerp(0.5f, rigid.position + Vector2.up * 2));
        skill3Blind.SetActive(false);
        circleCollider2D.isTrigger = false;
        SkillExit();
    }

        float rand = Random.Range(0.00f, 1.00f);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target && isDashing)
        {
            playerController.HitDamage(skill1Damage);
        }
    }

    private IEnumerator MoveLerp(float lerpTime, Vector2 endPos)
    {
        float t = 0;
        Vector2 startPos = transform.position;

        while (t < lerpTime)
        {
            return false;
        }
    }

    private bool Skill_Three_Condition()
    {
        if ((target.transform.position - dustsCenter.transform.position).magnitude > 6f) return true;
        if (attackCount < 5) return false;

        while (t < lerpTime)
        {
            skill2WarningObjectSpriteRenderer.color = Color.Lerp(startColor, endColor, t / lerpTime);
        if(rand < 0.33f)
        {
            return true;
        }
        else
        {
            return false;
        }

        skill2WarningObjectSpriteRenderer.color = endColor;
    }

    public override void Dead()
    {
        throw new System.NotImplementedException();
    }

    public class DustIdleState : StateBase
    {
        public DustIdleState(Enemy initenemy) : base(initenemy) { }

        public override void OnStateEnter()
        {

        }

        public override void OnStateExit()
        {

        }

        public override void OnStateUpdate()
        {

        }
    }

    public class DustMoveState : StateBase
    {
        public DustMoveState(Enemy initenemy) : base(initenemy) { }

        public override void OnStateEnter()
        {

        }

        public override void OnStateExit()
        {

        }

        public override void OnStateUpdate()
        {
            Vector3 moveDir = Vector3.zero;
            moveDir = enemy.target.transform.position - enemy.transform.position;
            moveDir = moveDir.normalized;
            enemy.transform.position += moveDir * enemy.spd * Time.deltaTime;
        }
    }

    public class DustAttackState : StateBase
    {
        public DustAttackState(Enemy initenemy) : base(initenemy) { }

        private float cycle;

        public override void OnStateEnter()
        {
            cycle = enemy.atkCycle;
        }

        public override void OnStateExit()
        {

        }

        public override void OnStateUpdate()
        {
            cycle -= Time.deltaTime;



            if (cycle < 0)
            {
                PlayerController playerController = enemy.target.GetComponent<PlayerController>();
                playerController.HitDamage(enemy.atk);
                cycle = enemy.atkCycle;
            }
        }
    }
}
