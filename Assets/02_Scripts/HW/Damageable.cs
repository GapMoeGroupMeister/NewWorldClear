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
    /// ��������� ���� enum.
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
    /// �������� ���� enum.
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
    /// Damageable�� ����� ��ü���� ���ϴ� ������ �ִ� �Լ�
    /// </summary>
    /// <param name="buff">���ϴ� ����</param>
    /// <param name="coolTime">������ ��Ÿ��</param>
    public void AddBuff(Buffs buff, float coolTime)
    {
        StopCoroutine("IE" + typeof(Buffs).GetEnumName(buff));
        StartCoroutine("IE" + typeof(Buffs).GetEnumName(buff), coolTime);
        buffs |= buff;
    }
    
    //�⺻��, ������ ��Ʈ�� �Ҷ��� �̳��� �⺻������ �ϸ��
    //���� �ż�, ����, ���� ���� �ֱ������� ȿ���� �� �ʿ䰡 ���ٸ� �ȿ��ִ� delay�� elasped�� �������� ����
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

    // ����� ������ �ƹ�ư �������� �ڵ尡
    // ������ ��
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