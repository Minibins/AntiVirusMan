using System;
using System.Collections;
using System.Linq;

using Unity.Collections;
using Unity.Mathematics;

using UnityEngine;
public class Player : MoveBase
{
    [SerializeField] private float FlightVelicityCap = 0;
    [SerializeField] private PChealth _health;
    public bool Stunned;
    [SerializeField] private float flySpeed;
    protected void Update()
    {
        transform.position = new Vector3(Mathf.Max(-18.527f, Mathf.Min(17.734f, transform.position.x)),
            transform.position.y, transform.position.z); 

        if(!IsGrounded() && !IsJump && !IsFly &&!Stunned) PlayAnimation("Fall");
    }
    private void Awake()
    {
       // _jumpEvents.Add(10,() => JumpAnimationStage(1));
        _jumpEvents.Add((int)JumpPower,() => JumpAnimationStage(2));
        _jumpEvents.Add(1 ,() => JumpAnimationStage(3));
        _jumpEvents.Add(-2,() => JumpAnimationStage(4));
        _jumpEvents.Add((int)FlightVelicityCap, StartFly);
        _jumpEvents.Add(-8,() => JumpAnimationStage(0));
        _dasher = gameObject.AddComponent<Dasher>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FAK"))
        {
            _health.HealHealth(1);
            Destroy(other.gameObject);
        }
    }
    private void JumpAnimationStage(int stage) => _animator.SetInteger("JumpStage", stage);
    private void StartFly()
    {
        if(!LevelUP.isTaken[15]) return;
        PlayAnimation("Fly");
        JumpAnimationStage(1);
        CanJump = false;
        StopJump();
        IsFly = true;
        StartCoroutine(Fly());
    }
    private bool IsFly;
    private IEnumerator Fly()
    {
        while(IsFly)
        {
            MoveVertically(flySpeed);
            yield return new WaitForFixedUpdate();
        }
    }
    public void Jump()
    {
        base.StartJump();
        if(_rigidbody.velocity.y<FlightVelicityCap) StartFly();
    }
    public void StopJump(bool force)
    {
        StopJump();
        JumpAnimationStage(0);
        CanJump = true;
        IsFly = false;
    }
    override public void MoveHorizontally(float direction)
    {
        base.MoveHorizontally(direction);
    }
    //Деш
    #region
    private Dasher _dasher;
    public void Dash(float direction)
    {
        _dasher.Dash(direction);
    }
    public void Dash()
    {
        PlayAnimation("Dash");
        _dasher.Dash(_spriteRenderer.flipX ? -1 : 1);
    }
    #endregion
    //Пике
    public void Down()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, _groundLayer); // Тут рейкаст
        Debug.DrawLine(transform.position, hit.point);
        transform.position =
            new Vector3(transform.position.x, hit.point.y + 0.8f, transform.position.z); // Тут перемещение
        PlayAnimation("Grounded");
        _rigidbody.velocity = Vector2.right*_rigidbody.velocity.x;
        Stunned = false;
    }

    //Дамаг
    #region
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
    #endregion
}