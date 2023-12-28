using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapSO")]
public class MapSO : ScriptableObject
{
    public string mapName;
    [TextArea(3, 10)]
    public string mapDescription;
}
