using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Tkfkadlsi;
using UnityEditor;


public class Storage_UIManager : MonoSingleton<Storage_UIManager>
{
    private Sequence seq;
    [SerializeField] private RectTransform UI_Description;

    [SerializeField] private TextMeshProUGUI Text_ItemName;

    [SerializeField] private TextMeshProUGUI Text_Description;
    [SerializeField] private Gradient DurabilityColorGradient;
    

    public void On_DescriptionUI()
    {
        seq = DOTween.Sequence();

        seq.Append(UI_Description.DOAnchorPosX(740f, 0.3f));

    }

    public void Off_DescriptionUI()
    {
        seq = DOTween.Sequence();

        seq.Append(UI_Description.DOAnchorPosX(1200f, 0.3f));

    }

    public void Refresh_DescriptionUI(Item _item)
    {
        Text_ItemName.text = _item.itemName;
        Text_Description.text = _item.description;
    }

    
}
