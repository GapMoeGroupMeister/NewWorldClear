using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq.Expressions;

public class AlphaBossTrap : MonoBehaviour
{
    private SpriteRenderer sr = null;
    [SerializeField] private float fadeOutTime = 5f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()     //디버그용 풀링하면 이거 지워주셈
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
        //뭐 시작 애니메이션을 실행해주고
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController p))
        {
            //대충 기절, 데미지 주고
            StopCoroutine("FadeOutRoutine");
            Debug.Log("덫에걸린...");
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1); 
        }
        
    }
}
