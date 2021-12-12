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
        Vector2 _dir;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            InputReader.MoveEvent += SetDir;
            InputReader.JumpEvent += Jump;
        }

        void OnDisable()
        {
            InputReader.MoveEvent -= SetDir;
            InputReader.JumpEvent -= Jump;
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

        void Jump()
        {
            // @note No multi jumping
            if (!_rb.IsTouchingLayers(LayerMask.GetMask("Ground")))
                return;

            _rb.velocity += new Vector2(0.0f, _jumpSpd);
            //_animator.SetBool("IsJumping", true);
        }

        void SetDir(Vector2 value)
        {
            _dir = value;
        }
    }
}
