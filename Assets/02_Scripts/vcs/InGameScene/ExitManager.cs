using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private bool isExitReady;

    private bool isExit;
    [SerializeField] private float currentTime;
    [SerializeField] private float CoolTime = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isExitReady && !isExit)
        {
            CountTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isExitReady = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isExitReady = false;
            currentTime = 0;
            GameManager.Instance._UIManager.RefreshExitGauge(0);
        }
    }

    private void CountTime()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= CoolTime)
        {
            isExit = true;
            Exit();
        }
        GameManager.Instance._UIManager.RefreshExitGauge(currentTime / CoolTime);
    }

    private void Exit()
    {
        GameManager.Instance.GameClearExit();
        GameManager.Instance._UIManager.RefreshExitGauge(0);
    }
    
}
