using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    [CanBeNull] public PlayerController _PlayerController { get; private set; }
    public SoundManager _SoundManager { get; private set; }


    public UnityEvent SceneStartEvent;
    [Space(20)]
    public UnityEvent GameExitEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent GameClearEvent;

    private void Awake()
    {
        _SoundManager = FindObjectOfType<SoundManager>();
        _PlayerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        SceneStartEvent?.Invoke();
    }

    
    public void GameForcedExit()
    {
        StopAll();
        GameExitEvent?.Invoke();
        GameOverEvent?.Invoke();
    }

    [ContextMenu("DebugGameClear")]
    public void GameClearExit()
    {
        StopAll();
        GameExitEvent?.Invoke();
        GameClearEvent?.Invoke();
    }
    
    


    private void StopAll()
    {
        if (FindObjectOfType<PlayerController>())
        {
            Destroy(_PlayerController.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    
    
    
}
