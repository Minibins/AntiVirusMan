using System;
using System.Collections;
using UnityEngine;
public class Player : MoveBase, IPlayer
{
    [SerializeField] private PChealth _health;
    public bool Stunned;
    private Dasher _dasher;
    
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


    override public void MoveHorizontally(float direction)
    {
        base.MoveHorizontally(direction);
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
        _rigidbody.velocity = Vector2.right*_rigidbody.velocity.x;
        Stunned = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FAK"))
        {
            _health.HealHealth(1);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(Vector3 respawn)
    {
        PlayDamageAnimation();
        transform.position = respawn;
    }
    [SerializeField] float damageForce;
    private void PlayDamageAnimation()
    {
        PlayAnimation("TakeDamage");
        _rigidbody.velocity.Set(_rigidbody.velocity.x,damageForce);
    }
    public override void Jump()
    {
        if(IsGrounded() || LevelUP.isTaken[15])
        { 
            StartJump();
            _animator.SetBool("IsJumping",true);
        }
    }
    public override void StopJump()
    {
        base.StopJump();

        if(IsGrounded())
        {
            StopJumpAnimation();
        }
    }
    [SerializeField] private float _flightVelicityCap = 0;
    [SerializeField] private float _flySpeed;
    bool canFly = false;
    protected override void JumpAction()
    {
        if(_rigidbody.velocity.y <= _flightVelicityCap&&LevelUP.isTaken[15] && _rigidbody.bodyType != RigidbodyType2D.Static) canFly = true;
        if(canFly)
        {
            PlayAnimation("Fly");
            MoveVertically(_flySpeed);
        }
        else base.JumpAction();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsGrounded())
        {
            StopJump();
        }
    }
}