using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEvent : MonoBehaviour
{
    public abstract void OnHit(Transform enemy);
    public abstract void Passive();
    public abstract void Skill(Transform enemy);
}

