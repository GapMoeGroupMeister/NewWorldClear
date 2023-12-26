using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AlphaBossBrain : MonoBehaviour
{
    private Sequence seq;
    //나중에 풀링해 주는 걸로ㅇㅋ?
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
