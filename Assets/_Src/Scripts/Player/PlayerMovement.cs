using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyPlatformer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _spd = 10.0f;
        [SerializeField] float _jumpSpd = 10.0f;

        Rigidbody2D _rb;
        Animator _animator;
        PlayerInput _playerInput;
        Vector2 _dir;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerInput = new PlayerInput();
        }

        void OnEnable()
        {
            _playerInput.Gameplay.Enable();
            _playerInput.Gameplay.Move.performed += SetDir;
            _playerInput.Gameplay.Move.canceled += SetDir;

            _playerInput.Gameplay.Jump.performed += Jump;
        }

        void OnDisable()
        {
            _playerInput.Gameplay.Disable();
            _playerInput.Gameplay.Move.performed -= SetDir;
            _playerInput.Gameplay.Move.canceled -= SetDir;
            _playerInput.Gameplay.Jump.performed -= Jump;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_dir.x * _spd, _rb.velocity.y);

            // @note If dir is 0, don't flip the sprite and stay where it's at
            if (!Mathf.Approximately(Mathf.Abs(_dir.x), 0.0f))
            {
                transform.localScale = new Vector2(Mathf.Sign(_dir.x), transform.localScale.y);
                _animator.SetBool("IsRunning", true);
            }
            else
                _animator.SetBool("IsRunning", false);
        }

        void Jump(InputAction.CallbackContext obj)
        {
            // @note No multi jumping
            if (!_rb.IsTouchingLayers(LayerMask.GetMask("Ground")))
                return;

            _rb.velocity += new Vector2(0.0f, obj.ReadValue<float>() * _jumpSpd);
            //_animator.SetBool("IsJumping", true);
        }

        void SetDir(InputAction.CallbackContext obj)
        {
            _dir = obj.ReadValue<Vector2>();


        }
    }
}
