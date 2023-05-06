using UnityEngine;
[RequireComponent(typeof(Move))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _downB;
    public bool IsDownSelected;
    private Vector2 _velocity;
    private Move _move;
    private void Awake()
    {
        _move = GetComponent<Move>();
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
    public void Left()
    {
        _move.MoveHorizontally(-1f);
    }
    public void Rigth()
    {
        _move.MoveHorizontally(1f);
    }
    public void Stop()
    {
        _move.MoveHorizontally(0f);
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            _move.StartJump();
            Invoke(nameof(StopJump), 0.1f);
        }
    }
    private void StopJump()
    {
        if (IsGrounded())
        {
            _move.StopJump();
        }
        else
        {
            Invoke(nameof(StopJump), 0.1f);
        }
    }



    public void Down()
    {
        if (_downB && IsDownSelected)
        {
            transform.position = new Vector3(transform.position.x, -3f, transform.position.z);
            _move.StopJump();
        }
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