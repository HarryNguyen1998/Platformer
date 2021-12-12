using UnityEngine;
using UnityEngine.Tilemaps;


namespace MyPlatformer
{
    public static class MapGenerationHelper
    {
        public static void RenderMapWithOffset(Map2D map, Tilemap tilemap, TileBase tile, Vector2Int offset)
        {
            for (int y = 0; y < map.Height; ++y)
            {
                for (int x = 0; x < map.Width; ++x)
                {
                    if (map[x, y])
                        tilemap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), tile);
                }
            }

        }

        public static void UpdateMap(int[,] map, Tilemap tilemap)
        {
            for (int i = 0; i < map.GetUpperBound(0); ++i)
            {
                for (int j = 0; j < map.GetUpperBound(1); ++j)
                {
                    if (map[i, j] == 0)
                        tilemap.SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }

        public static int[,] PerlinNoise(int[,] map, float seed)
        {
            int newP;
            float reduction = 0.5f; // Used to reduce pos of Perlin pt
            for (int i = 0; i < map.GetUpperBound(0); ++i)
            {
                newP = Mathf.FloorToInt((Mathf.PerlinNoise(i, seed) - reduction) * map.GetUpperBound(1));

                // Make sure the pt starts at near halfway pt of the height
                newP += (map.GetUpperBound(1) / 2);
                for (int j = newP; j >= 0; --j)
                    map[i, j] = 1;
            }

            return map;
        }

    }

}
