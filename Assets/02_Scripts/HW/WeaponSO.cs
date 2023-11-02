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
    public Vector2 attackRange;
}
