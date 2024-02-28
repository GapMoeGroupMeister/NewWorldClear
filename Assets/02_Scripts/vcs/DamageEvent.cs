using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageEvent : MonoBehaviour
{
    [SerializeField] private float Offset;
    [SerializeField] private int damage;
    [SerializeField] private bool isCrit;
    
    private Sequence seq;
    private TextMeshPro damageText;
    private Transform MoveObj;
    private ParticleSystem normalParticle;
    private ParticleSystem critParticle;
    
    private Color defaultColor = Color.white;
    private Color critColor = new Color(1f, 0.6f, 0.6f);
    private Vector3 defaultScale = new Vector3(1.5f,1.5f,1);
    private Vector3 critScale = new Vector3(2.5f, 2.5f, 1);
    
    private void Awake()
    {
        MoveObj = transform.Find("Move").transform;
        normalParticle = transform.Find("NormalParticle").GetComponent<ParticleSystem>();
        critParticle = transform.Find("CritParticle").GetComponent<ParticleSystem>();
        damageText = MoveObj.Find("DamageText").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        ShowEvent(damage, isCrit);
    }

    /**
     * <summary>
     * 데미지 이벤트 발생시 사용
     * </summary>
     */
    public void ShowEvent(int _damage, bool isCrit)
    {
        this.isCrit = isCrit;
        this.damage = _damage;
        MoveObj.position = new Vector3(transform.position.x, transform.position.y + Offset);
        damageText.text = _damage.ToString();
        
       
        if (!isCrit)
        {// 평타
            damageText.color = defaultColor;
            damageText.transform.localScale = defaultScale;
            normalParticle.Play();
        }
        else
        {// 치명타
            damageText.color = critColor;
            damageText.transform.localScale = critScale;
            critParticle.Play();
        }
        StartCoroutine(EventRoutine());
    }

    private IEnumerator EventRoutine()
    {
        damageText.transform.DOScale(new Vector3(0, 0, 1), 0.5f).SetEase(Ease.InExpo);
        MoveObj.DOMoveY(MoveObj.position.y + 1.3f, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(2f);
        PoolManager.Release(gameObject);

    }
    
}