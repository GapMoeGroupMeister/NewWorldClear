using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Gun,
    Bow,
    Else
}
public enum AttackMotion
{
    Swing,
    Shake,
    Poke
}
[CreateAssetMenu(menuName = "SO/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string name;
    public int id;
    public GameObject weaponPrefab;
    public GameObject attackEffect;
    public float baseDamage;
    public float damage;
    public float attackDelay;
    public WeaponType weaponType = WeaponType.Else;
    public AttackMotion attackMotion;
    public Vector2 attackRange; //���Ÿ��� ��쿣 Vector2 ���� x���� �޾ƿ� �����.
}



