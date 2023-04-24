using UnityEngine;
public class Move : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float HorizontalSpeed;
    [SerializeField] private float jumpingPower = 10f;
    [SerializeField] private bool DownB;
    public bool IsDownSelected;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        IsGrounded();
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (IsGrounded() == true)
        {
            anim.SetBool("Jump", false);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public void Left()
    {
        anim.SetBool("IsRunning", true);
        speed = -HorizontalSpeed;
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    public void Rigth()
    {
        anim.SetBool("IsRunning", true);
        speed = HorizontalSpeed;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("Jump", true);
        }
    }

    public void Down()
    {
        if (DownB && IsDownSelected)
        {
            transform.position = new Vector3(transform.position.x, -3f, transform.position.z);
            anim.SetBool("Jump", false);
        }
    }
    public void Stop()
    {
        speed = 0;
        anim.SetBool("IsRunning", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            DownB = false;
        }
        else if (!other.CompareTag("Wall"))
        {
            DownB = true;
        }
    }
}