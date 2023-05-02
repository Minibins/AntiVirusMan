using UnityEngine;
[RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(SpriteRenderer))]
public class Move : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _velocity = Vector2.zero;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void MoveHorizontally(float direction)
    {
        _velocity.Set(direction, _rigidbody2D.velocity.y);
        if (direction == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else if (direction < 0f)
        {
            _spriteRenderer.flipX = true;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _spriteRenderer.flipX = false;
            _animator.SetBool("IsRunning", true);
        }
        _rigidbody2D.velocity = _velocity;
    }
    public void MoveVertically(float direction)
    {
        _velocity.Set(_rigidbody2D.velocity.x, direction);
        if (direction == 0f)
        {
            _animator.SetBool("IsRunning", false);
        }
        else
        {
            _animator.SetBool("IsRunning", true);
        }

        _rigidbody2D.velocity = _velocity;
    }
    public void StartJump(float power)
    {
        _velocity.Set(_rigidbody2D.velocity.x, power);
        _animator.SetBool("IsJumping", true);
        _rigidbody2D.velocity = _velocity;
    }
    public void StopJump()
    {
        _animator.SetBool("IsJumping", false);
    }

}
