using System;
using System.Collections;
using UnityEngine;
public class Player : MoveBase
{
    [SerializeField] private float FlightVelicityCap = 0;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PChealth _health;
    public bool Stunned;
    private Dasher _dasher;
    [SerializeField] private float flySpeed;
    protected override void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Max(-18.527f, Mathf.Min(17.734f, transform.position.x)),
            transform.position.y, transform.position.z); 
        if(!Stunned)base.FixedUpdate();
    }

    private void Awake()
    {
        _dasher = gameObject.AddComponent<Dasher>();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    override public void MoveHorizontally(float direction)
    {
        if (!Stunned) base.MoveHorizontally(direction);
        else base.MoveHorizontally(0f);
    }

    public void Dash(float direction)
    {
        _dasher.Dash(direction);
    }
    public void Dash()
    {
        PlayAnimation("Dash");
        _dasher.Dash(_spriteRenderer.flipX ? -1 : 1);
    }
    public void Jump()
    {
        if(IsGrounded()&& !Stunned)
        {
            StartJump();
            Invoke(nameof(StopJump), 0.1f);
        }
        if(LevelUP.isTaken[15]&&_rigidbody.bodyType!=RigidbodyType2D.Static)
        {
            fly7 = true;
            StartCoroutine(fly());
            
        }
    }
    private bool fly7;
    private IEnumerator fly()
    {
        while(fly7)
        {
            if(_rigidbody.velocity.y<= FlightVelicityCap) CanJump = false;
            MoveVertically(flySpeed);
            if(!CanJump) PlayAnimation("Fly");
            yield return new WaitForFixedUpdate();
        }
        CanJump = true;
    }
    public void StopJump(bool StopFly)
    {
        if (IsGrounded())
        {
            StopJumpAnimation();
        }
        if(StopFly)
        {
            fly7= false;
        }
    }
    public void StopJump()
    {
        if(IsGrounded())
        {
            StopJumpAnimation();
        }
        else
        {
            Invoke(nameof(StopJump),0.1f);
        }
    }

    private void StopJumpAnimation()
    {
        _animator.SetBool("IsJumping",false);
    }

    public void Down()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, 1 << 10); // Тут рейкаст
        Debug.DrawLine(transform.position, hit.point);
        transform.position =
            new Vector3(transform.position.x, hit.point.y + 0.8f, transform.position.z); // Тут перемещение
        PlayAnimation("Grounded");
        Stunned = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FAK"))
        {
            _health.HealHealth(1);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("DeadZone"))
        {
            LoseGame.instance.Lose();
        }
    }
    [Serializable]
    private struct DashProperties
    {

    }
}