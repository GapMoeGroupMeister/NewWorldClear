using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlphaBossTrap : MonoBehaviour
{
    private SpriteRenderer sr = null;
    [SerializeField] private float fadeOutTime = 5f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()     //����׿� Ǯ���ϸ� �̰� �����ּ�
    {
        StartCoroutine("FadeOutRoutine");   
    }

    IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(fadeOutTime);
        FadeOut();
    }

    private void FadeOut()
    {
        sr.DOFade(0, 0.5f);
    }

    public void Init(Vector2 position)
    {
        transform.position = position;
        StartCoroutine("FadeOutRoutine");
        //�� ���� �ִϸ��̼��� �������ְ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController p))
        {
            //���� ����, ������ �ְ�
        }
        
    }
}
