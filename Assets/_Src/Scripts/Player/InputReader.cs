using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyPlatformer
{
    [CreateAssetMenu]
    public class InputReader : MonoBehaviour, PlayerInput.IConfigActions, PlayerInput.IGameplayActions
    {
        public static event Action GenerateMapEvent;
        public static event Action<Vector2> MoveEvent;
        public static event Action JumpEvent;

        PlayerInput _playerInput;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.Config.SetCallbacks(this);
                _playerInput.Gameplay.SetCallbacks(this);
            }

            _playerInput.Config.Enable();
            _playerInput.Gameplay.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Config.Disable();
            _playerInput.Gameplay.Disable();
        }

        public void OnGenerateMap(InputAction.CallbackContext context)
        {
            // @note This is basically isPressedThisFrame.
            if (context.phase == InputActionPhase.Performed)
                GenerateMapEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                JumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

    }
}
