using UnityEngine;
public class Move : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    private float _speed;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _jumpingPower = 10f;
    [SerializeField] private bool _downB;
    public bool IsDownSelected;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _velocity;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _velocity.Set(_speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = _velocity;
        if (IsGrounded() == true)
        {
            _animator.SetBool("Jump", false);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
    public void Left()
    {
        _animator.SetBool("IsRunning", true);
        _speed = -_horizontalSpeed;
        _spriteRenderer.flipX = true;
    }
    public void Rigth()
    {
        _animator.SetBool("IsRunning", true);
        _speed = _horizontalSpeed;
        _spriteRenderer.flipX = false;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpingPower);
            _animator.SetBool("Jump", true);
        }
    }

    public void Down()
    {
        if (_downB && IsDownSelected)
        {
            transform.position = new Vector3(transform.position.x, -3f, transform.position.z);
            _animator.SetBool("Jump", false);
        }
    }
    public void Stop()
    {
        _speed = 0;
        _animator.SetBool("IsRunning", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            _downB = false;
        }
        else if (!other.CompareTag("Wall"))
        {
            _downB = true;
        }
    }
}