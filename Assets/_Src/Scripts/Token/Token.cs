using System;
using UnityEngine;

namespace MyPlatformer
{
    public class Token : MonoBehaviour
    {
        public static event Action WasEaten;
        

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerMovement>() != null)
            {
                WasEaten?.Invoke();
                Destroy(gameObject);
            }
        }

    }
}
