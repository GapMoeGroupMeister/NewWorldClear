using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEvent : MonoBehaviour
{
    //위랑 똑같은 변수들

    public abstract void OnHit(Transform enemy);
    public abstract void Passive();
    public abstract void Skill(Transform enemy);
}

