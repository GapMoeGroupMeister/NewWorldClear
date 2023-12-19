using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorageSceneManager : MonoBehaviour
{
    [SerializeField] private Storage_InventoryManager _InventoryManager;

    public void SceneExit()
    {
        _InventoryManager.SceneExit();
        SceneManager.LoadScene("StartScene");
    }
}
