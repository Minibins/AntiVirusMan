using System.Linq;
using DustyStudios.MathAVM;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(SpriteRenderer))]
public class Move : MoveBase, iDraggable
{
    [SerializeField] private float AutojumpRange, AutojumpHeight;
    [SerializeField] private bool Autojump;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Autojump && CanJump)
        {
            if (Physics2D.Raycast(transform.position + Vector3.up * AutojumpHeight,
                Vector2.right * MathA.OneOrNegativeOne(Rigidbody.velocity.x), AutojumpRange, _groundLayer))
            {
                Jump();
                Debug.DrawRay(transform.position + Vector3.up * AutojumpHeight,
                    Vector2.right * MathA.OneOrNegativeOne(Rigidbody.velocity.x) * AutojumpRange);
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

        if (Time.time - _jumpStartTime > _jumpingCurve.keys.OrderBy(k => k.time).LastOrDefault().time)
        {
            StopJump();
        }
    }
}