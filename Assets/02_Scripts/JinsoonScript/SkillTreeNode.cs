using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class SkillTreeNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public bool nodeSelected;
    public SkillTreeNode lastSkillTreeNode;
    public UnityEvent selectedAction;

    private SkillTreeHolder skillTreeHolder = null;
    private Transform connecterTrm = null;
    private Image connecter = null;
    private Transform selectedTrm = null;
    private Image framImage = null;

    private float maxProgress = 50;
    private float curProgress = 0f;

    private bool isLoading = false;
    private bool isLoadingCancel = false;
    private bool isSelected = false;
    private bool isChecking = false;

    [SerializeField] private int requireCoin = 1;

    public bool IsSelected => isSelected;

    public int index { get; private set; }

    private void Awake()
    {
        skillTreeHolder = GetComponentInParent<SkillTreeHolder>();
        connecterTrm = transform.Find("connecter");
        connecter = connecterTrm.Find("connecterFill").GetComponent<Image>();
        framImage = transform.Find("Frame").GetComponent<Image>();
        selectedTrm = transform.Find("Selected");

        RectTransform connecterRectTrm = connecterTrm.GetComponent<RectTransform>();
        Vector3 connecterPos = connecterRectTrm.position;

        connecterTrm.parent = transform.root.Find("SkillTreeBackground/Viewport/Content/SkillTreeNodeHolder");
        connecterTrm.SetAsFirstSibling();
        connecterRectTrm.position = connecterPos;

        selectedTrm.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (isSelected)
        {
            connecter.fillAmount = 1;
            framImage.fillAmount = 1;
            selectedTrm.gameObject.SetActive(true);
            selectedTrm.localScale = new Vector3(1, 1, 1);

            return;
        }

        if(lastSkillTreeNode == null|| lastSkillTreeNode.IsSelected == true)
        {
            if (isLoading == true)
            {
                curProgress += Time.deltaTime * 200;

                if (curProgress - maxProgress / 2 >= maxProgress)
                {
                    selectedTrm.DOScale(1, 0.2f);
                    isLoading = false;
                    isSelected = true;
                    skillTreeHolder.GetSkillTreeCoin(-requireCoin);
                    skillTreeHolder.JsonSave();
                    selectedAction?.Invoke();
                }
            }
        }
        

        if (isLoadingCancel == true)
        {
            curProgress -= Time.deltaTime * 300;

            if (curProgress <= 0)
            {
                curProgress = 0f;
                isLoadingCancel = false;
            }
        }

        if (isChecking == false)
        {
            connecter.fillAmount = curProgress / (maxProgress / 2);
            framImage.fillAmount = (curProgress - maxProgress / 2) / maxProgress;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillTreeHolder.CheckAll(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        skillTreeHolder.CheckCancelAll(this);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        skillTreeHolder.LoadAll(this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        skillTreeHolder.LoadCancelAll(this);
    }


    public void OnCheck()
    {
        if (isLoading || isSelected || isLoadingCancel) return;
        isChecking = true;
        framImage.fillAmount = 1f;
        connecter.fillAmount = 1f;
    }
    public void OnEndCheck()
    {
        isLoading = false;

        if (isSelected || isLoadingCancel)
        {
            return;
        }
        else
        {
            isLoadingCancel = true;
        }

        isChecking = false;
        framImage.fillAmount = 0f;
        connecter.fillAmount = 0f;
    }

    public void Load()
    {
        if (skillTreeHolder.SkillTreeCoin < requireCoin) return;
        isChecking = false;
        isLoading = true;
        isLoadingCancel = false;
    }
    public void CancelLoad()
    {
        isLoading = false;
        isLoadingCancel = true;
    }

    public void Init(int index, bool isSelected)
    {
        this.index = index;
        this.isSelected = isSelected;
    }
}
