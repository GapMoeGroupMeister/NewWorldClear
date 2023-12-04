using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponMarketManager : MonoBehaviour
{
    private List<Weapon2> _weapons = new List<Weapon2>();

    private void Awake()
    {
        _weapons.AddRange(FindObjectsOfType<Weapon2>());
    }

    private void OnEnable()
    {
        WeaponChange();
    }

    public void WeaponChange()
    {
        foreach (var item in _weapons)
        {
            item.OnImageChanged();
        }
    }
}

