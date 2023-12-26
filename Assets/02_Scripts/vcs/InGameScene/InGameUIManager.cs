using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InGameUIManager : MonoBehaviour
{
    public UnityEvent EscEvent;
    public UIInfo UI_AudioSetting;
    private bool isEscLock;
    private bool OnAudioSetting;
    
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

    public void OnAudioSettingEventCheck()
    {
        OnAudioSetting = true;
    }

    public void OffAudioSettingEventCheck()
    {
        OnAudioSetting = false;
    }
        
}
