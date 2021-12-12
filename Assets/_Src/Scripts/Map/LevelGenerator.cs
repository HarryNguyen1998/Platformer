using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

namespace MyPlatformer
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] SpriteRenderer _bg;
        [SerializeField] SpriteRenderer _bgGradient;

        [Tooltip("Tilemap to draw tiles on")]
        [SerializeField] Tilemap _tilemap;

        [SerializeField] MapData[] _mapDatas;

        [Header("Map settings")]
        [SerializeField] bool _isSeedRandom;
        [SerializeField] float _seed;
        [SerializeField] int _pillarHeight;
        [Tooltip("Area where Player starts, shouldn't apply procedural generation here")]
        [SerializeField] int _spawnWidth;
        [Tooltip("Area where the Goal is set, shouldn't apply procedural generation here")]
        [SerializeField] int _gameOverWidth;

        [Header("Members")]
        public int Width;
        public int Height;

        void OnEnable()
        {
            InputReader.GenerateMapEvent += GenerateMap;
        }

        void OnDisable()
        {
            InputReader.GenerateMapEvent -= GenerateMap;
        }

        public void GenerateMap()
        {
            ClearMap();

            if (_isSeedRandom)
                _seed = Time.time;

            Map2D map = new Map2D(Width, Height + _pillarHeight, true);

            // Where to spawn chasm and pillar, the loc shouldn't touch starting zone or the ending zone.
            int chasmWidth = 0;
            for (int x = 0; x < map.Width; ++x)
            {
                if (chasmWidth > 0)
                {
                    --chasmWidth;
                    continue;
                }
                
                bool shouldSpawnChasm = ((x >= _spawnWidth) && (x <= map.Width - _gameOverWidth) && Random.Range(0, 11) == 1);
                if (shouldSpawnChasm)
                {
                    chasmWidth = Random.Range(0, 5) == 1 ? 2 : 1;
                    --chasmWidth;
                    continue;
                }

                int height = map.Height;
                bool shouldSpawnPillar = ((x >= _spawnWidth) && (x <= map.Width - _gameOverWidth) && Random.Range(0, 5) == 1);

                if (!shouldSpawnPillar)
                    height -= _pillarHeight;
                else
                    height -= Random.Range(1, _pillarHeight);

                for (int y = 0; y < height; ++y)
                {
                    map[x, y] = true;
                }
            }

            Vector2Int offset = new Vector2Int(-_spawnWidth / 2, -Height);

            // Choose a random biome to draw
            int biomeIndex = Random.Range(0, _mapDatas.Length);
            _bg.sprite = _mapDatas[biomeIndex].GetRandomBGSprite();
            _bgGradient.sprite = _mapDatas[biomeIndex].BGGradient;
            MapGenerationHelper.RenderMapWithOffset(map, _tilemap, _mapDatas[biomeIndex].RuleTile, offset);
        }

        public void ClearMap()
        {
            _tilemap.ClearAllTiles();
        }

    }


}
