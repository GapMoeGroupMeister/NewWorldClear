using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneMoveStorage()
    {
        SceneManager.LoadScene("Storage");
    }

    public void SceneMoveShop()
    {
        SceneManager.LoadScene("Leo_BlackMarket");
    }

    public void SceneMoveInGame()
    {
        SceneManager.LoadScene("InGameScene");
    }
}
