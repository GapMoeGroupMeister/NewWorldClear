using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlphaState
{

}

public class AlphaBossBrain : MonoBehaviour
{
    //나중에 풀링해 주는 걸로ㅇㅋ?
    private AlphaBossMove alphaMove = null;
    [SerializeField] private GameObject trap = null;
    [SerializeField] private GameObject circleWarning = null;
    private Animator anim = null;

    
    public bool isUsingSkill { get; private set; }





    public void AmbushTrapPlacement()
    {
        //풀링으로 trap소환하는 걸로 하셈
        trap = Instantiate(trap);
        //대충 애니메이션 실행시키고 그기서 함수로 실행
        trap.GetComponent<AlphaBossTrap>().Init(transform.position);
    }

    public void ChemicalHeavyWeapon()
    {

    }

    public void VeilOfSilence()
    {

    }
}
