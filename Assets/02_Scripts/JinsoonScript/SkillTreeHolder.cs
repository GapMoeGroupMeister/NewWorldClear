using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;

public class SkillTreeHolder : MonoBehaviour
{
    private string path = "";
    [SerializeField] private SkillTreeNode[,] skillTreeNodes;
    [SerializeField] private TextMeshProUGUI coinTmp = null;

    public int SkillTreeCoin { get; private set; }


    private void Awake()
    {
        path = Path.Combine(Application.dataPath, "Saves/SkillTree.json");

        coinTmp = GameObject.Find("SkillTreeCoinNumTxt").GetComponent<TextMeshProUGUI>();
        coinTmp.SetText(SkillTreeCoin.ToString());

        for (int i = 0; i < transform.childCount; i++)
        {
            SkillTreeNode node = transform.GetChild(i).GetComponent<SkillTreeNode>();
        }

        JsonLoad();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetSkillTreeCoin(1);
        }
    }

    public void GetSkillTreeCoin(int value)
    {
        SkillTreeCoin += value;
        Debug.Log(SkillTreeCoin);
        coinTmp.SetText(SkillTreeCoin.ToString());
        JsonSave();
    }

    public void CheckAll(SkillTreeNode skillTreeNode)
    {
        Queue<SkillTreeNode> skillTrees = new Queue<SkillTreeNode>();
        SkillTreeNode curSkillTreeNode = skillTreeNode;
        skillTrees.Enqueue(curSkillTreeNode);

        while (curSkillTreeNode.lastSkillTreeNode != null)
        {
            curSkillTreeNode = curSkillTreeNode.lastSkillTreeNode;
            skillTrees.Enqueue(curSkillTreeNode);
        }
        skillTrees.Reverse();

        while (skillTrees.TryDequeue(out SkillTreeNode node))
        {
            node.OnCheck();
        }
    }
    public void CheckCancelAll(SkillTreeNode skillTreeNode)
    {
        Queue<SkillTreeNode> skillTrees = new Queue<SkillTreeNode>();
        SkillTreeNode curSkillTreeNode = skillTreeNode;
        skillTrees.Enqueue(curSkillTreeNode);

        while (curSkillTreeNode.lastSkillTreeNode != null)
        {
            curSkillTreeNode = curSkillTreeNode.lastSkillTreeNode;
            skillTrees.Enqueue(curSkillTreeNode);
        }
        skillTrees.Reverse();

        while (skillTrees.TryDequeue(out SkillTreeNode node))
        {
            node.OnEndCheck();
        }
    }

    public void LoadAll(SkillTreeNode skillTreeNode)
    {
        Queue<SkillTreeNode> skillTrees = new Queue<SkillTreeNode>();
        SkillTreeNode curSkillTreeNode = skillTreeNode;
        skillTrees.Enqueue(curSkillTreeNode);

        while (curSkillTreeNode.lastSkillTreeNode != null)
        {
            curSkillTreeNode = curSkillTreeNode.lastSkillTreeNode;
            skillTrees.Enqueue(curSkillTreeNode);
        }
        skillTrees.Reverse();

        while (skillTrees.TryDequeue(out SkillTreeNode node))
        {
            Debug.Log(node);
            node.Load();
        }
    }

    public void LoadCancelAll(SkillTreeNode skillTreeNode)
    {
        Queue<SkillTreeNode> skillTrees = new Queue<SkillTreeNode>();
        SkillTreeNode curSkillTreeNode = skillTreeNode;
        skillTrees.Enqueue(curSkillTreeNode);

        while (curSkillTreeNode.lastSkillTreeNode != null)
        {
            curSkillTreeNode = curSkillTreeNode.lastSkillTreeNode;
            skillTrees.Enqueue(curSkillTreeNode);
        }
        skillTrees.Reverse();

        while (skillTrees.TryDequeue(out SkillTreeNode node))
        {
            node.CancelLoad();
        }
    }


    public void JsonSave()
    {
        SKillTreeSave save = new SKillTreeSave();

        save.coinNum = SkillTreeCoin;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<SkillTreeNode>(out SkillTreeNode s))
            {
                save.isSelected.Add(s.IsSelected);
            }
        }

        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(path, json);
    }

    public void JsonLoad()
    {
        SKillTreeSave save = new SKillTreeSave();

        if (!File.Exists(path))
        {
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);

            save = JsonUtility.FromJson<SKillTreeSave>(loadJson);

            int num = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<SkillTreeNode>(out SkillTreeNode s))
                {
                    s.Init(i - num, save.isSelected[i - num]);
                }
                else
                {
                    num++;
                }
            }
            SkillTreeCoin = save.coinNum;
            coinTmp.SetText(SkillTreeCoin.ToString());
        }
    }

    public class SKillTreeSave
    {
        public int coinNum = 0;
        public List<bool> isSelected = new List<bool>();
    }
}
