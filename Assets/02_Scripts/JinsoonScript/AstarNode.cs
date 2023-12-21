using System;
using UnityEngine;

public class AstarNode : IComparable<AstarNode>
{
    public Vector3Int pos; // 타일맵 포지션은 Vector3Int를 사용해서 이걸 쓴거야 
    public AstarNode parent;
    public float G;
    public float F; //H는 그냥 계산하면되니까(직선거리)
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

        return pos == other.pos; //포지션이 같지 않다면 false, 같다면 true를 리턴한다.
    }

    //연산자 재정의는 반드시 static 매서드로 해줘야 한다.
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
