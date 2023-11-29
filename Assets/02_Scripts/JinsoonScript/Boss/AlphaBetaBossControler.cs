using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AlphaBetaBossControler : MonoBehaviour
{
    public AlphaBossBrain alpha { get; private set; }
    public BetaBossBrain beta { get; private set; }


    private void Awake()
    {
        alpha = GetComponentInChildren<AlphaBossBrain>();
        beta = GetComponentInChildren<BetaBossBrain>();
    }
}
