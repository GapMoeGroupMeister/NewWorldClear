using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tkfkadlsi
{

    public class Dust : Damageable
    {
        public EnemyData dustData;
        public Animator animator;
        public PlayerController target;

        public DustsCenter dustsCenter;
        public DustAttack dustAttack;
        public DustSkill_One skill_One;
        public DustSkill_Two skill_Two;
        public DustSkill_Three skill_Three;

        public GameObject dustAttackObject;

        public float def;
        public float atkDelay;
        public float range;

        private SpriteRenderer spriteRenderer;

        private float attackCount;
        private float skillOneCount;

        public float AttackCount
        {
            get
            {
                return attackCount;
            }

            set
            {
                attackCount = value;
                if (Skill_One_Condition())
                {
                    skill_One.StartSkill_One();
                }
            }
        }

        public float SkillOneCount
        {
            get
            {
                return skillOneCount;
            }
            set
            {
                skillOneCount = value;
                if (Skill_Two_Condition())
                {
                    skill_Two.StartSkill_Two();
                }
            }
        }

        public DustState currentState;

        public enum DustState
        {
            NotDetect,
            Idle,
            Attack,
            Skill_1,
            Skill_2,
            Skill_3
        }

        private void Awake()
        {
            ResetStat();

            currentState = DustState.NotDetect;
            dustsCenter = this.GetComponentInParent<DustsCenter>();
            animator = this.GetComponent<Animator>();
            dustAttack = this.GetComponent<DustAttack>();
            skill_One = this.GetComponent<DustSkill_One>();
            skill_Two = this.GetComponent<DustSkill_Two>();
            skill_Three = this.GetComponent<DustSkill_Three>();
            spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        private void ResetStat()
        {
            _maxHp = dustData.DefaultHP;
            _currentHp = dustData.DefaultHP;
            attackDamage = dustData.DefaultATK;
            _moveSpeed = dustData.DefaultSPD;
            atkDelay = dustData.AttackCycle;
            range = dustData.DetectRange;
            attackCount = 0;
        }

        private void Update()
        {
            if (currentState == DustState.NotDetect) return;

            if(currentState == DustState.Idle)
            {
                if(target.transform.position.y < transform.position.y)
                {
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                }
                else
                {
                    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
            }

            if (Skill_Three_Condition())
            {
                skill_Three.StartSkill_Three();
            }

            if (Attack_Condition() && currentState == DustState.Idle)
            {
                dustAttack.AttackStart();
            }
        }


        private bool Attack_Condition()
        {
            float currentDistance = Vector3.Distance(transform.position, target.transform.position);

            if (currentDistance < 3.5f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Skill_One_Condition()
        {
            if(attackCount >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Skill_Two_Condition()
        {
            if(skillOneCount >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Skill_Three_Condition()
        {
            float currentDistance = Vector3.Distance(transform.position, target.transform.position);

            if (currentDistance > 10f && currentState == DustState.Idle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void HitDamage(float damage)
        {
            damage -= def;
            if (damage < 0) damage = 0.1f;

            StartCoroutine(HitSprite());
            base.HitDamage(damage);
            if (_currentHp <= 0) Die();
        }

        public override void CriticalDamage(float damage, float percent)
        {
            damage -= def;
            if (damage < 0) damage = 0.1f;

            StartCoroutine(HitSprite());
            base.CriticalDamage(damage, percent);
            if (_currentHp <= 0) Die();
        }

        public override void BleedDamage(float damage)
        {
            damage -= def;
            if (damage < 0) damage = 0.1f;

            StartCoroutine(HitSprite());
            base.BleedDamage(damage);
            if (_currentHp <= 0) Die();
        }

        public override void PoisonDamage(float damage)
        {
            damage -= def;
            if (damage < 0) damage = 0.1f;

            StartCoroutine(HitSprite());
            base.PoisonDamage(damage);
            if (_currentHp <= 0) Die();
        }

        private IEnumerator HitSprite()
        {
            float hitTime = 0.125f;
            float t = 0;

            Color startColor = Color.red;
            Color endColor = Color.white;

            while(t < hitTime)
            {
                spriteRenderer.color = Color.Lerp(startColor, endColor, t / hitTime);

                t += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = endColor;
        }

        public override void Die()
        {
            LootManager.Instance.GenerateReward(dustData.Reward, transform.position, 4);
            Destroy(gameObject);
        }
    }
}