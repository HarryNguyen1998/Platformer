using UnityEngine;

namespace MyPlatformer
{
    [RequireComponent(typeof(Animator))]
    public class GameStateParameterProvider : MonoBehaviour
    {
        Animator _animator;
        public static GameStateParameterProvider Instance { get; private set; }

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            PlayerController.PlayerDied += SetGameOver;
        }

        void OnDisable()
        {
            PlayerController.PlayerDied -= SetGameOver;
        }

        public void SetMainMenu()
        {
            _animator.SetBool("MainMenu", true);
        }

        public void UnsetMainMenu()
        {
            _animator.SetBool("MainMenu", false);
        }

        public void SetGameOver()
        {
            _animator.SetBool("GameOver", true);
        }

        public void UnsetGameOver()
        {
            _animator.SetBool("GameOver", false);
        }

        public void SetPause()
        {
            _animator.SetBool("Pause", true);
        }

        public void UnsetPause()
        {
            _animator.SetBool("Pause", false);
        }
    }

}
