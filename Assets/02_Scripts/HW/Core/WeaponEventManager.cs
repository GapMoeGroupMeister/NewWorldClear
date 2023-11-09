using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEventManager : MonoBehaviour
{
    public static WeaponEventManager Instance;


    private void Awake()
    {
        Instance = this;
    }
}
