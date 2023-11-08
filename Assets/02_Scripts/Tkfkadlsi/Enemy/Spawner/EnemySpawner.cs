using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<string> SpawnMobList;

        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private float spawnDelay;

        private void Start()
        {
            StartCoroutine(MobSpawnStart());
        }

        private IEnumerator MobSpawnStart()
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnMonster();
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        public void SpawnMonster()
        {
            int idx = Random.Range(0, SpawnMobList.Count);
            GameObject spawnMob = PoolManager.Instance.GetObject(SpawnMobList[idx]);

            idx = Random.Range(0, spawnPoints.Count);
            spawnMob.transform.position = spawnPoints[idx].position;
        }
    }
}

