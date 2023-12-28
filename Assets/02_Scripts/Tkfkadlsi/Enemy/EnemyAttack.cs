using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tkfkadlsi
{
    public class EnemyAttack : MonoBehaviour
    {
        Enemy enemy;

        private void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
            StartCoroutine(LifeTime());
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(0.01f);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController playerController = enemy.target.GetComponent<PlayerController>();
                playerController.HitDamage(enemy.attackDamage);
            }
        }
    }
}
