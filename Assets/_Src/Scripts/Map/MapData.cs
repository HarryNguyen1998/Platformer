using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyPlatformer
{
    [CreateAssetMenu]
    public class MapData : ScriptableObject
    {
        public Sprite[] BGSprites;
        public TileBase RuleTile;
        public Sprite BGGradient;

        public Sprite GetRandomBGSprite()
        {
            return BGSprites[UnityEngine.Random.Range(0, BGSprites.Length)];
        }
    }
}
