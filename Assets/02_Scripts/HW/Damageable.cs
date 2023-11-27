using System.Collections;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    protected float _currentHp;
    protected float _maxHp;
    protected float _moveSpeed;
    public abstract void HitDamage(float damage);
    public abstract void BleedDamage(float damage);
    public abstract void PoisonDamage(float damage);

    /// <summary>
    /// 디버프들을 담은 enum.
    /// </summary>
    public enum Debuffs
    {
        None = 0,
        Slow = 1,
        Stun = 2,
        Subdue = 4,
        Poison = 8
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
    public void AddBuff(Buffs buff, float coolTime)
    {
        StopCoroutine("IE" + typeof(Buffs).GetEnumName(buff));
        StartCoroutine("IE" + typeof(Buffs).GetEnumName(buff), coolTime);
        buffs |= buff;
    }
    
    //기본형, 버프를 컨트롤 할때는 이놈을 기본형으로 하면됨
    //만약 신속, 구속, 스턴 같이 주기적으로 효과를 줄 필요가 없다면 안에있는 delay와 elasped는 빼버려도 무방
    IEnumerator IEDefault(float coolTime)
    {
        float cool = coolTime;
        float delay = 0.5f;
        float elasped = 0f;
        while (cool <= 0)
        {
            if ((buffs &= Buffs.Generation) != Buffs.Generation)
                break;
            cool -= Time.deltaTime;
            elasped += Time.deltaTime;
            if (elasped >= delay)
            {
                _currentHp += 5f;
            }
            yield return null;
        }
        buffs -= Buffs.Generation;
    }

    IEnumerator IEFast(float coolTime)
    {
        float cool = coolTime;
        while (cool <= 0)
        {
            if ((buffs &= Buffs.Fast) != Buffs.Fast)
                break;
            cool -= Time.deltaTime;
            yield return null;
        }
        buffs -= Buffs.Fast;
    }

    // 대안은 없지만 아무튼 ㅈㄴ구림 코드가
    // 이유는 모름
    IEnumerator IEGeneration (float coolTime)
    {
        float cool = coolTime;
        float delay = 0.5f;
        float elasped = 0f;
        while(cool <= 0)
        {
            if ((buffs &= Buffs.Generation) != Buffs.Generation)
                break;
            cool -= Time.deltaTime;
            elasped += Time.deltaTime;
            if(elasped >= delay)
            {
                _currentHp += 5f;
            }
            yield return null;
        }
        buffs -= Buffs.Generation;
    }
}