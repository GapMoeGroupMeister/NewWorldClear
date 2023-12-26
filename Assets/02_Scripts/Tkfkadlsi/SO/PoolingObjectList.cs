using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolingObjectList")]
public class PoolingObjectList : ScriptableObject
{
    public List<PoolingObject> poolingObjects = new List<PoolingObject>();
}
