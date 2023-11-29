using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextContent : TMPInfo
{
    public int index;
    public string content;
    public float duration;
    public bool isReWrite;

    public void TextPrint()
    {
        if (isReWrite)
        {
            AddWrite(content, duration);
        }
    }
}

/**
 * 이 클래스는 TextMeshProGUI를 다루는 2D용 TMP 스크립트임
 */

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPInfo : MonoBehaviour
{
    public static TextMeshProUGUI TMP;


    [Tooltip("'$v' 로 content 중간중간에 ")]
    public string content;

    
    public object[] insertValue;
    

    private void Awake()
    {
        try
        {
            TMP = GetComponent<TextMeshProUGUI>();

        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("<color='red'>[ TMPInfo ] 컴포넌트에 TMP가 없습니다 </color>");
            throw;
        }
    }

    /**
     * <summary>
     * Text 재작성 메서드
     * </summary>
     */
    public void Write(string content)
    {
        TMP.text = content;
    }

    /**
     * <summary>
     * Text 재작성 메서드, Duration으로 출력할 시간을 입력 할 수 있다
     * </summary>
     */
    public void Write(string content, float duration)
    {
        TMP.text = "";
        StartCoroutine(WriteRoutine(content, duration));
    }
    /**
     * <summary>
     * Text 추가 작성 메서드
     * </summary>
     */
    public void AddWrite(string content)
    {
        TMP.text += content;
    }

    /**
     * <summar>
     * Text 추가 작성 메서드,  Duration으로 출력할 시간을 입력 할 수 있다
     * </summary>
     */
    public void AddWrite(string content, float duration)
    {
        if (duration <= 0)
        {
            AddWrite(content);
            return;
        }
        StartCoroutine(WriteRoutine(content, duration));
    }

    private IEnumerator WriteRoutine(string content, float duration)
    {
        float term = Mathf.Clamp(duration / content.Length, 0f,10f);
        foreach(var word in content)
        {
            TMP.text += word.ToString();
            yield return new WaitForSeconds(term);

        }
        
    }
}
