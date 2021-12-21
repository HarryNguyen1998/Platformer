using UnityEngine;
using UnityEngine.UI;

namespace MyPlatformer
{
    public class MainMenuDialog : MonoBehaviour
    {
        [SerializeField] Button _playBtn;

        void OnEnable()
        {
            _playBtn.onClick.AddListener(GameStateParameterProvider.Instance.UnsetMainMenu);
        }

        void OnDisable()
        {
            _playBtn.onClick.AddListener(GameStateParameterProvider.Instance.UnsetMainMenu);
        }
    }
}
