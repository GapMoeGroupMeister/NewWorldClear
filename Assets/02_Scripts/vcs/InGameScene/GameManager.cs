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
    public ItemGetRange _ItemGetRange { get; private set; }

    public UnityEvent SceneStartEvent;
    [Space(20)]
    public UnityEvent GameExitEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent GameClearEvent;
    [SerializeField] private GameObject DamageEvent;


    public bool isGameOver;
    public float Phase = 1f;

    private float playTime = 0;
    
    
    private void Awake()
    {
        _SoundManager = FindObjectOfType<SoundManager>();
        _UIManager = FindObjectOfType<InGameUIManager>();
        _PlayerController = FindObjectOfType<PlayerController>();
        _LevelManager = FindObjectOfType<LevelManager>();
        _ItemGetRange = _PlayerController.transform.Find("ItemGetRange").GetComponent<ItemGetRange>();

    }

    private void Start()
    {
        SceneStartEvent?.Invoke();
    }

    private void Update()
    {
        CountPlayTime();
        
        
    }

    public void GameForcedExit()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            StopAll();
            GameExitEvent?.Invoke();
            GameOverEvent?.Invoke();
        }
    }

    [ContextMenu("DebugGameClear")]
    public void GameClearExit()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            StopAll();
            GameExitEvent?.Invoke();
            GameClearEvent?.Invoke();
        }

    }
    
    


    private void StopAll()
    {
        if (FindObjectOfType<PlayerController>())
        {
            _PlayerController.isStun = true;
            //Destroy(_PlayerController.gameObject);
        }
    }

    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void CountPlayTime()
    {
        playTime += Time.deltaTime;
        Phase = 1 + (playTime * 0.01f);

    }

    public void DamageEffect(Vector2 pos, int damage, bool isCrit)
    {
        Transform damageEvent = PoolManager.Get(DamageEvent).transform;
        damageEvent.position = pos;
        damageEvent.GetComponent<DamageEvent>().ShowEvent(damage, isCrit);
    }
    
    
    
}
