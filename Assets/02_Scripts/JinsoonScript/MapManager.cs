using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private Tilemap _mainMap ;
    private Tilemap _collisionMap ;
    [SerializeField]private Transform tileMapTrm;

    public static MapManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;

        _collisionMap = tileMapTrm.Find("Collision").GetComponent<Tilemap>();
        _mainMap = tileMapTrm.Find("Floor").GetComponent<Tilemap>();
        _mainMap.CompressBounds();
    }

    public Vector3Int GetTilePos(Vector3 worldPos)
    {
        return _mainMap.WorldToCell(worldPos); //���� ��ǥ�� ������ �ش� ���� ��ǥ�� Ÿ�ϸ����� �����Ѵ�.
    }

    public Vector3 GetWorldPosition(Vector3Int tilePos)
    {
        return _mainMap.GetCellCenterWorld(tilePos);
    }

    public bool CanMove(Vector3Int tilePos)
    {
        BoundsInt mapBound = _mainMap.cellBounds; //Compress���״� Ÿ���� �ٿ�尡 ������ �ȴ�.
        if (tilePos.x < mapBound.xMin || tilePos.x > mapBound.xMax || tilePos.y < mapBound.yMin || tilePos.y > mapBound.yMax)
        {
            return false;
        }

        return _collisionMap.GetTile(tilePos) is null;
    }
}
