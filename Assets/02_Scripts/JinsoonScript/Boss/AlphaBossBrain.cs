using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlphaState
{

}

public class AlphaBossBrain : MonoBehaviour
{
    //���߿� Ǯ���� �ִ� �ɷΤ���?
    private AlphaBossMove alphaMove = null;
    [SerializeField] private GameObject trap = null;
    [SerializeField] private GameObject circleWarning = null;
    private Animator anim = null;

    
    public bool isUsingSkill { get; private set; }





    public void AmbushTrapPlacement()
    {
        //Ǯ������ trap��ȯ�ϴ� �ɷ� �ϼ�
        trap = Instantiate(trap);
        //���� �ִϸ��̼� �����Ű�� �ױ⼭ �Լ��� ����
        trap.GetComponent<AlphaBossTrap>().Init(transform.position);
    }

    public void ChemicalHeavyWeapon()
    {

    }

    public void VeilOfSilence()
    {

    }
}
