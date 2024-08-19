using System;
using System.Collections;
using System.Linq;
using DustyStudios.MathAVM;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    [SerializeField] private bool _canJump;
    public float _speed = 1f;
    public Stat _curentSpeed = new Stat(1);
    private Action _move;
    private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody
    {
        get => _rigidbody;
    }

    protected Animator _animator;
    public new Transform transform;
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private bool isUsingRigidbody = true;
    private Vector2 _velocity;
    public Vector2 Velocity { get; set; }

    public bool CanJump
    {
        get => _canJump;
        set
        {
            _canJump = value;

            _move = value ? MoveAndJump : MoveNotJump;
            _rigidbody.gravityScale = value ? 1f : 0f;
        }
    }

    protected virtual void FixedUpdate()
    {
        _move?.Invoke();
    }

    protected void PlayAnimation(string name) => _animator.Play(name);

    private const string WalkAnimationName = "isRunning";

    protected virtual void Awake()
    {
        _curentSpeed.BaseValue = _speed;

        transform = base.transform;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        defaultXscale = transform.localScale.x;
        
        if (_animator != null && _animator.isActiveAndEnabled && gameObject.activeInHierarchy)
        {
            if (_animator.parameters.Any(a => a.name == WalkAnimationName))
                hasRunAnimation = true;
        }

    }

    private bool hasRunAnimation = false;

    private enum RotationMode
    {
        Transform,
        SpriteRenderer,
        none
    }

    [SerializeField] RotationMode rotationMode = RotationMode.SpriteRenderer;
    private float defaultXscale;

    public virtual void MoveHorizontally(float direction)
    {
        _velocity.x = _curentSpeed * direction;
        if (hasRunAnimation)
            _animator.SetBool(WalkAnimationName, direction != 0);
        if (direction != 0)
            RotateSprite();
    }

    private void RotateSprite()
    {
        if(_velocity.x == 0)
            return;
        if (_rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            switch (rotationMode)
            {
                case RotationMode.Transform:
                    transform.localScale = new Vector3(
                        defaultXscale * MathA.OneOrNegativeOne(_velocity.x < 0),
                        transform.localScale.y, 
                        transform.localScale.z);
                    break;
                case RotationMode.SpriteRenderer:
                    _spriteRenderer.flipX = _velocity.x < 0;
                    break;
            }
        }
        else
            Invoke(nameof(RotateSprite), Time.deltaTime);
    }

    public void MoveOnWire(GameObject MoveToPoint)
    {
        _rigidbody.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, MoveToPoint.transform.position,
            Time.fixedDeltaTime * _curentSpeed);
    }

    public void MoveVertically(float direction) => _velocity.y = _curentSpeed * direction;

    public void MoveBoth(Vector2 direction)
    {
        MoveHorizontally(direction.x);
        MoveVertically(direction.y);
    }

    public void SetSpeedMultiplierTemporary(float multiplier, float time = 1f)
    {
        _curentSpeed.multiplingMultiplers.Add(multiplier);
        ResetSpeed();
        StartCoroutine(resetSpeed());

        IEnumerator resetSpeed()
        {
            yield return new WaitForSeconds(time);
            _curentSpeed.multiplingMultiplers.Remove(multiplier);
            ResetSpeed();
        }
    }

    public void SetSpeedMultiplierForever(float multiplier = 1f)
    {
        _curentSpeed.multiplingMultiplers.Add(multiplier);
        ResetSpeed();
    }

    public void ClearSpeedMultiplers()
    {
        _curentSpeed.multiplingMultiplers.Clear();
        ResetSpeed();
    }

    private void ResetSpeed()
    {
        MoveHorizontally(Mathf.Clamp(_velocity.x, -1, 1));
        if (!CanJump)
        {
            MoveVertically(Mathf.Clamp(_velocity.y, -1, 1));
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
        if (isUsingRigidbody)
        {
            if (!gameObject.isStatic && _rigidbody.bodyType != RigidbodyType2D.Static)
                _rigidbody.velocity = _velocity;
        }
        else
            transform.position += (Vector3) _velocity * Time.fixedDeltaTime;

        if (!gameObject.isStatic && _rigidbody.bodyType != RigidbodyType2D.Static)
            _rigidbody.velocity += _rigidbody.totalForce;
    }
    private void OnDisable()=> _move = null;
    public void OnDrag()=> _move = null;

    public void OnDragEnd()
    {
        CanJump = CanJump;
        transform.rotation = new Quaternion();
    }

    [SerializeField] protected Transform _groundCheck, _roofCheck;
    [SerializeField] protected LayerMask _groundLayer, platformLayer;

    public bool IsGrounded()=> physics2D.OverlapCircleWithoutTrigger(_groundCheck.position, 0.2f, _groundLayer);

    public bool IsGrounded(LayerMask layerMask) => physics2D.OverlapCircleWithoutTrigger(_groundCheck.position, 0.2f, layerMask);

    public virtual void Jump()
    {
        if (IsGrounded())
            StartJump();
    }

    protected float _jumpStartTime;

    public void StartJump()
    {
        if(!CanJump) return;

        _jumpStartTime = Time.time;
        isJump = true;
        StartCoroutine(jump());
        CanJump = false;

    }

    private bool isJump;

    [SerializeField] private float _jumpingPower = 10f;
    [SerializeField] protected AnimationCurve _jumpingCurve;

    private IEnumerator jump()
    {
        do
        {
            yield return new WaitForFixedUpdate();
            JumpAction();
        } while (isJump);

        CanJump = true;
    }

    protected virtual void JumpAction() => MoveVertically(_jumpingCurve.Evaluate(Time.time - _jumpStartTime) * _jumpingPower);

    [SerializeField] private float _maxJumpLeftover = 0;

    public virtual void StopJump()
    {
        _velocity.y = Mathf.Min(_maxJumpLeftover, _rigidbody.velocity.y);
        isJump = false;
        _canJump = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(_roofCheck == null || !physics2D.OverlapCircleWithoutTrigger(_roofCheck.position,0.2f,_groundLayer ^ platformLayer))
            return;
        _jumpStartTime = Time.time - _jumpingCurve.keys[_jumpingCurve.keys.Length - 1].time / 2f;
    }
}