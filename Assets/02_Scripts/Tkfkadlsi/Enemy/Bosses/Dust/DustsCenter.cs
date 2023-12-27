using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{

    public class DustsCenter : MonoBehaviour
    {
        public float rotationSpeed;
        public float moveSpeed;
        public float z;

        private CircleCollider2D dustDetectArea;
        private Dust dust;

        private Vector3 direction;

        private void Awake()
        {
            dust = this.GetComponentInChildren<Dust>();
            dustDetectArea = this.GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            moveSpeed = dust._moveSpeed;
            dustDetectArea.radius = dust.range;
        }

        private void Update()
        {
            if (dust.currentState != Dust.DustState.Idle) return;
            Rotating();
        }

        private void Rotating()
        {
            z += moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));
            dust.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (dust.currentState == Dust.DustState.Idle)
                    dust.skill_Three.StartSkill_Three();
            }
        }
    }
}