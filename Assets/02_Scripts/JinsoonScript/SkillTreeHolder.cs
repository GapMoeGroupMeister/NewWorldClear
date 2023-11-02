using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeHolder : MonoBehaviour
{
    [SerializeField] private SkillTreeNode[,] skillTreeNodes;
    [SerializeField] private TextMeshProUGUI coinTmp = null;
    public int skillTreeCoin
    {
        get { return skillTreeCoin; }
        set { OnChangeCoinNum(); }
    }

    private void Awake()
    {
        coinTmp = GameObject.Find("SkillTreeCoinNumTxt").GetComponent<TextMeshProUGUI>();
        coinTmp.SetText(skillTreeCoin.ToString());
    }

    public void OnChangeCoinNum()
    {
        coinTmp.SetText(skillTreeCoin.ToString());
    }
}
