using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDoor : PlayersCollisionChecker
{
    Animator animator;
    [SerializeField] int ExpReqired;
    void Start()
    {
        animator = GetComponent<Animator>();
        CollisionEnterAction += () => OpenOrClose(true);
        CollisionExitAction += () => OpenOrClose(false);
    }
    void OpenOrClose(bool Open)
    {
        if(Level.EXP < ExpReqired) return;
        animator.SetBool("Open", Open);
    }
}