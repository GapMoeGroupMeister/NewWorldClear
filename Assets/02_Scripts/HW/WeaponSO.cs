using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    LongRange,
    ShortRange,
}
[CreateAssetMenu(menuName = "SO/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string name;
    public string desc;
    public Sprite weaponSprite;
    public float damage;
    public float attackDelay;
    public WeaponType weaponType;
    public Vector2 attackRange; //원거리의 경우엔 Vector2 값중 x값만 받아와 사용함.
}



