using System;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpingPower = 10f;
    public float _speed = 1f;
    private float _speedMultiplier = 1f;
    private float _curentSpeed;
    private Action _move;
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    public SpriteRenderer _spriteRenderer;

    private Vector2 _velocity;
    public Vector2 Velocity { get; set; }
    public bool CanJump
    {
        get { return _canJump; }
        set
        {
            _canJump = value;
            if(value)
            {
                _move = MoveAndJump;
                _rigidbody.gravityScale = 1f;
            }
            else
            {
                _move = MoveNotJump;
                _rigidbody.gravityScale = 0f;
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        _move();
    }
    protected void PlayAnimation(string name)
    {
        _animator.Play(name);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        SetSpeedMultiplierForOllTime(_speedMultiplier);
    }

    virtual public void MoveHorizontally(float direction)
    {
        _velocity.Set(_curentSpeed * direction,_rigidbody.velocity.y);
        _animator.SetBool("IsRunning",direction != 0);
        if(direction != 0)
        {
            _spriteRenderer.flipX = direction < 0;
        }

    }

    public void MoveOnWire(GameObject MoveToPoint)
    {
        _rigidbody.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position,MoveToPoint.transform.position,
            Time.deltaTime * _curentSpeed);
    }

    public void MoveVertically(float direction)
    {
        _velocity.y = _curentSpeed * direction;
    }

    public void MoveBoth(Vector2 direction)
    {
        MoveHorizontally(direction.x);
        MoveVertically(direction.y);
    }

    public void StartJump()
    {
        if(CanJump)
        {
            _velocity.Set(_velocity.x,_jumpingPower);
            _animator.SetBool("IsJumping",true);
            _rigidbody.velocity = _velocity;
        }
    }

    public void SetSpeedMultiplierTemporary(float multiplier,float time = 1f)
    {
        _curentSpeed = _speed * multiplier;
        ResetSpeed();
        Invoke(nameof(SetDefaultSpeed),time);
        _animator.SetBool("Boosted",true);
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
        _animator.SetBool("Boosted",false);
    }

    private void ResetSpeed()
    {
        MoveHorizontally(_velocity.x);
        if(!CanJump)
        {
            MoveVertically(_velocity.y);
        }
    }

    private void OnEnable()
    {
        Awake();
        CanJump = CanJump;
    }

    private void MoveAndJump()
    {
        _velocity.y = _rigidbody.velocity.y;
        MoveNotJump();
    }

    private void MoveNotJump()
    {
        _rigidbody.velocity = _velocity;
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
