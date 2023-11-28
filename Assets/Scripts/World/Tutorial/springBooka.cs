using System.Collections;
using System.Collections.Generic;

using MathAVM;

using UnityEngine;

public class springBooka : BookaCollisionChecker
{
    bool UnderPreasure;
    Rigidbody2D rigidbody;
    Vector2 defaultPosition;
    [SerializeField] float maxOffset,rotationOfVelocity,VelocityStrengh;

    void Start()
    {
        defaultPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
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
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            if(rigidbody.position.y<defaultPosition.y-maxOffset)
            {
                UnderPreasure=false;
                foreach(GameObject g in EnteredThings)
                {
                    g.GetComponent<Rigidbody2D>().velocity=MathA.RotatedVector( Vector2.up*VelocityStrengh,rotationOfVelocity);
                }
            }
        }
        if (!UnderPreasure)
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            transform.position = defaultPosition;
        }
    }
    protected override bool EnterCondition(Collision2D other)
    {
        Rigidbody2D rigidbody;
        return base.EnterCondition(other)&&other.gameObject.TryGetComponent<Rigidbody2D>(out rigidbody);
    }
    protected override bool ExitCondition(Collision2D other)
    {
        List<GameObject> EnteredThingsWithoutExiter=EnteredThings;
        if(EnteredThingsWithoutExiter.Contains(other.gameObject) )
        { 
            EnteredThingsWithoutExiter.Remove(other.gameObject);
        }
        return EnteredThingsWithoutExiter.Count<=0;
    }
}
