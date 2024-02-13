using System;

using UnityEngine;

public class Yasha : TagCollisionChecker
{
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