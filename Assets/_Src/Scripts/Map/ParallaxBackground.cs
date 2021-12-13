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
            Vector3 deltaMvmt = _camTransform.position - _camPrevPos;
            transform.position -= deltaMvmt * (1.0f - _parallaxMultiplier);
            _camPrevPos = _camTransform.position;

            float diffX = Mathf.Abs(_camTransform.position.x - transform.position.x);
            if (diffX >= _textureUnitSizeX)
            {
                diffX %= _textureUnitSizeX;
                transform.position = new Vector3(_camTransform.position.x + diffX, transform.position.y);
            }
        }

    }
}
