using DustyStudios.MathAVM;

using System;
using System.Linq;

using UnityEngine;
[RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(Animator)),
 RequireComponent(typeof(SpriteRenderer))]
public class Move : MoveBase, iDraggable
{
    [SerializeField] float AutojumpRange, AutojumpHeight;
    [SerializeField] bool Autojump;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Autojump&& CanJump)
        {
            if(Physics2D.Raycast(transform.position + Vector3.up * AutojumpHeight,Vector2.right * MathA.OneOrNegativeOne(_rigidbody.velocity.x),AutojumpRange, _groundLayer))
            {
                Jump();
                Debug.DrawRay(transform.position + Vector3.up * AutojumpHeight,Vector2.right * MathA.OneOrNegativeOne(_rigidbody.velocity.x) * AutojumpRange);
            }
            else
            {
                StopJump();
            }
        }
    }
    protected override void JumpAction()
    {
        base.JumpAction();

        if(Time.time - _jumpStartTime > _jumpingCurve.keys.OrderBy(k =>k.time).LastOrDefault().time)
        {
            StopJump();
        }
    }
}