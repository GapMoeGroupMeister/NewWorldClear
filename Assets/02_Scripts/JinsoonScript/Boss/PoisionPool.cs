using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoisionPool : MonoBehaviour
{
    private float duration = 5f;
    private SpriteRenderer sr = null;
    private float dotDamageTime = 0.2f;
    private float dotDamageTimeDown = 0f;
    private Collider2D col;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        col.enabled = false;
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        col.enabled = false;
    }

    private void Update()
    {
        if (dotDamageTimeDown >= 0)
        {
            dotDamageTimeDown -= Time.deltaTime;
        }
    }

    IEnumerator StartRoutine()
    {
        col.enabled = true;
        transform.DOScale(4, 0.2f);
        yield return new WaitForSeconds(duration);
        sr.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                //여기서도 풀링 해주셈
            });
    }

    public void FieldPoision()  //지속시간 나중에 받아와서 쓰게 해주는게 좋겠지?
    {
        StartCoroutine("StartRoutine");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController p))
        {
            if (dotDamageTimeDown <= 0)
            {
                Debug.Log("으아아아아ㅏㅋㅋ");
                //짤짤이로 데미지 넣고
                //독디버프 계속 걸어줘

                dotDamageTimeDown = dotDamageTime;
            }
        }
    }

}
