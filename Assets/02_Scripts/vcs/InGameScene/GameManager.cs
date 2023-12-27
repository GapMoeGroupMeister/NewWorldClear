using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{

    public SoundManager _SoundManager { get; private set; }


    public UnityEvent SceneStartEvent;

    private void Awake()
    {
        _SoundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        SceneStartEvent?.Invoke();
    }



    public void GameClear()
    {
        PlayerController.Instance.isStun = true;
        
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
