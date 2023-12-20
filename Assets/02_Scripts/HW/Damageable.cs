using System;
using System.Collections;
using UnityEngine;

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
    Poison = 16,
    Painful = 32
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


public abstract class Damageable : MonoBehaviour
{
    protected float _currentHp = 100f;
    protected float _maxHp = 100f;
    [SerializeField]
    protected float _moveSpeed;
    public float damage;
    public bool isStun = false;
    public bool isPainful = false;
    public bool isSubdue = false;
    public bool thinSheild = false;




    public Buffs buffs = Buffs.None;
    public Debuffs debuffs = Debuffs.None;
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
    public virtual void CriticalDamage(float damage, float percent)
    {
        _currentHp -= damage * (100 / percent);
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("죽음");
    }
    #region Buff & Debuff
    /// <summary>
    /// 가독성을 ㅈ박았을수도 있지만 amount는 각 버프와 디버프에 따라 다르게 작용한다. 신속이나 구속같은 경우엔 amount가 감소,증가하는 %로 작용하고 다른것은 미정이다. 알아서해라
    /// 기본값은 0이다.
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="coolTime"></param>
    /// <param name="amount"></param>
    public void AddBuff(Buffs buff, float coolTime, float amount = 0)
    {
        if (buff == Buffs.None) return;
        buffs |= buff;
        StopCoroutine("IE" + typeof(Buffs).GetEnumName(buff));
        StartCoroutine("IE" + typeof(Buffs).GetEnumName(buff), new float[] { coolTime, amount });
    }

    public void AddDebuff(Debuffs debuff, float coolTime, float amount = 0)
    {
        if (debuff == Debuffs.None) return;
        debuffs |= debuff;
        StopCoroutine("IE" + typeof(Debuffs).GetEnumName(debuff));
        StartCoroutine("IE" + typeof(Debuffs).GetEnumName(debuff), new float[] { coolTime, amount });
    }

    IEnumerator IEBleed(float[] values)
    {
        float cool = values[0];
        float delay = 0.5f;
        float elasped = 0f;
        while (cool > 0)
        {
            if (cool == Mathf.Infinity)
            {
                yield return null;
                continue;
            }
            if ((debuffs &= Debuffs.Bleed) != Debuffs.Bleed && cool != Mathf.Infinity)
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

    IEnumerator IESlow(float[] values)
    {
        float prevSpeed = _moveSpeed;
        float cool = values[0];
        while (cool > 0)
        {
            if (cool == Mathf.Infinity)
            {
                yield return null;
                continue;
            }
            if ((debuffs &= Debuffs.Slow) != Debuffs.Slow)
                break;
            float addSpeed = prevSpeed * (100 / values[1]);
            if (addSpeed != _moveSpeed) prevSpeed = _moveSpeed;
            _moveSpeed = addSpeed;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs -= Debuffs.Slow;
        _moveSpeed = prevSpeed;
    }

    IEnumerator IEStun(float[] values)
    {
        float cool = values[0];
        while (cool > 0)
        {
            if (cool == Mathf.Infinity)
            {
                yield return null;
                continue;
            }
            if ((debuffs &= Debuffs.Stun) != Debuffs.Stun)
                break;
            isStun = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs -= Debuffs.Stun;
        isStun = false;
    }

    IEnumerator IESubdue(float[] values)
    {
        float cool = values[0];
        while (cool > 0)
        {
            if (cool == Mathf.Infinity)
            {
                yield return null;
                continue;
            }
            if ((debuffs &= Debuffs.Subdue) != Debuffs.Subdue)
                break;
            isSubdue = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs -= Debuffs.Subdue;
        isSubdue = false;
    }

    IEnumerator IEPoison(float[] values)
    {
        float cool = values[0];
        float delay = 0.5f;
        float elasped = 0f;
        while (cool > 0)
        {
            if (cool == Mathf.Infinity)
            {
                yield return null;
                continue;
            }
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

    IEnumerator IEPainful(float[] values)
    {
        float cool = values[0];
        while (cool > 0)
        {
            if (cool == Mathf.Infinity)
            {
                yield return null;
                continue;
            }
            if ((debuffs &= Debuffs.Painful) != Debuffs.Painful)
                break;
            isPainful = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        debuffs -= Debuffs.Painful;
        isPainful = false;
    }

    #region Buff Coroutines
    IEnumerator IEFast(float[] values)
    {
        float prevSpeed = _moveSpeed;
        float cool = values[0];
        while (cool > 0)
        {
            if ((buffs &= Buffs.Fast) != Buffs.Fast && cool != Mathf.Infinity)
                break;
            float addSpeed = prevSpeed + (prevSpeed * (100 / values[1]));
            if (addSpeed != _moveSpeed) prevSpeed = _moveSpeed;
            _moveSpeed = addSpeed;
            cool -= Time.deltaTime;
            yield return null;
        }
        buffs -= Buffs.Fast;
        _moveSpeed = prevSpeed;
    }

    IEnumerator IEGeneration(float[] values)
    {
        float cool = values[0];
        float delay = 0.5f;
        float elasped = 0f;
        while (cool > 0)
        {
            if ((buffs &= Buffs.Generation) != Buffs.Generation && cool != Mathf.Infinity)
                break;
            cool -= Time.deltaTime;
            elasped += Time.deltaTime;
            if (elasped >= delay)
            {
                _currentHp += 5f;
                elasped = 0;
            }
            yield return null;
        }
        buffs -= Buffs.Generation;
    }

    IEnumerator IEPowerUp(float[] values)
    {
        float prevdamage = damage;
        float cool = values[0];
        while (cool > 0)
        {
            if ((buffs &= Buffs.PowerUp) != Buffs.PowerUp && cool != Mathf.Infinity)
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

    IEnumerator IEThinSheld(float[] values)
    {
        float cool = values[0];
        while (cool > 0)
        {
            if ((buffs &= Buffs.ThinSheild) != Buffs.ThinSheild && cool != Mathf.Infinity)
                break;
            thinSheild = true;
            cool -= Time.deltaTime;
            yield return null;
        }
        buffs -= Buffs.ThinSheild;
        thinSheild = false;
    }
    #endregion
    #endregion
}