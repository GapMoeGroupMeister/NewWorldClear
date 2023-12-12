using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class FlashBang : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private Vector2 moveDir;
    private float originFlySpeed = 9f;
    private float flySpeed = 9f;
    private float flyTime = 2f;

    private float originRotateSpeed = 1f;
    private float rotateSpeed = 1f;
    [SerializeField] private float attackRange = 5;

    private Light2D flashLight;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        flashLight = transform.Find("FlashLight").GetComponent<Light2D>();
    }

    private void Start()
    {
        flashLight.intensity = 0;
    }

    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + Time.fixedDeltaTime * 360 * rotateSpeed);
        rb.velocity = moveDir.normalized * flySpeed;
        flySpeed *= 0.999f;
        rotateSpeed *= 0.99f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir * -1f);

        moveDir = Vector2.Reflect(moveDir, hit.normal);
        rotateSpeed = -1f;
        flySpeed *= 0.2f;
    }

    IEnumerator FlashBangRoutine()
    {
        yield return new WaitForSeconds(flyTime);
        flySpeed = 0f;

        Collider2D coll = Physics2D.OverlapCircle(transform.position, attackRange, 0, LayerMask.GetMask("Enemy_TEST"));

        sr.enabled = false;
        StartCoroutine("Flash");
        if (coll != null)
        {
            Debug.Log("맞은");
            if (coll.TryGetComponent<PlayerController>(out PlayerController p))
            {
                //여기서 기절시키는거 넣으셈
            }
        }
        //풀링은 여기에
    }

    IEnumerator Flash()
    {
        while (flashLight.intensity < 4)
        {
            flashLight.intensity += Time.deltaTime * 32;
            yield return null;
        }
        while (flashLight.intensity > 0)
        {
            flashLight.intensity -= Time.deltaTime * 32;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void Init(Vector2 pos, Vector2 dir)
    {
        sr.enabled = true;
        transform.position = pos;
        moveDir = dir;
        flySpeed = originFlySpeed;
        rotateSpeed = originRotateSpeed;
        StartCoroutine("FlashBangRoutine");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
