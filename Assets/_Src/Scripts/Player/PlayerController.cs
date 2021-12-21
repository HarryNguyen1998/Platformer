using System;
using UnityEngine;
using DG.Tweening;

namespace MyPlatformer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public static event Action PlayerDied;

        [SerializeField] float _spd = 10.0f;
        [SerializeField] float _jumpSpd = 10.0f;
        [SerializeField] Vector2 _hurtJumpBack;

        Rigidbody2D _rb;
        Animator _animator;
        Vector2 _dir;
        bool _isDead;

        public bool IsGrounded { get; set; }

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

        }

        void OnEnable()
        {
            InputReader.MoveEvent += SetDir;
            InputReader.JumpEvent += Jump;
            SendMessageState.GameStateChanged += GameOver;
        }

        void OnDisable()
        {
            InputReader.MoveEvent -= SetDir;
            InputReader.JumpEvent -= Jump;
            SendMessageState.GameStateChanged -= GameOver;
        }

        void FixedUpdate()
        {
            if (Application.isEditor)
                Debug.DrawLine(transform.position, new Vector3(transform.position.x + _rb.velocity.x, transform.position.y + _rb.velocity.y, transform.position.z), Color.yellow);

            if (transform.position.y < -4.0f)
                Die();

            if (_isDead)
                return;

            _rb.velocity = new Vector2(_dir.x * _spd, _rb.velocity.y);
        }

        void Update()
        {
            // Running animation
            if (Mathf.Abs(_dir.x) > 0.0f)
            {
                transform.localScale = new Vector2(Mathf.Sign(_dir.x), transform.localScale.y);
                _animator.SetBool("IsRunning", true);
            }
            else
            {
                _animator.SetBool("IsRunning", false);
            }

            // Jumping animation
            _animator.SetBool("IsGrounded", IsGrounded);
            _animator.SetFloat("VelocityY", 1.0f * Mathf.Sign(_rb.velocity.y));
        }

        public void GameOver(GameState newState)
        {
            if (newState == GameState.kGameOver)
                PlayerDied?.Invoke();
        }

        public void Die()
        {
            _animator.SetTrigger("IsDead");
            _isDead = true;
            _rb.AddForce(_hurtJumpBack, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().DOFade(0.0f, 0.25f).SetEase(Ease.OutQuad).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
            {
                _rb.velocity = Vector2.zero;
                enabled = false;
                gameObject.SetActive(false);
            });

            PlayerDied?.Invoke();
        }

        void Jump()
        {
            // @note No multi jumping
            if (!IsGrounded)
                return;

            IsGrounded = false;
            _rb.velocity += Vector2.up * _jumpSpd;
        }

        void SetDir(Vector2 value)
        {
            _dir = value;
        }
    }
}
