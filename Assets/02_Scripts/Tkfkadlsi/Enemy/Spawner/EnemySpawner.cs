using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<GameObject> SpawnMobList;

        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private float spawnDelay;
        [SerializeField] private Transform MobParents;
        
        private void Start()
        {
            StartCoroutine(SpawnMonster());
        }

        public IEnumerator SpawnMonster()
        {
            int idx = Random.Range(0, SpawnMobList.Count);
            GameObject spawnMob = PoolManager.Get(SpawnMobList[idx], MobParents);

            idx = Random.Range(0, spawnPoints.Count);
            spawnMob.transform.position = spawnPoints[idx].position;
            yield return new WaitForSeconds(spawnDelay);

            StartCoroutine(SpawnMonster());
        }

        public void StopSpawnMob()
        {
            StopAllCoroutines();
        }
    }
}

