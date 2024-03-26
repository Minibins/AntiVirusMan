using System.Collections;
using System.Collections.Generic;



using UnityEngine;

public class randomAnimator : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController[] controllers;
    private void Start()
    {
        int spriteID = Random.Range(0,controllers.Length);
        GetComponent<Animator>().runtimeAnimatorController = controllers[spriteID];
    }
}
