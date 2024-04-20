using UnityEngine;

public class AbstractConduitionDoor : PlayersCollisionChecker
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnterAction += () => OpenOrClose(true);
        ExitAction += () => OpenOrClose(false);
    }

    private void OpenOrClose(bool Open)
    {
        if (!Conduition()) return;
        animator.SetBool("Open", Open);
    }

    protected virtual bool Conduition()
    {
        return true;
    }
}