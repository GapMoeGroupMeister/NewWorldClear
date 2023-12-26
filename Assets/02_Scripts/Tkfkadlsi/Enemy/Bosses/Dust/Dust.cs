using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tkfkadlsi
{

    public class Dust : MonoBehaviour
    {
        public EnemyData dustData;
        public Animator animator;
        public PlayerController target;

        public DustsCenter dustsCenter;
        public DustAttack dustAttack;
        public DustSkill_One skill_One;
        public DustSkill_Two skill_Two;
        public DustSkill_Three skill_Three;

        public float hp;
        public float atk;
        public float def;
        public float speed;
        public float atkDelay;
        public float range;

        public DustState currentState;

        private int attackCount = 0;
        public int AttackCount
        {
            get
            {
                return attackCount;
            }
            set
            {
                if (value < 0) return;
                attackCount = value;

                Skills_Condition();
            }
        }

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
            dustAttack = this.GetComponent<DustAttack>();
            skill_One = this.GetComponent<DustSkill_One>();
            skill_Three = this.GetComponent<DustSkill_Three>();
        }

        private void ResetStat()
        {
            hp = dustData.DefaultHP;
            atk = dustData.DefaultATK;
            speed = dustData.DefaultSPD;
            atkDelay = dustData.AttackCycle;
            range = dustData.DetectRange;
        }

        private void Update()
        {
            if (currentState == DustState.NotDetect) return;

            if (currentState == DustState.Idle && Time.deltaTime > Random.Range(0.000f, 3.000f))
            {
                dustAttack.AttackStart();
            }
        }

        public void Hit(float damage)
        {
            damage -= def;
            if (damage < 0) return;
            hp -= damage;

            if (hp < 0)
                Dead();
        }

        private void Dead()
        {
            Destroy(gameObject);
        }

        private void Skills_Condition()
        {
            if (Skill_Three_Condition())
            {
                skill_Three.StartSkill_Three();
                return;
            }
            if (Skill_One_Condition())
            {
                skill_One.StartSkill_One();
                return;
            }
            if (Skill_Two_Condition())
            {
                skill_Two.StartSkill_Two();
                return;
            }
        }

        private bool Skill_One_Condition()
        {
            if (attackCount < 4) return false;

            float rand = Random.Range(0.00f, 1.00f);

            if (rand <= 0.33f)
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
            if (attackCount < 6) return false;

            float rand = Random.Range(0.00f, 1.00f);

            if (rand <= 0.25f)
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
            if ((target.transform.position - dustsCenter.transform.position).magnitude > 6f) return true;
            if (attackCount < 5) return false;

            float rand = Random.Range(0.00f, 1.00f);

            if (rand < 0.1667f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}