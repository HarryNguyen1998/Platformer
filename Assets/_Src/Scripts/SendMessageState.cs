using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public enum GameState
    {
        kMainMenu,
        kGameplay,
        kGameOver,
        kPause,
        kQuit
    }

    public class SendMessageState : StateMachineBehaviour
    {
        public static event Action<GameState> GameStateChanged;
        public GameState StateToSwitchTo;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DG.Tweening.DOTween.KillAll();
            switch (StateToSwitchTo)
            {
                case GameState.kMainMenu:
                {
                    Time.timeScale = 0.0f;
                    break;
                }
                case GameState.kGameplay:
                {
                    AsyncOperation ao = SceneManager.LoadSceneAsync("Game");
                    ao.completed += LoadSceneCompleted;
                    Time.timeScale = 1.0f;
                    return;
                }
                case GameState.kGameOver:
                    break;
                case GameState.kPause:
                    Time.timeScale = 0.0f;
                    break;
                case GameState.kQuit:
                {
                    if (Application.isEditor)
                        UnityEditor.EditorApplication.isPlaying = false;
                    else
                        Application.Quit();
                    break;
                }
            }

            GameStateChanged?.Invoke(StateToSwitchTo);
        }

        private void LoadSceneCompleted(AsyncOperation obj)
        {
            GameStateChanged?.Invoke(StateToSwitchTo);
            obj.completed -= LoadSceneCompleted;
        }
    }
}
