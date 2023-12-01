using System;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(Animator)),
 RequireComponent(typeof(SpriteRenderer))]
public class Move : MonoBehaviour, Draggable
{
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpingPower = 10f;
    public float _speed = 1f;
    private float _speedMultiplier = 1f;
    private float _curentSpeed;
    private Action _move;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public SpriteRenderer _spriteRenderer;

    private Vector2 _velocity;
    public Vector2 Velocity { get { return _velocity; } }

    public bool CanJump
    {
        get { return _canJump; }
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

    public void PlayAnimation(string name)
    {
        _animator.Play(name);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SetSpeedMultiplierForOllTime(_speedMultiplier);
    }

    private void FixedUpdate()
    {
        if (_speedMultiplier != 0) _move();
    }

    public void MoveHorizontally(float direction)
    {
        _velocity.Set(0f, _rigidbody2D.velocity.y);
        _velocity.x = _curentSpeed * direction;
        _animator.SetBool("IsRunning",direction != 0);
        if(direction!=0)
        {
            _spriteRenderer.flipX = direction < 0;
        }
        
    }

    public void MoveOnWire(GameObject MoveToPoint)
    {
        _rigidbody2D.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, MoveToPoint.transform.position,
            Time.deltaTime * _curentSpeed);
    }

    public void MoveVertically(float direction)
    {
        _velocity.Set(_velocity.x, direction);
        if (direction == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction < 0f)
        {
            _velocity.y = direction;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _velocity.y = direction;
            _animator.SetBool("IsRunning", true);
        }
    }

    public void MoveBoth(Vector2 direction)
    {
        _velocity.Set(_rigidbody2D.velocity.x, 0f);
        if (direction.y == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction.y < 0f)
        {
            _velocity.y = -_curentSpeed;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _velocity.y = _curentSpeed;
            _animator.SetBool("IsRunning", true);
        }

        _velocity.Set(0f, _rigidbody2D.velocity.y);
        if (direction.x == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction.x < 0f)
        {
            _velocity.x = -_curentSpeed;
            ;
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
        _animator.SetBool("Boosted", true);
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
        _animator.SetBool("Boosted", false);
    }

    private void ResetSpeed()
    {
        MoveHorizontally(_velocity.x);
        if (!CanJump)
        {
            MoveVertically(_velocity.y);
        }
    }

    private void OnEnable()
    {
        CanJump = CanJump;
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

    public void OnDrag()
    {
        _move = SetDefaultSpeed;
    }

    public void OnDragEnd()
    {
        CanJump = CanJump;
        transform.rotation = new Quaternion();
    }
}