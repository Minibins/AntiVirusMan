using DustyStudios.MathAVM;
using UnityEngine;

public class JoystickDependendAttack : AbstractAttack
{
    protected Vector2 joystickDirection => UiElementsList.instance.Joysticks.Attack.Direction;

    protected override GameObject attack()
    {
        GameObject weapon = base.attack();
        weapon.transform.rotation = MathA.VectorsAngle(joystickDirection);
        return weapon;
    }
}