using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BetaBossFlashLight : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private Vector2 moveDir;
    private float originFlySpeed = 5f;
    private float flySpeed = 5f;
    private float flyTime = 2f;

    private float rotateSpeed = 1f;

    private Sequence seq = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + Time.fixedDeltaTime * 360 * rotateSpeed);
        rb.velocity = moveDir.normalized * flySpeed;
        flySpeed *= 0.99f;
        rotateSpeed *= 0.99f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir * -1f);

        moveDir = Vector2.Reflect(moveDir, hit.normal);
        rotateSpeed = -1f;
        flySpeed *= 0.2f;
    }

    IEnumerator StartRoutine()
    {
        seq = DOTween.Sequence();
        Vector3 originScale = transform.localScale;

        seq.Append(transform.DOScale(originScale * 1.5f, 1))
            .Append(transform.DOScale(originScale, 1));

        yield return new WaitForSeconds(flyTime);
        flySpeed = 0f;

        Collider2D coll = Physics2D.OverlapBox(transform.position, new Vector2(5, 5), 0, LayerMask.GetMask("Player"));

        //터지는 이펙트 여기로!
        if (coll != null)
        {
            if (coll.TryGetComponent<PlayerController>(out PlayerController p))
            {
                //여기서 기절시키는거 넣으셈
            }
        }
        gameObject.SetActive(false);
        //풀링은 여기에
    }

    public void Init(Vector2 pos, Vector2 dir)
    {
        transform.position = pos;
        moveDir = dir;
        flySpeed = originFlySpeed;
        StartCoroutine("StartRoutine");
    }
}
