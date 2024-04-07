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
        if(EnteredThings.Count > 0) 
        move.SetSpeedMultiplierForever(0);
        else move.ClearSpeedMultiplers();
        anim.SetBool("IsPushed",EnteredThings.Count > 0);
    }
}