using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _destroyed = false;
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_destroyed)
            {
                return null;
            }


            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    private void OnApplicationQuit()
    {
        _destroyed = true;
    }

    private void OnDestroy()
    {
        _destroyed = true;
    }
}
