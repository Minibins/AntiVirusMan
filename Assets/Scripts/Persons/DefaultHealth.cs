using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultHealth : Health
{
    private Rigidbody2D rb;
    public float needVelocityForInvisibility = 9999;
    private bool IsInvisible;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Start();
    }
    public override void ApplyDamage(float damage)
    {
        if(!IsInvisible)
        {
            base.ApplyDamage(damage);
        }
    }
    protected virtual void FixedUpdate()
    {
        if(rb.velocity.magnitude < needVelocityForInvisibility)
        {
            IsInvisible = false;
            animator.SetBool("IsInvisible",false);
        }
        else
        {
            IsInvisible = true;
            animator.SetBool("IsInvisible",true);
        }
    }
}
