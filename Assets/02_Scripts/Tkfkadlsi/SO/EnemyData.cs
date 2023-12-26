using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    [CreateAssetMenu(fileName = "EnemyData", menuName ="SO/Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string enemyName;
        public string EnemyName { get { return enemyName; } }

        [SerializeField] private float defaultHP;
        public float DefaultHP { get { return defaultHP; } }

        [SerializeField] private float defaultATK;
        public float DefaultATK { get { return defaultATK; } }

        [SerializeField] private float defaultSPD;
        public float DefaultSPD { get { return defaultSPD; } }

        [SerializeField] private float attackCycle;
        public float AttackCycle { get { return attackCycle; } }

        [SerializeField] private float detectRange;
        public float DetectRange { get { return detectRange; } }

        [SerializeField] private float attackRange;
        public float AttackRange { get { return attackRange; } }

        [SerializeField] private Reward reward;
        public Reward Reward { get { return reward; } }
    }
}