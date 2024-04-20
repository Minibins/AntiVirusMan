using UnityEngine;

public class randomAnimator : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] controllers;

    private void Start()
    {
        int spriteID = Random.Range(0, controllers.Length);
        GetComponent<Animator>().runtimeAnimatorController = controllers[spriteID];
    }
}