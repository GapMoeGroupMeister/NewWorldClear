using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneback : MonoBehaviour
{
    public void MoveStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
