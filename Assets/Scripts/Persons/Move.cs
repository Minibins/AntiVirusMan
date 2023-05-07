using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(SpriteRenderer))]
public class Move : MonoBehaviour
{
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
            }
            else
            {
                _move = MoveNotJump;
            }
        }
    }
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpingPower = 10f;
    private Action _move;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _velocity = Vector2.zero;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    public void MoveHorizontally(float direction)
    {
        _velocity.Set(0f, _rigidbody2D.velocity.y);
        if (direction == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction < 0f)
        {
            _velocity.x = -_speed;
            _spriteRenderer.flipX = true;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _velocity.x = _speed;
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
            _velocity.y = -_speed;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _velocity.y = _speed;
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
    private void OnDisable()
    {
        _move = null;
    }
}
