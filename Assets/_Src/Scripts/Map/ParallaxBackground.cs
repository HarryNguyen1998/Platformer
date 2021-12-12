using UnityEngine;

namespace MyPlatformer
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] float _parallaxMultiplier;
        Transform _camTransform;
        Vector3 _camPrevPos;
        float _textureUnitSizeX;

        private void Start()
        {
            _camTransform = Camera.main.transform;
            _camPrevPos = _camTransform.position;
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            _textureUnitSizeX = (sprite.texture.width / sprite.pixelsPerUnit);
        }

        private void LateUpdate()
        {
#if false
            Vector3 deltaMvmt = _camTransform.position - _camPrevPos;
            transform.position += deltaMvmt * _parallaxMultiplier;
            _camPrevPos = _camTransform.position;
#endif

            float diffX = Mathf.Abs(_camTransform.position.x - transform.position.x);
            if (diffX >= _textureUnitSizeX)
            {
                diffX %= _textureUnitSizeX;
                transform.position = new Vector3(_camTransform.position.x + diffX, transform.position.y);
            }
        }

    }
}
