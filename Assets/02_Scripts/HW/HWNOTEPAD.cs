using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWNOTEPAD : MonoBehaviour
{
    private enum Debuff
    {
        None = 0,
        Bleed = 1,
        Stun = 2,
        Poison = 4,
        Slow = 8,
    }

    int _currnetDebuff = 0;

    private void Update()
    {
        _currnetDebuff = _currnetDebuff | (int)Debuff.Stun;

        if((int)Debuff.Stun == (_currnetDebuff & (int)Debuff.Stun))
        {
            Debug.Log("기저ㄹ기절ㅈㅣㄱㅓㄹ");
        }
    }
}
