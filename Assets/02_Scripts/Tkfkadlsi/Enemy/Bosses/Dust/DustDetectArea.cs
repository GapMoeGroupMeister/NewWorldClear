using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{

    public class DustDetectArea : MonoBehaviour
    {
        private Dust dust;
        private DustsCenter dustsCenter;
        private CircleCollider2D dustDetectArea;

        private void Awake()
        {
            dust = GetComponentInParent<Dust>();
            dustsCenter = transform.root.GetComponent<DustsCenter>();
            dustDetectArea = this.GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            dustDetectArea.radius = dust.range;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                dust.animator.SetTrigger("Detect");
                dust.skill_Three.StartSkill_Three();
                dust.target = collision.gameObject.GetComponent<PlayerController>();
                Destroy(gameObject);
            }
        }
    }
}