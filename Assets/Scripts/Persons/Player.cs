﻿using System.Collections;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Move))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Health _health;
    [SerializeField] private float dashRange;
    private PlayerAttack playerAttack;
    private bool Stunned;
    private Vector2 _velocity;
    private Move _move;
    private Rigidbody2D _rb;
    
    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Max(-18.527f,Mathf.Min(17.734f,transform.position.x)),transform.position.y,transform.position.z);
    }

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        _rb = GetComponent<Rigidbody2D>();
        _move = GetComponent<Move>();
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
    public void Left()
    {
        if(!Stunned)_move.MoveHorizontally(-1f);
        
        
    }
    public void Dash() 
    {
        if (playerAttack.Ammo<0)
        {
            return;
        }
        playerAttack.Ammo--;
        int direction;
        if (_move._spriteRenderer.flipX)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*direction, dashRange, 1 << 10); // Тут рейкаст
        if(hit) {
            transform.position = new Vector3(hit.point.x+-0.65f * direction * 0.5f, transform.position.y, transform.position.z); // Тут перемещение
        }
        else
        {
            transform.position += Vector3.right * direction * dashRange*0.5f;
        }
        _move.PlayAnimation("Dash");
        StartCoroutine(DashEnd(direction));
    }
    IEnumerator DashEnd(int direction) 
    { Stunned = true;
        playerAttack.enabled = false;
        _move.enabled=false;
        
    for(int a  = 0; a <= 10; a++)
        {
            _rb.velocity=new Vector2(direction * (dashRange*5-a/2*dashRange)*10,0);
            yield return new WaitForFixedUpdate();
        }
        _move.enabled = true;
        playerAttack.enabled = true;
        Stunned = false;
    }
    public void Rigth()
    {
        if (!Stunned) _move.MoveHorizontally(1f);
    }
    public void Stop()
    {
         _move.MoveHorizontally(0f);
    }
    public void Jump()
    {
        if (IsGrounded()&&!Stunned)
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
        Stunned = false;
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