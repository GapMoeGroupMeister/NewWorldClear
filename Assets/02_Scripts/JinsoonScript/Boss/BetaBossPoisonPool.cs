using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BetaBossPoisonPool : MonoBehaviour
{
    private float duration = 5f;
    private SpriteRenderer sr = null;
    private float dotDamageTime = 0.2f;
    private float dotDamageTimeDown = 0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
        transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(duration);
        sr.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                //���⼭�� Ǯ�� ���ּ�
            });
    }

    public void Init(Vector2 position)  //���ӽð� ���߿� �޾ƿͼ� ���� ���ִ°� ������?
    {
        transform.position = position;
        StartCoroutine("StartRoutine");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController p))
        {
            if (dotDamageTimeDown <= 0)
            {
                //©©�̷� ������ �ְ�
                //������� ��� �ɾ���

                dotDamageTimeDown = dotDamageTime;
            }
        }
    }

}
