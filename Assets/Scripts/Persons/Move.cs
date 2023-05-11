using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(SpriteRenderer))]
public class Move : MonoBehaviour
{
    /// <summary>
    /// True - передвигается только влево или вправо, может прыгать, на тело действует гравитация.
    /// False - Летает по экрано во всех направлениях, гравитация отключена.
    /// </summary>
    [SerializeField] private bool _canJump;
    public bool CanJump
    {
        get
        {
            return _canJump;
        }
        set
        {
            _canJump = value;
            if (_canJump)
            {
                _move = MoveAndJump;
                _rigidbody2D.gravityScale = 1f;
            }
            else
            {
                _move = MoveNotJump;
                _rigidbody2D.gravityScale = 0f;
            }
        }
    }
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpingPower = 10f;
    private float _speedMultiplier = 1f;
    private float _curentSpeed;
    private Action _move;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _velocity = Vector2.zero;
    public void MoveHorizontally(float direction)
    {
        _velocity.Set(0f, _rigidbody2D.velocity.y);
        if (direction == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction < 0f)
        {
            _velocity.x = -_curentSpeed;
            _spriteRenderer.flipX = true;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _velocity.x = _curentSpeed;
            _spriteRenderer.flipX = false;
            _animator.SetBool("IsRunning", true);
        }
    }
    public void MoveVertically(float direction)
    {
        _velocity.Set(_rigidbody2D.velocity.x, 0f);
        if (direction == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction < 0f)
        {
            _velocity.y = -_curentSpeed;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _velocity.y = _curentSpeed;
            _animator.SetBool("IsRunning", true);
        }
    }
    public void StartJump()
    {
        if (CanJump)
        {
            _velocity.Set(_velocity.x, _jumpingPower);
            _animator.SetBool("IsJumping", true);
            _rigidbody2D.velocity = _velocity;
        }
    }
    public void StopJump()
    {
        _animator.SetBool("IsJumping", false);
    }
    public void SetSpeedMultiplierTemporary(float multiplier, float time = 1f)
    {
        _curentSpeed = _speed * multiplier;
        ResetSpeed();
        Invoke(nameof(SetDefaultSpeed), time);
    }

    public void SetSpeedMultiplierForOllTime(float multiplier = 1f)
    {
        _speedMultiplier = multiplier;
        _curentSpeed = _speed * _speedMultiplier;
        ResetSpeed();
    }
    public bool IsMultiplierBoost()
    {
        return IsInvoking(nameof(SetDefaultSpeed));
    }
    private void SetDefaultSpeed()
    {
        SetSpeedMultiplierForOllTime(_speedMultiplier);
    }
    private void ResetSpeed()
    {
        MoveHorizontally(_velocity.x);
        if (!CanJump)
        {
            MoveVertically(_velocity.y);
        }
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SetSpeedMultiplierForOllTime(_speedMultiplier);
    }
    private void OnEnable()
    {
        CanJump = CanJump;
    }
    private void FixedUpdate()
    {
        _move();
    }
    private void MoveAndJump()
    {
        _velocity.y = _rigidbody2D.velocity.y;
        MoveNotJump();
    }
    private void MoveNotJump()
    {
        _rigidbody2D.velocity = _velocity;
    }
    private void OnDisable()
    {
        _move = null;
    }
}
