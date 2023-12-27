using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource _audioSource;
    
    public AudioClip[] clips;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 0 : Null (Nothing)
    /// 1 : FunkShop
    /// 2 : SF_Silent
    /// </summary>
    public void BGMChange(int index, bool fade = false, float duration = 0)
    {
        if(fade)
        {
            _audioSource.DOFade(0, duration).OnComplete(() =>
            {
                _audioSource.clip = clips[index];
                _audioSource.Play();
                _audioSource.DOFade(1, duration);
            });
            return;
        }
        _audioSource.clip = clips[index];
        _audioSource.Play();
    }
}
