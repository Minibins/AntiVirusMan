using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerBoolUpgrade : Upgrade
{
    [SerializeField] Animator animator;
    [SerializeField] string boolName;
    protected override void OnTake()
    {
        base.OnTake();
        animator.SetBool(boolName,true);
    }
}
