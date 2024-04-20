using UnityEngine;

public class Yasha : TagCollisionChecker
{
    private Animator anim;
    private Move move;

    private void Start()
    {
        move = GetComponent<Move>();
        anim = GetComponent<Animator>();
        EnterAction += OnUpdateEnteredThings;
        ExitAction += OnUpdateEnteredThings;
    }

    private void OnUpdateEnteredThings()
    {
        if (EnteredThings.Count > 0)
            move.SetSpeedMultiplierForever(0);
        else move.ClearSpeedMultiplers();
        anim.SetBool("IsPushed", EnteredThings.Count > 0);
    }
}