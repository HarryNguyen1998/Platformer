using System;
using UnityEngine;
using TMPro;

namespace MyPlatformer
{
    public class GameplayDialog : MonoBehaviour
    {
        [SerializeField] TMP_Text _currentTimeText;
        [SerializeField] TMP_Text _scoreText;
        [SerializeField] TMP_Text _highScoreText;
        ScoreManager _scoreManager;

        private void Awake()
        {
            _scoreManager = ScoreManager.Instance;
        }

        private void OnEnable()
        {
            _scoreManager.ScoreChanged += ShowScore;
        }

        private void OnDisable()
        {
            _scoreManager.ScoreChanged -= ShowScore;
        }

        void Start()
        {
            _currentTimeText.text = "00:00:00";
            _scoreText.text = $"SCORE: {_scoreManager.Score}";
            _highScoreText.text = $"HIGH SCORE: {_scoreManager.HighScore}";
        }

        void Update()
        {
            TimeSpan time = TimeSpan.FromSeconds(_scoreManager.CurrentTime);
            _currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        }

        void ShowScore()
        {
            _scoreText.text = $"SCORE: {_scoreManager.Score}";
            _highScoreText.text = $"HIGH SCORE: {_scoreManager.HighScore}";
        }

    }
}
