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

        public float def;
        public float atkDelay;
        public float coolTime;
        public float range;

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
            dustAttack = this.GetComponent<DustAttack>();
            skill_One = this.GetComponent<DustSkill_One>();
            skill_Three = this.GetComponent<DustSkill_Three>();
        }

        private void ResetStat()
        {
            _maxHp = dustData.DefaultHP;
            _currentHp = dustData.DefaultHP;
            damage = dustData.DefaultATK;
            _moveSpeed = dustData.DefaultSPD;
            atkDelay = dustData.AttackCycle;
            range = dustData.DetectRange;
            coolTime = 0;
        }

        private void Update()
        {
            if (currentState == DustState.NotDetect) return;

            if (Attack_Condition() && currentState == DustState.Idle)
            {
                dustAttack.AttackStart();
            }

            coolTime += Time.deltaTime;
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

        public override void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}