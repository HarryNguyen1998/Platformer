using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] CinemachineBrain _cinemachine;

        void OnEnable()
        {
            PlayerController.PlayerDied += OnPlayerDied;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            PlayerController.PlayerDied -= OnPlayerDied;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.buildIndex == 0)
                return;

            _cinemachine = FindObjectOfType<CinemachineBrain>();
        }

        void OnPlayerDied()
        {
            _cinemachine.enabled = false;
        }

    }
}
