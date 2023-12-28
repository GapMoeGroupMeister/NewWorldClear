using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InGameUIManager : MonoBehaviour
{
    public UnityEvent EscEvent;
    public UIInfo UI_AudioSetting;
    private bool isEscLock;
    private bool OnAudioSetting;
    [SerializeField] private Image HpGauge;

    [SerializeField] private GameObject ExitPanel;
    [SerializeField] private Image ExitGauge;
    
    public void OnEsc()
    {
        if (!isEscLock && !OnAudioSetting)
        {
            EscEvent?.Invoke();
            GameManager.Instance._SoundManager.SaveAudioSet();
        }
        else
        {
            if (OnAudioSetting)
            {
                UI_AudioSetting.MoveOff();
                OffAudioSettingEventCheck();
                EscEvent?.Invoke();
            }
        }
    }

    private void Update()
    {
        RefreshHpGauge();
    }

    public void OnAudioSettingEventCheck()
    {
        OnAudioSetting = true;
    }

    public void OffAudioSettingEventCheck()
    {
        OnAudioSetting = false;
    }

    private void RefreshHpGauge()
    {
        float t = PlayerController.Instance._currentHp / PlayerController.Instance._maxHp;
        t = Mathf.Clamp(t, 0f, 1f);
        HpGauge.fillAmount = t;
    }

    public void RefreshExitGauge(float t)
    {
        if (t == 0)
        {
            ExitPanel.SetActive(false);
        }
        else
        {
            ExitPanel.SetActive(true);
        }
        ExitGauge.fillAmount = Mathf.Clamp(t, 0f, 1f);
    }
        
}
