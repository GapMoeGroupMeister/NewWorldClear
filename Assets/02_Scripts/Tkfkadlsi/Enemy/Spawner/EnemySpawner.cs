using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<string> SpawnMobList;

        private List<Transform> spawnPoints;

        public void SpawnMonster()
        {
            int idx = Random.Range(0, SpawnMobList.Count);
            GameObject spawnMob = PoolManager.Instance.GetObject(SpawnMobList[idx]);


            idx = Random.Range(0, spawnPoints.Count);
            spawnMob.transform.position = spawnPoints[idx].position;
        }
    }
}

