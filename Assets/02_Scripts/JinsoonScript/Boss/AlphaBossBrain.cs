using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AlphaBossBrain : MonoBehaviour
{
    private Sequence seq;
    //���߿� Ǯ���� �ִ� �ɷΤ���?
    private AlphaBossMove alphaMove = null;
    private Animator anim = null;

    private RectTransform bossHpGauge;
    private Image hpFill;
    private Image hpFillBack;
    
    public bool isUsingSkill { get; private set; }




    public void StartBoss()
    {
        bossHpGauge.DOAnchorPosY(0, 0.5f);
    }
}
