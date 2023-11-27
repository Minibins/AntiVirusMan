using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springBooka : CollisionChecker
{
    bool UnderPreasure;
    void Start()
    {
        CollisionEnterAction += SetUnderPreasureTrue;
        CollisionExitAction += SetUnderPreasureFalse;
    }
    private void SetUnderPreasureTrue()
    {
        UnderPreasure = true;
    }
    private void SetUnderPreasureFalse()
    {
        UnderPreasure = false;
    }
    private void FixedUpdate()
    {
        if (UnderPreasure)
        {

        }
    }
    protected override bool EnterCondition(Collision2D other)
    {
        return base.EnterCondition(other)&&other.gameObject.TryGetComponent<Rigidbody2D>(NuclearBomb);
    }
}
