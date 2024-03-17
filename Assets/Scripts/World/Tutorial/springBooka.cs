using System.Collections;
using System.Collections.Generic;

using DustyStudios.MathAVM;

using UnityEngine;

public class springBooka : BookaCollisionChecker
{
    bool UnderPreasure;
    Rigidbody2D _rigidbody;
    Vector2 defaultPosition;
    [SerializeField] float maxOffset,rotationOfVelocity,VelocityStrengh;

    void Start()
    {
        defaultPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        EnterAction += SetUnderPreasureTrue;
        ExitAction += SetUnderPreasureFalse;
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
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            if(_rigidbody.position.y<defaultPosition.y-maxOffset)
            {
                UnderPreasure=false;
                foreach(GameObject g in EnteredThings)
                {
                    const string ppname = "EnemyBookaTutorial";
                    PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
                    g.GetComponent<Rigidbody2D>().velocity=MathA.RotatedVector( Vector2.up*VelocityStrengh,rotationOfVelocity);
                }
            }
        }
        if (!UnderPreasure)
        {
            _rigidbody.bodyType = RigidbodyType2D.Static;
            transform.position = defaultPosition;
        }
    }
    protected override bool EnterCondition(Collider2D other)
    {
        Rigidbody2D rigidbody;
        return base.EnterCondition(other)&&other.gameObject.TryGetComponent<Rigidbody2D>(out rigidbody);
    }
    protected override bool ExitCondition(Collider2D other)
    {
        List<GameObject> EnteredThingsWithoutExiter=EnteredThings;
        if(EnteredThingsWithoutExiter.Contains(other.gameObject) )
        { 
            EnteredThingsWithoutExiter.Remove(other.gameObject);
        }
        return EnteredThingsWithoutExiter.Count<=0;
    }
}
