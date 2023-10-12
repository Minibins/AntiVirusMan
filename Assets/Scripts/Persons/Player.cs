﻿using UnityEngine;
[RequireComponent(typeof(Move))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _downB;
    [SerializeField] private Health _health;
    public bool IsDownSelected;
    private Vector2 _velocity;
    private Move _move;
    /*
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 5);
        Debug.DrawRay(transform.position, Vector2.down * 5, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.magenta);
        }
    }
    */

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
        if (!_downB && IsDownSelected)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, 1 << 10); // Тут рейкаст

            if (hit.collider != null)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y-1, transform.position.z); // Тут перемещение
            }

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            _downB = false;
        }
        else {
            _downB = true;
            print(other);
        }
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