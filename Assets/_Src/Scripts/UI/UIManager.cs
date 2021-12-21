using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public class UIManager : MonoBehaviour
    {
        List<MenuData> _menus;
        MenuData _lastActiveMenu;

        static UIManager _instance;
        public static UIManager Instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            if (!_instance)
            {
                _instance = this;
            }
            else
                Destroy(gameObject);

            _menus = new List<MenuData>();
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SendMessageState.GameStateChanged += OnGameStateChanged;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SendMessageState.GameStateChanged -= OnGameStateChanged;
        }

        public void RegisterMenu(MenuData menu)
        {
            _menus.Add(menu);
        }

        public void UnregisterMenu(MenuData menu)
        {
            _menus.Remove(menu);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.buildIndex == 0)
                return;
            StartCoroutine(CO_WaitOneFrame());

            _menus.ForEach(menu =>
            {
                if (menu.GameStateToShow == GameState.kMainMenu)
                    _lastActiveMenu = menu;
                else
                    menu.gameObject.SetActive(false);
            });
        }

        IEnumerator CO_WaitOneFrame()
        {
            yield return null;
        }

        public void OnGameStateChanged(GameState newState)
        {
            // Transition from Gameplay to others
            if (!_lastActiveMenu)
            {
                switch (newState)
                {
                    case GameState.kGameOver:
                    case GameState.kPause:
                    {
                        _lastActiveMenu = _menus.Find(menu => menu.GameStateToShow == newState);
                        _lastActiveMenu.gameObject.SetActive(true);
                        break;
                    }
                    case GameState.kMainMenu:
                    case GameState.kGameplay:
                    case GameState.kQuit:
                        break;
                }

                return;
            }

            // Transition from others to Gameplay
            if (_lastActiveMenu && newState == GameState.kGameplay)
            {
                switch (newState)
                {
                    case GameState.kMainMenu:
                    case GameState.kGameOver:
                    case GameState.kPause:
                    case GameState.kGameplay:
                    {
                        _lastActiveMenu.gameObject.SetActive(false);
                        _lastActiveMenu = null;
                        break;
                }
                    case GameState.kQuit:
                        break;
                }

                return;
            }

            // Transition to/from MainMenu
            switch (newState)
            {
                case GameState.kMainMenu:
                case GameState.kGameOver:
                {
                    _lastActiveMenu.gameObject.SetActive(false);
                    _lastActiveMenu = _menus.Find(menu => menu.GameStateToShow == newState);
                    _lastActiveMenu.gameObject.SetActive(true);
                    break;
            }
                case GameState.kGameplay:
                case GameState.kPause:
                case GameState.kQuit:
                    break;
            }
        }

    }
}

