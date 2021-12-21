using UnityEngine;

namespace MyPlatformer
{
    public class Goal : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponentInParent<PlayerCollider>())
            {
                GameStateParameterProvider.Instance.SetGameOver();
            }
        }
    }
}
