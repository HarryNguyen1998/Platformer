using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyPlatformer
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] SpriteRenderer _bg;
        [SerializeField] SpriteRenderer _bgGradient;
        [SerializeField] Transform _dynamicObjs;
        [SerializeField] Token _tokenPrefab;

        [Tooltip("Tilemap to draw tiles on")]
        [SerializeField] Tilemap _tilemap;

        [SerializeField] MapData[] _mapDatas;

        [Header("Map settings")]
        [SerializeField] bool _isSeedRandom;
        [SerializeField] int _seed;
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
                _seed = (int)System.DateTime.UtcNow.Ticks;

            Random.InitState(_seed);

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
                
                bool shouldSpawnChasm = ((x >= _spawnWidth) && (x <= map.Width - _gameOverWidth) && Random.Range(0, 10) == 1);
                if (shouldSpawnChasm)
                {
                    chasmWidth = Random.Range(0, 5) == 1 ? 4 : 2;
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

            // Spawn token
            // @note World position to grid position is offset by Vector2(0.5f, 0.5f)
            BoxCollider2D tokenCollider = _tokenPrefab.GetComponent<BoxCollider2D>();
            Vector2 tokenOffset = new Vector2(offset.x + 0.5f - tokenCollider.offset.x, offset.y + 0.5f - tokenCollider.offset.y);
            int counter = 0;
            for (int x = 0; x < map.Width; ++x)
            {
                if (Random.Range(0, 6) != 1)
                    continue;

                for (int y = 0; y < map.Height; ++y)
                {
                    // Skip chasm
                    if (!map[x, y] && y == 0)
                    {
                        break;
                    }

                    // We're above top tile
                    if (!map[x, y])
                    {
                        Debug.Log($"{x},{y} the {++counter} times");
                        Instantiate(_tokenPrefab, new Vector3(x + tokenOffset.x, y + tokenOffset.y, 0), Quaternion.identity, _dynamicObjs).name += counter.ToString();
                        break;
                    }
                }
            }

            // Choose a random biome to draw
            int biomeIndex = Random.Range(0, _mapDatas.Length);
            _bg.sprite = _mapDatas[biomeIndex].GetRandomBGSprite();
            _bgGradient.sprite = _mapDatas[biomeIndex].BGGradient;
            MapGenerationHelper.RenderMapWithOffset(map, _tilemap, _mapDatas[biomeIndex].RuleTile, offset);
        }

        public void ClearMap()
        {
            _tilemap.ClearAllTiles();
            // Destroy all dynamic objs from previous time
            for (int i = _dynamicObjs.transform.childCount - 1; i >= 0; --i)
            {
                DestroyImmediate(_dynamicObjs.transform.GetChild(i).gameObject);
            }

        }

        void OnDrawGizmos()
        {
            
        }

    }


}
