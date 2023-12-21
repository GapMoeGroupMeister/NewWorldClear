using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMaskBullet : MonoBehaviour
{
    private SpriteRenderer sr;
    private TrailRenderer trailRenderer;
    private Material mat;

    [ColorUsage(true,true)]
    [SerializeField] private Color bulletColor;
    private float bulletSpeed = 60f;
    private float bulletLifeTime = 1.5f;
    private float bulletLifeTimeDown = 0;


    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
        //mat.SetColor("EmissionColor", bulletColor);
        //Vector4 hdrColor = new Vector4(bulletColor.r, bulletColor.g, bulletColor.b, 3.5f);
        mat.SetColor("_EmissionColor", bulletColor);
        trailRenderer.material.SetColor("_EmissionColor", bulletColor);
    }

    private void OnEnable()
    {
        bulletLifeTimeDown = bulletLifeTime;
    }

    private void Update()
    {
        bulletLifeTimeDown -= Time.deltaTime;
        if (bulletLifeTimeDown < 0)
        {
            Destroy(gameObject);
        }

        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }
}
