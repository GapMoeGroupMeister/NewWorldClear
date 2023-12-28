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
    public LevelManager _LevelManager { get; private set; }
    public InGameUIManager _UIManager { get; private set; }

    public UnityEvent SceneStartEvent;
    [Space(20)]
    public UnityEvent GameExitEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent GameClearEvent;

    private void Awake()
    {
        _SoundManager = FindObjectOfType<SoundManager>();
        _UIManager = FindObjectOfType<InGameUIManager>();
        _PlayerController = FindObjectOfType<PlayerController>();
        _LevelManager = FindObjectOfType<LevelManager>();

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
            //Destroy(_PlayerController.gameObject);
        }
    }

    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    
    
    
}
