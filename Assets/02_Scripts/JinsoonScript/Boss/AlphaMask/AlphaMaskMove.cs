using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AlphaMaskMove : MonoBehaviour
{
    private AlphaMaskBrain brain;
    private NavAgent agent;

    private Vector3Int nextPosition;
    public UnityEvent<Vector2> onMove;

    private void Awake()
    {
        agent = GetComponent<NavAgent>();
        brain = GetComponent<AlphaMaskBrain>();
    }


    public void MoveTo(Vector3 targetPosition)
    {
        Vector3Int destination = MapManager.Instance.GetTilePos(targetPosition);
        agent.Destination = destination; //경로 설정을 시작할 거고

        Vector3 nextWorld = MapManager.Instance.GetWorldPosition(nextPosition);

        if (Vector3.Distance(nextWorld, transform.position) < 0.2f)
        {
            if (agent.GetNextPath(out Vector3Int next)) nextPosition = next;
            else onMove?.Invoke(Vector2.zero);
        }
        else
        {
            Vector2 dir = (nextWorld - transform.position).normalized;
            onMove?.Invoke(dir);
            //적이 바라보는 방향
        }
    }
}
