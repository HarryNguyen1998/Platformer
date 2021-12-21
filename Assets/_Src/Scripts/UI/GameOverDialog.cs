using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace MyPlatformer
{
    public class GameOverDialog : MonoBehaviour
    {
        [SerializeField] TMP_Text _timeText;
        [SerializeField] TMP_Text _scoreText;
        [SerializeField] TMP_Text _highScoreText;
        [SerializeField] Button _tryAgainBtn;
        [SerializeField] Button _backToMainMenuBtn;
        ScoreManager _scoreManager;

        private void Awake()
        {
            _scoreManager = ScoreManager.Instance;
        }

        void OnEnable()
        {
            _tryAgainBtn.onClick.AddListener(GameStateParameterProvider.Instance.UnsetGameOver);
            _tryAgainBtn.onClick.AddListener(ScoreManager.Instance.Reset);
            _backToMainMenuBtn.onClick.AddListener(GameStateParameterProvider.Instance.SetMainMenu);
            _backToMainMenuBtn.onClick.AddListener(GameStateParameterProvider.Instance.UnsetGameOver);

            if (_scoreManager.Score == _scoreManager.HighScore)
            {
                _scoreText.text = $"NEW High Score: {_scoreManager.Score}";
                _scoreText.color = Color.green;
                _scoreText.DOFade(0.0f, 0.5f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
                _highScoreText.gameObject.SetActive(false);
            }
            else
            {
                _scoreText.color = Color.white;
                _scoreText.text = $"Score: {_scoreManager.Score}";
                _highScoreText.text = $"High Score: {_scoreManager.HighScore}";
            }

            TimeSpan time = TimeSpan.FromSeconds(_scoreManager.CurrentTime);
            _timeText.text = $"Time: {time.ToString(@"mm\:ss\:fff")}";

        }

        void OnDisable()
        {
            _tryAgainBtn.onClick.RemoveListener(GameStateParameterProvider.Instance.UnsetGameOver);
            _tryAgainBtn.onClick.RemoveListener(ScoreManager.Instance.Reset);
            _backToMainMenuBtn.onClick.RemoveListener(GameStateParameterProvider.Instance.SetMainMenu);
            _backToMainMenuBtn.onClick.RemoveListener(GameStateParameterProvider.Instance.UnsetGameOver);
        }

    }
}
