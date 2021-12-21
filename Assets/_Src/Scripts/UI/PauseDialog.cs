using UnityEngine;
using UnityEngine.UI;

namespace MyPlatformer
{
    public class PauseDialog : MonoBehaviour
    {
        [SerializeField] Button _backBtn;
        [SerializeField] Button _mainMenuBtn;

        void OnEnable()
        {
            _backBtn.onClick.AddListener(GameStateParameterProvider.Instance.UnsetPause);
            _mainMenuBtn.onClick.AddListener(GameStateParameterProvider.Instance.SetMainMenu);
            _mainMenuBtn.onClick.AddListener(GameStateParameterProvider.Instance.UnsetPause);
        }

        void OnDisable()
        {
            _backBtn.onClick.RemoveListener(GameStateParameterProvider.Instance.UnsetPause);
            _mainMenuBtn.onClick.RemoveListener(GameStateParameterProvider.Instance.SetMainMenu);
            _mainMenuBtn.onClick.RemoveListener(GameStateParameterProvider.Instance.UnsetPause);
        }
    }
}
