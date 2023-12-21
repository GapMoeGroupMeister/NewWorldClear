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
        return _mainMap.WorldToCell(worldPos); //월드 좌표를 넣으면 해당 월드 좌표의 타일맵으로 리턴한다.
    }

    public Vector3 GetWorldPosition(Vector3Int tilePos)
    {
        return _mainMap.GetCellCenterWorld(tilePos);
    }

    public bool CanMove(Vector3Int tilePos)
    {
        BoundsInt mapBound = _mainMap.cellBounds; //Compress시켰던 타일의 바운드가 나오게 된다.
        if (tilePos.x < mapBound.xMin || tilePos.x > mapBound.xMax || tilePos.y < mapBound.yMin || tilePos.y > mapBound.yMax)
        {
            return false;
        }

        return _collisionMap.GetTile(tilePos) is null;
    }
}
