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
                //���⼭�� Ǯ�� ���ּ�
            });
    }

    public void FieldPoision()  //���ӽð� ���߿� �޾ƿͼ� ���� ���ִ°� ������?
    {
        StartCoroutine("StartRoutine");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController p))
        {
            if (dotDamageTimeDown <= 0)
            {
                Debug.Log("���ƾƾƾƤ�����");
                //©©�̷� ������ �ְ�
                //������� ��� �ɾ���

                dotDamageTimeDown = dotDamageTime;
            }
        }
    }

}
