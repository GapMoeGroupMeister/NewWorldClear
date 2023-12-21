using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NavAgent : MonoBehaviour
{
    private PriorityQueue<AstarNode> openList; //�̰� ���� �� �� �ִ� �͵��� ��Ƴ��� ��
    private List<AstarNode> closeList; //�̰� ���� �ѹ��̶� �湮�� ������ ��Ƴ��� ��

    private List<Vector3Int> routePath; //���� ������ ��θ� Ÿ�� ���������� ������ �ִ� ��

    [SerializeField] private bool cornerCheck = false; //�ڳʸ� ���� üũ���� ����

    private int moveIdx = 0; //���Ʈ ����� ���° ��带 ���� ���� �ִ°�?

    private Vector3Int currentPos; //���� Ÿ�� ������
    private Vector3Int destinationPos; //��ǥ Ÿ�� ������

    public Vector3Int Destination
    {
        get => destinationPos;
        set
        {
            if (destinationPos == value) return; //�������� ������ �ʾҴٸ� ����
            SetCurrentPosition(); //�� ���� ��ġ�� �������� �����ϰ�
            destinationPos = value;
            CalculatePath(); //����� ��� ������ְ�
        }
    }

    public bool GetNextPath(out Vector3Int nextPos)
    {
        nextPos = new Vector3Int();

        if (routePath.Count == 0 || moveIdx >= routePath.Count) return false;

        nextPos = routePath[moveIdx++];
        return true;
    }

    private void Awake()
    {
        closeList = new List<AstarNode>();
        routePath = new List<Vector3Int>();
        openList = new PriorityQueue<AstarNode>();
    }

    private void Start()
    {
        SetCurrentPosition();
        transform.position = MapManager.Instance.GetWorldPosition(currentPos); //���� Ÿ����ǥ�� �߽����� �̵�
    }

    private void SetCurrentPosition()
    {
        currentPos = MapManager.Instance.GetTilePos(transform.position);
    }

    #region Astar �˰���

    private void CalculatePath()
    {
        openList.Clear();
        closeList.Clear();

        openList.Push(new AstarNode
        {
            pos = currentPos,
            parent = null,
            G = 0,
            F = CalculateH(currentPos)
        });

        bool result = false; //��θ� ã�Ҵ°�?

        int cnt = 0;
        while (openList.Count > 0)
        {
            AstarNode node = openList.Pop(); //�켱���� ť�� ������������ϱ� ���� �켱�� �Ǵ� �ְ� ��������.
            FindOpenList(node);
            closeList.Add(node); //node�� �ѹ� ��Ծ����� �ٽ� ������ closeList�� ���� �Ѵ�.
            if (node.pos == destinationPos) //�湮 ��尡 ���������ٸ� �� �°Ŵϱ� break �����.
            {
                result = true;
                break;
            }

            cnt++;
            if (cnt > 200)  //�ִ� �󸶳� ã�� �ǰ�?
            {
                break;
            }
        }

        if (result)
        {
            routePath.Clear();
            AstarNode node = closeList.Last();
            while (node.parent != null)
            {
                routePath.Add(node.pos);
                node = node.parent; //�θ�ã�Ƽ� �ö󰣴�. 
            }
            routePath.Reverse(); //�������� �����´�.
            moveIdx = 0;
        }

    }

    private void FindOpenList(AstarNode node)
    {
        //�������� �Ͼ���� �𸣰ڴµ� ��·�� �̰� �̷��� ���� �˾Ƽ� �ڵ����ٲ���.
        //��ư �̰� ������ _openList���� �� �� �ִ� ���� �� �� ��������.
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == 0 && x == y) continue; //�̰� ���� �� �ڸ��ϱ� ����

                Vector3Int nextPos = node.pos + new Vector3Int(x, y, 0); //�˻��� ��ǥ�� nextPos�� �����.

                AstarNode temp = closeList.Find(node => node.pos == nextPos);
                if (temp != null) continue; //���� �̹� �ѹ� �������̴ϱ� �� �ʿ䰡 ����.

                if (!MapManager.Instance.CanMove(nextPos)) continue; //�ʿ��� �� �� ���� ���̴ϱ� ����

                //������� ������ ���� �� �� �ִ� ���̴ϱ� ����ؼ� ���¸���Ʈ�� �־�����.
                float g = (node.pos - nextPos).magnitude + node.G;
                AstarNode nextOpenNode = new AstarNode
                {
                    pos = nextPos,
                    parent = node,
                    G = g,
                    F = g + CalculateH(nextPos)
                };

                AstarNode exist = openList.Contains(nextOpenNode); //���� ���� ������ �ϴ°� �̹� ������ ���¸���Ʈ�� �־����?
                if (exist != null)
                {
                    if (nextOpenNode.G < exist.G)
                    {
                        exist.G = nextOpenNode.G;
                        exist.F = nextOpenNode.F;
                        exist.parent = nextOpenNode.parent;

                        openList.Recalculation(exist); //�ٽ� ����ؼ� ������ �����ְ� ���� ������ �����.
                    }
                }
                else
                {
                    openList.Push(nextOpenNode);
                }
            }
        }
    }

    private float CalculateH(Vector3Int pos)
    {
        return (destinationPos - pos).magnitude;
    }
    #endregion
    //�����٤Ф�
}