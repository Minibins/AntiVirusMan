using System;

using UnityEngine;

public class Yasha : CollisionChecker
{
    [SerializeField] LayerMask PusherMask;
    protected override bool EnterCondition(Collider2D other)
    {
        return Condition(other);
    }
    protected override bool ExitCondition(Collider2D other)
    {
        return Condition(other);
    }
    protected override bool StayCondition(Collider2D other)
    {
        return false;
    }
    bool Condition(Collider2D other)
    {
        return (PusherMask.value & (1 << other.gameObject.layer)) != 0;
    }

    Animator anim;
    Move move;
    void Start()
    {
        move = GetComponent<Move>();
        anim = GetComponent<Animator>();
        EnterAction += OnUpdateEnteredThings;
        ExitAction += OnUpdateEnteredThings;
    }
    void OnUpdateEnteredThings()
    {
        move.SetSpeedMultiplierForOllTime(Convert.ToInt16(EnteredThings.Count == 0));
        anim.SetBool("IsPushed",EnteredThings.Count > 0);
    }
}