using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class OutlineGlitch : MonoBehaviour
{
    private Outline outline;
    
    [Range(0.01f, 1f)]
    public float frequency = 0.5f;

    public bool onOff;



    private void Awake()
    {
        outline = GetComponent<Outline>();
    }


    private void Start()
    {
        On();
    }

    private IEnumerator OnOffRoutine()
    {
        float term = Term();
        while (onOff)
        {
            outline.effectDistance = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
            yield return new WaitForSeconds(term);
            outline.effectDistance = new Vector2(0,0);
            yield return new WaitForSeconds(term*1.1f);
            term = Term();
        }
        
    }

    private float Term()
    {
        float term = Mathf.Clamp((Random.Range(0f, 0.5f) * (1.01f - frequency)), 0f, 1f);
        return term;

    }

    public void On()
    {
        onOff = true;
        StartCoroutine(OnOffRoutine());
    }

    public void Off()
    {
        onOff = false;
        outline.effectDistance = new Vector2(0,0);
        StopCoroutine(OnOffRoutine());
    }
}
