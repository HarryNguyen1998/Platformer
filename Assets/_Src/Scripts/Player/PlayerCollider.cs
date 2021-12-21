using UnityEngine;

namespace MyPlatformer
{
    public class PlayerCollider : MonoBehaviour
    {
        [SerializeField] PlayerController _pc;
        [SerializeField] LayerMask _groundMask;
        [SerializeField] LayerMask _enemyMask;

        Collider2D[] _colliders;

        private void Awake()
        {
            _colliders = GetComponentsInChildren<Collider2D>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            for (int i = 0; i < _colliders.Length; ++i)
            {
                if (_colliders[i].CompareTag(GameTags.LegCollider))
                {
                    if (1 << collision.gameObject.layer == _groundMask)
                    {
                        _pc.IsGrounded = true;
                    }
                }
            }

            if (1 << collision.gameObject.layer == _enemyMask)
            {
                _pc.Die();
            }
        }
    }
}
