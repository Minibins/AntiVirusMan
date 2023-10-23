using UnityEngine;
[RequireComponent(typeof(Move))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Health _health;
    private Vector2 _velocity;
    private Move _move;
    
   /* private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 5);
        Debug.DrawRay(transform.position, Vector2.down * 5, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.magenta);
        }
    }*/

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, 1 << 10); // Тут рейкаст
        Debug.DrawLine(transform.position, hit.point);
            transform.position = new Vector3(transform.position.x, hit.point.y+0.8f, transform.position.z); // Тут перемещение
        _move.PlayAnimation("Grounded");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FAK")){
            _health.HealHealth(1);
            Destroy(other.gameObject);
        }
                else {
            print(other);
        }
    }
}