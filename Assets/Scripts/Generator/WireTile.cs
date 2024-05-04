using System;

using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class WireTile : ScriptableObject
{
    public bool IsNode;
    public TileBase[] Tiles;
    public Condition[] conditions;
    [Serializable]
    public class Condition
    {
        public Vector3Int offset;
        public bool isNeighborPresent = true;
    }
}
