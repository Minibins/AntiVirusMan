using DustyStudios.MathAVM;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Tilemaps;
public class WireGenerator : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private Line[] lines;
    [SerializeField] private TileBase lineTile;
    [SerializeField] private WireTile[] replacementTiles;
    [SerializeField, Range(0,3)] private byte color;
    [SerializeField] private int seed;
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        replacementTiles = replacementTiles.OrderBy(t => t.conditions.Length).ToArray();
        StartCoroutine(regenerateWires());
    }
    private IEnumerator regenerateWires()
    {
        while(true)
        {
            tilemap.ClearAllTiles();
            foreach(var line in lines) DrawLine(line.object1.position,line.object2.position);
            ReplaceTilesWithNeighbors();
            yield return null;
        }
    }
    private void DrawLine(Vector3 cell1,Vector3 cell2)
    {
        Vector3 direction = cell2 - cell1;
        direction = direction.NormalizedMin1();
        int distance = (int)Mathf.Max(Mathf.Abs(cell2.x - cell1.x), Mathf.Abs(cell2.y - cell1.y));
        Vector3Int lastCell = tilemap.WorldToCell(cell1);
        for(int i = 0; i <= distance; i++)
        {
            Vector3Int currentCell = tilemap.WorldToCell(cell1 + direction * i);
            if(currentCell.x != lastCell.x)
                tilemap.SetTile(new(lastCell.x,currentCell.y,currentCell.z),lineTile);
            tilemap.SetTile(currentCell,lineTile);
            lastCell = currentCell;
        }
    }
    private void ReplaceTilesWithNeighbors()
    {
        Dictionary<Vector2Int,WireTile> tiles=new();
        BoundsInt bounds = tilemap.cellBounds;
        for(int x = bounds.xMin; x < bounds.xMax; x++)
            for(int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, (int)tilemap.transform.position.z);
                foreach(WireTile replacementTile in replacementTiles)
                {
                    TileBase tile = tilemap.GetTile(pos);
                    if(tile && CheckTileNeighbors(pos,replacementTile.conditions))
                    {
                        if(tiles.ContainsKey((Vector2Int)pos))
                            tiles[(Vector2Int)pos] = replacementTile;
                        else
                            tiles.Add((Vector2Int)pos,replacementTile);

                    }
                }
            }
        bool CheckTileNeighbors(Vector3Int position,WireTile.Condition[] conditions)
        {
            foreach(WireTile.Condition condition in conditions)
                if(tilemap.GetTile(position + condition.offset) == null == condition.isNeighborPresent)
                    return false;
            return true;
        }
        SetDifferentWireColors(tiles);
    }
    private void SetDifferentWireColors(Dictionary<Vector2Int,WireTile> tiles)
    {
        Dictionary<Vector2Int,int> placesToFill = new();
        System.Random random = new(seed);
        foreach(KeyValuePair<Vector2Int,WireTile> nodeTile in tiles.Where(t => t.Value.IsNode).OrderBy(t=>t.Value.conditions.Length))
        {
            foreach(WireTile.Condition pos in nodeTile.Value.conditions.Where(c=>c.isNeighborPresent))
            {
                int color = random.Next(0,4);
                bool hasChangedTile = false;
                List<Vector2Int> positions = new(), checkedPositions = new();
                positions.Add((Vector2Int)pos.offset+nodeTile.Key);
                while(positions.Count > 0)
                {
                    checkedPositions.Add(positions[0]);
                    if(tiles.ContainsKey(positions[0]) && !tiles[positions[0]].IsNode)
                    {
                        if(placesToFill.ContainsKey(positions[0]))
                            placesToFill[positions[0]] = color;
                        else
                            placesToFill.Add(positions[0],color);
                        foreach(WireTile.Condition nextPos in tiles[positions[0]].conditions.Where(c => c.isNeighborPresent))
                            if(!checkedPositions.Contains((Vector2Int)nextPos.offset + positions[0]))
                                positions.Add((Vector2Int)nextPos.offset + positions[0]);
                        hasChangedTile = true;
                    }
                    positions.RemoveAt(0);
                }
                if(hasChangedTile)
                {
                    if(placesToFill.ContainsKey(nodeTile.Key))
                        placesToFill[nodeTile.Key] = color;
                    else placesToFill.Add(nodeTile.Key, color);
                }
            }
        }
        foreach(KeyValuePair<Vector2Int,WireTile> tile in tiles)
            if(!placesToFill.ContainsKey(tile.Key)) placesToFill.Add(tile.Key,placesToFill[tile.Key+(Vector2Int)tile.Value.conditions[0].offset]);
        foreach(KeyValuePair<Vector2Int,int> place in placesToFill)
            tilemap.SetTile((Vector3Int)place.Key,tiles[place.Key].Tiles[place.Value]);
    }
    
    [System.Serializable]
    class Line
    {
        public Transform object1,object2;
    }
}