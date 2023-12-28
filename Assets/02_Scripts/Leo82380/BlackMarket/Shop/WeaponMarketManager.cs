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

    private void OnDisable()
    {
        SaveInfo saveInfo = DBManager.Get_UserInfo();
        saveInfo.nowDay = saveInfo.day;
        DBManager.Save_userInfo(saveInfo);
    }

    private void OnEnable()
    {
        SaveInfo saveInfo = DBManager.Get_UserInfo();
        if (saveInfo.nowDay != saveInfo.day)
        {
            ItemChange();
        }
    }

    /**
     * <summary>
     * 무기 전부 바꾸기
     * </summary>
     */
    public void ItemChange()
    {
        foreach (var item in _weapons)
        {
            item.OnImageChanged();
        }
    }
}

