using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Ground : MonoBehaviour
{
    private Tilemap _tilemap;

    public List<Vector3> TilesPositions { get; private set; }

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        TilesPositions = GetTilesPositions();
    }

    private List<Vector3> GetTilesPositions()
    {
        int marginX = 2;
        List<Vector3> spawnPositions = new List<Vector3>();
        BoundsInt platformBounds = _tilemap.cellBounds;
        TileBase[] tiles = _tilemap.GetTilesBlock(platformBounds);
        Vector3 start = _tilemap.CellToWorld(new Vector3Int(platformBounds.xMin, platformBounds.yMin, platformBounds.zMin));

        for (int x = marginX; x < platformBounds.size.x - marginX; x++)
        {
            int maxY = int.MinValue;

            for (int y = 0; y < platformBounds.size.y; y++)
            {
                TileBase tile = tiles[x + y * platformBounds.size.x];

                if (tile != null && y > maxY)
                    maxY = y;
            }

            if (maxY > int.MinValue)
            {
                Vector3 position = start + new Vector3(x, maxY, platformBounds.zMin);
                spawnPositions.Add(position);
            }
        }

        return spawnPositions;
    }
}
