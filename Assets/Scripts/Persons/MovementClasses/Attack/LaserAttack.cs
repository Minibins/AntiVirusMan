using DustyStudios.MathAVM;
using UnityEngine;

public class LaserAttack : JoystickDependendAttack
{
    [SerializeField] private float spawnPosDistance;

    protected override Vector2 spawnPos => base.spawnPos + MathA.RotatedVector(spawnPosDistance * Vector2.right,
        UiElementsList.instance.Joysticks.Attack.Direction);

    protected override GameObject attack()
    {
        playerAttack.Animator.SetBool("IsChad", false);
        playerAttack.Animator.SetBool("Attack", false);
        return base.attack();
    }
}