using DustyStudios.MathAVM;

using System;
using System.Collections;
using UnityEngine;
public class Player : MoveBase, IPlayer, IHealable
{
    public bool Stunned;
    private Dasher _dasher;
    public static bool IsJump;
    private static Player instance;
    [SerializeField] private Vector2 locationBounds = new Vector2(-18.527f,17.734f);
    protected override void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Max(locationBounds.x, Mathf.Min(locationBounds.y, transform.position.x)),
            transform.position.y, transform.position.z); 
        if(!Stunned)base.FixedUpdate();
    }

    protected override void Awake()
    {
        instance = this;
        base.Awake();
        _dasher = gameObject.AddComponent<Dasher>();
        
    }
    
    public void Dash(float direction)
    {
        _dasher.Dash(direction);
    }
    public void Dash()
    {
        PlayAnimation("Dash");
        _dasher.Dash(MathA.OneOrNegativeOne(transform.localScale.x));
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
        Rigidbody.velocity = Vector2.right* Rigidbody.velocity.x;
        Stunned = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Collectible collectible;
        if (other.TryGetComponent<Collectible>(out collectible))
        {
            if(collectible.canPick)
                collectible.Pick(gameObject);
        }
    }
    public static void TakeDamage(Vector3 respawn)
    {
        instance.takeDamage(respawn);
    }
    public static void TakeDamage()
    {
        instance.takeDamage(instance.transform.position);
    }
    public void takeDamage(Vector3 respawn)
    {
        PlayDamageAnimation();
        PChealth.instance.ApplyDamage(1);
        transform.position = respawn;
    }
    [SerializeField] private float damageForce;
    private void PlayDamageAnimation()
    {
        PlayAnimation("TakeDamage");
        Rigidbody.velocity.Set(Rigidbody.velocity.x,damageForce);
    }
    public override void Jump()
    {
        IsJump = true;
        if(IsGrounded() || LevelUP.Items[15].IsTaken)
        { 
            StartJump();
            _animator.SetBool("IsJumping",true);
        }
    }
    public override void StopJump()
    {
        IsJump = false;
        base.StopJump();

        if(IsGrounded())
        {
            StopJumpAnimation();
        }
    }
    [SerializeField] private float _flightVelicityCap = 0;
    [SerializeField] private float _flySpeed;
    private bool canFly = false;
    protected override void JumpAction()
    {
        if(Rigidbody.velocity.y <= _flightVelicityCap&&LevelUP.Items[15].IsTaken && Rigidbody.bodyType != RigidbodyType2D.Static) canFly = true;
        if(canFly)
        {
            PlayAnimation("Fly");
            MoveVertically(_flySpeed);
        }
        else base.JumpAction();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(base.IsGrounded())
        {
            base.StopJump();
            StopJumpAnimation();
        }
    }
    [SerializeField] private LayerMask TreksolesLayer;
    new public bool IsGrounded()
    {
        return base.IsGrounded() || base.IsGrounded(TreksolesLayer);
    }

    public void Heal(int hp)
    {
        PChealth.instance.HealHealth(hp);
    }
}