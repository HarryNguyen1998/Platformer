using UnityEngine;

namespace MyPlatformer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _spd = 10.0f;
        [SerializeField] float _jumpSpd = 10.0f;
        [SerializeField] LayerMask _groundMask;
        [SerializeField] Transform _groundChecker;

        Rigidbody2D _rb;
        Animator _animator;
        Vector2 _dir;
        bool _isGrounded;

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

        void FixedUpdate()
        {
            _rb.velocity = new Vector2(_dir.x * _spd, _rb.velocity.y);
            _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, 0.2f, _groundMask);//_rb.IsTouchingLayers(_groundMask);
        }

        private void Update()
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
            if (!_isGrounded)
            {
                _animator.SetBool("IsGrounded", false);
                _animator.SetFloat("VelocityY", 1.0f * Mathf.Sign(_rb.velocity.y));
            }
            else
            {
                _animator.SetBool("IsGrounded", true);
                _animator.SetFloat("VelocityY", 0.0f);
            }
        }

        void Jump()
        {
            // @note No multi jumping
            if (!_isGrounded)
                return;

            _rb.velocity += Vector2.up * _jumpSpd;
        }

        void SetDir(Vector2 value)
        {
            _dir = value;
        }
    }
}
