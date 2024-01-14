using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractConduitionDoor : PlayersCollisionChecker
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        CollisionEnterAction += () => OpenOrClose(true);
        CollisionExitAction += () => OpenOrClose(false);
    }
    void OpenOrClose(bool Open)
    {
        if(Conduition()) return;
        animator.SetBool("Open",Open);
    }

    virtual protected bool Conduition()
    {
        return true;
    }
}
