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

    /**
     * <summary>
     * 무기 전부 바꾸기
     * </summary>
     */
    internal void WeaponChange()
    {
        foreach (var item in _weapons)
        {
            item.OnImageChanged();
        }
    }
}

[CustomEditor(typeof(WeaponMarketManager))]
class WeaponMarketManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WeaponMarketManager weaponMarketManager = target as WeaponMarketManager;
        GUILayout.Space(20f);
        if (GUILayout.Button("무기 변경"))
        {
            weaponMarketManager.WeaponChange();
        }
    }
}
