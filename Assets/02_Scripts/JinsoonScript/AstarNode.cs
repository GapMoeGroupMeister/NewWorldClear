using System;
using UnityEngine;

public class AstarNode : IComparable<AstarNode>
{
    public Vector3Int pos; // Ÿ�ϸ� �������� Vector3Int�� ����ؼ� �̰� ���ž� 
    public AstarNode parent;
    public float G;
    public float F; //H�� �׳� ����ϸ�Ǵϱ�(�����Ÿ�)
    // F = G + H

    public int CompareTo(AstarNode other)
    {
        if (other.F == this.F) return 0;
        return other.F < this.F ? -1 : 1;
    }

    public override bool Equals(object obj) => this.Equals(obj as AstarNode);
    public override int GetHashCode() => pos.GetHashCode();

    public bool Equals(AstarNode other)
    {
        if (other is null || this.GetType() != other.GetType())
            return false;

        return pos == other.pos; //�������� ���� �ʴٸ� false, ���ٸ� true�� �����Ѵ�.
    }

    //������ �����Ǵ� �ݵ�� static �ż���� ����� �Ѵ�.
    public static bool operator ==(AstarNode lhs, AstarNode rhs)
    {
        if (lhs is null)
        {
            return rhs is null ? true : false;
        }

        return lhs.Equals(rhs);
    }

    public static bool operator !=(AstarNode lhs, AstarNode rhs)
    {
        return !(lhs == rhs);
    }
}
