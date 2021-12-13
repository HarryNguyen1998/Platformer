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
        bool _shouldStartStopWatch;
        float _currentTime;
        int _score;
        int _highScore;

        private void OnEnable()
        {
            Token.WasEaten += AddScore;
        }

        private void OnDisable()
        {
            Token.WasEaten -= AddScore;
        }

        void Start()
        {
            _currentTimeText.text = "00:00:00";
            _highScoreText.text = $"HIGH SCORE: {_highScore}";
        }

        void Update()
        {
            if (_shouldStartStopWatch)
            {
                _currentTime += Time.deltaTime;
                TimeSpan time = TimeSpan.FromSeconds(_currentTime);

                _currentTimeText.text = time.ToString(@"mm\:ss\:fff");
            }
        }

        public void StartStopWatch()
        {
            _shouldStartStopWatch = true;
        }

        public void StopStopWatch()
        {
            _shouldStartStopWatch = false;
        }

        public void AddScore()
        {
            ++_score;
            _scoreText.text = $"SCORE: {_score}";
            if (_highScore < _score)
            {
                _highScore = _score;
                _highScoreText.text = $"HIGH SCORE: {_highScore}";
            }
        }
    }
}
