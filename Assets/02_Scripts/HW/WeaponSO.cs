using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Gun,
    Bow,
    Swing
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
    public GameObject weaponPrefab;
    public float damage;
    public float attackDelay;
    public WeaponType weaponType;
    public AttackMotion attackMotion;
    public Vector2 attackRange; //���Ÿ��� ��쿣 Vector2 ���� x���� �޾ƿ� �����.
}



