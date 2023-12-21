using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;

public class SkillTreeNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public bool nodeSelected;
    public SkillTreeNode lastSkillTreeNode;
    public UnityEvent selectedAction;

    private SkillTreeHolder skillTreeHolder;
    private Transform connecterTrm;
    private Image connecter;
    private Transform selectedTrm;
    private Transform text;
    private Image framImage;

    private float maxProgress = 50;
    private float curProgress = 0f;

    private bool isLoading = false;
    private bool isLoadingCancel = false;
    private bool isSelected = false;
    private bool isChecking = false;

    [SerializeField] private int requireCoin = 1;

    public bool IsSelected => isSelected;

    public int index { get; private set; }
    public int RequireCoin => requireCoin;

    private void Awake()
    {
        skillTreeHolder = GetComponentInParent<SkillTreeHolder>();
        connecterTrm = transform.Find("connecter");
        connecter = connecterTrm.Find("connecterFill").GetComponent<Image>();
        framImage = transform.Find("Frame").GetComponent<Image>();
        selectedTrm = transform.Find("Selected");
        text = transform.Find("Text");

        Image[] image = text.GetComponentsInChildren<Image>();
        TextMeshProUGUI txt = text.GetComponentInChildren<TextMeshProUGUI>();

        foreach (Image i in image)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        }
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);

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

        if (lastSkillTreeNode == null || lastSkillTreeNode.IsSelected == true)
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


    private void TextOn()
    {
        Sequence seq = DOTween.Sequence();

        Image[] image = text.GetComponentsInChildren<Image>();
        TextMeshProUGUI txt = text.GetComponentInChildren<TextMeshProUGUI>();

        foreach(Image i in image)
        {
            seq.Join(i.DOFade(1, 0.5f));
        }
        seq.Join(txt.DOFade(1, 0.5f));
    }
    private void TextOff()
    {
        Sequence seq = DOTween.Sequence();

        Image[] image = text.GetComponentsInChildren<Image>();
        TextMeshProUGUI txt = text.GetComponentInChildren<TextMeshProUGUI>();

        foreach (Image i in image)
        {
            seq.Join(i.DOFade(0, 0.5f));
        }
        seq.Join(txt.DOFade(0, 0.5f));
    }


    public void OnCheck()
    {
        if (isLoading || isSelected || isLoadingCancel) return;
        isChecking = true;
        framImage.fillAmount = 1f;
        connecter.fillAmount = 1f;
        TextOn();
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
        TextOff();
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
