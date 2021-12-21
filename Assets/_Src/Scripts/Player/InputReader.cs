using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyPlatformer
{
    public class InputReader : MonoBehaviour, PlayerInput.IConfigActions, PlayerInput.IGameplayActions
    {
        public static event Action GenerateMapEvent;
        public static event Action<Vector2> MoveEvent;
        public static event Action JumpEvent;

        PlayerInput _playerInput;

        void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.Config.SetCallbacks(this);
                _playerInput.Gameplay.SetCallbacks(this);
            }

            _playerInput.Config.Enable();
            _playerInput.Gameplay.Enable();
            PlayerController.PlayerDied += DisableGameplayInput;
        }

        void OnDisable()
        {
            _playerInput.Config.Disable();
            _playerInput.Gameplay.Disable();
            PlayerController.PlayerDied -= DisableGameplayInput;
        }

        void PlayerInput.IConfigActions.OnGenerateMap(InputAction.CallbackContext context)
        {
            // @note This is basically isPressedThisFrame.
            if (context.phase == InputActionPhase.Performed)
                GenerateMapEvent?.Invoke();
        }

        void PlayerInput.IGameplayActions.OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                JumpEvent?.Invoke();
        }

        void PlayerInput.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        void DisableGameplayInput()
        {
            _playerInput.Gameplay.Disable();
        }

    }
}
