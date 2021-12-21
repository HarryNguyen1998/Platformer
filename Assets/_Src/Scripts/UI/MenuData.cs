using UnityEngine;

namespace MyPlatformer
{
    public class MenuData : MonoBehaviour
    {
        public GameState GameStateToShow;

        void Awake()
        {
            UIManager.Instance.RegisterMenu(this);    
        }

        void OnDestroy()
        {
            UIManager.Instance?.UnregisterMenu(this);
        }
    }
}

