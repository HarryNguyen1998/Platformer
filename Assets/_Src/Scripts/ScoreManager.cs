using System;
using UnityEngine;

namespace MyPlatformer
{
    public class ScoreManager : MonoBehaviour
    {
        public event Action ScoreChanged;

        public int Score { get; set; }
        public int HighScore { get; set; }
        public float CurrentTime { get; set; }
        public static ScoreManager Instance { get; set; }

        bool _shouldStartStopwatch;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void OnEnable()
        {
            SendMessageState.GameStateChanged += OnGameStateChanged;
            Token.WasEaten += AddScore;
        }

        void OnDisable()
        {
            SendMessageState.GameStateChanged -= OnGameStateChanged;
            Token.WasEaten -= AddScore;
        }

        void Update()
        {
            if (!_shouldStartStopwatch)
                return;

            CurrentTime += Time.deltaTime;
        }

        public void Reset()
        {
            Score = 0;
            CurrentTime = 0.0f;
        }

        void AddScore()
        {
            ++Score;
            if (HighScore < Score)
            {
                HighScore = Score;
            }

            ScoreChanged?.Invoke();
        }

        void OnGameStateChanged(GameState newState)
        {
            if (newState == GameState.kGameplay)
            {
                _shouldStartStopwatch = true;
                return;
            }

            _shouldStartStopwatch = false;
            if (newState == GameState.kMainMenu)
                Reset();
        }

    }
}
