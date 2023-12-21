using System;
using System.Collections;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public float _currentHp = 100f;
    public float _maxHp = 100f;
    public float _moveSpeed;
    public float damage;
    public bool isStun = false;
    public bool isSubdue = false;
    public bool thinSheild = false;
    

    /// <summary>
    /// 디버프들을 담은 enum.
    /// </summary>
    public enum Debuffs
    {
        None = 0,
        Bleed = 1,
        Slow = 2,
        Stun = 4,
        Subdue = 8,
        Poison = 16
    }

    /// <summary>
    /// 버프들을 담은 enum.
    /// </summary>
    public enum Buffs
    {
        None = 0,
        Fast = 1,
        Generation = 2,
        PowerUp = 4,
        ThinSheild = 8,
    }

    public Buffs buffs = Buffs.None;
    public Debuffs debuffs = Debuffs.None;

    /// <summary>
    /// Damageable을 상속한 개체에게 원하는 버프를 주는 함수
    /// </summary>
    /// <param name="buff">원하는 버프</param>
    /// <param name="coolTime">버프의 쿨타임</param>
    /// 
    public virtual void HitDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    public virtual void BleedDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    public virtual void PoisonDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
 
        }
    }

    private void Die()
    {
        print("죽음");
    }

    public void AddBuff(Buffs buff, float coolTime)
    {
        if (buff == Buffs.None) return;
        buffs |= buff;
        StopCoroutine("IE" + typeof(Buffs).GetEnumName(buff));
        StartCoroutine("IE" + typeof(Buffs).GetEnumName(buff), coolTime);
    }

    public void AddDebuff(Debuffs debuff, float coolTime)
    {
        if (debuff == Debuffs.None) return;
        debuffs |= debuff;
        StopCoroutine("IE" + typeof(Debuffs).GetEnumName(debuff));
        StartCoroutine("IE" + typeof(Debuffs).GetEnumName(debuff), coolTime);
    }

    IEnumerator IEBleed(float coolTime)
    {
        float cool = coolTime;
        float delay = 0.5f;
        float elasped = 0f;
        while (cool > 0)
        {
            if ((debuffs &= Debuffs.Bleed) != Debuffs.Bleed)
                break;
            cool -= Time.deltaTime;
            elasped += Time.deltaTime;
            if (elasped >= delay)
            {
                BleedDamage(5f);
                elasped = 0;
            }
            yield return null;
        }
        debuffs -= Debuffs.Bleed;
    }

    IEnumerator IESlow(float coolTime)
    {
        float prevSpeed = _moveSpeed;
        float cool = coolTime;
        while (cool > 0)
        {
            if ((debuffs &= Debuffs.Slow) != Debuffs.Slow)
                break;
            float addSpeed = prevSpeed - 2;
            if (addSpeed != _moveSpeed) prevSpeed = _moveSpeed;
            _moveSpeed = addSpeed;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs -= Debuffs.Slow;
        _moveSpeed = prevSpeed;
    }

    IEnumerator IEStun(float coolTime)
    {
        float cool = coolTime;
        while (cool > 0)
        {
            if ((debuffs &= Debuffs.Stun) != Debuffs.Stun)
                break;
            isStun = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs-= Debuffs.Stun;
        isStun = false;
    }

    IEnumerator IESubdue(float coolTime)
    {
        float cool = coolTime;
        while (cool > 0)
        {
            if ((debuffs &= Debuffs.Subdue) != Debuffs.Subdue)
                break;
            isSubdue = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs -= Debuffs.Subdue;
        isSubdue = false;
    }

    IEnumerator IEPoison(float coolTime)
    {
        float cool = coolTime;
        float delay = 0.5f;
        float elasped = 0f;
        while (cool > 0)
        {
            if ((debuffs &= Debuffs.Poison) != Debuffs.Poison)
                break;
            cool -= Time.deltaTime;
            elasped += Time.deltaTime;
            if (elasped >= delay)
            {
                PoisonDamage(5f);
                elasped = 0;
            }
            yield return null;
        }
        debuffs -= Debuffs.Poison;
    }

    #region Buff Coroutines
    IEnumerator IEFast(float coolTime)
    {
        float prevSpeed = _moveSpeed;
        float cool = coolTime;
        while (cool > 0)
        {
            if ((buffs &= Buffs.Fast) != Buffs.Fast)
                break;
            float addSpeed = prevSpeed + 3;
            if (addSpeed != _moveSpeed) prevSpeed = _moveSpeed;
            _moveSpeed = addSpeed;
            print(_moveSpeed);
            cool -= Time.deltaTime;
            yield return null;
        }
        buffs -= Buffs.Fast;
        _moveSpeed = prevSpeed;
    }

    IEnumerator IEGeneration (float coolTime)
    {
        float cool = coolTime;
        float delay = 0.5f;
        float elasped = 0f;
        while(cool > 0)
        {
            if ((buffs &= Buffs.Generation) != Buffs.Generation)
                break;
            cool -= Time.deltaTime;
            elasped += Time.deltaTime;
            if(elasped >= delay)
            {
                _currentHp += 5f;
                elasped = 0;
            }
            yield return null;
        }
        buffs -= Buffs.Generation;
    }

    IEnumerator IEPowerUp(float coolTime)
    {
        float prevdamage = damage;
        float cool = coolTime;
        while (cool > 0)
        {
            if ((buffs &= Buffs.PowerUp) != Buffs.PowerUp)
                break;
            float addDamage = prevdamage + (prevdamage * 0.5f);
            if (addDamage != damage) prevdamage = damage;
            damage = addDamage;
            cool -= Time.deltaTime;
            yield return null;
        }
        buffs -= Buffs.PowerUp;
        damage = prevdamage;
    }

    IEnumerator IEThinSheld(float coolTime)
    {
        float cool = coolTime;
        while (cool > 0)
        {
            if ((buffs &= Buffs.ThinSheild) != Buffs.ThinSheild)
                break;
            thinSheild = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        buffs -= Buffs.ThinSheild;
        thinSheild = false;
    }
    #endregion
}