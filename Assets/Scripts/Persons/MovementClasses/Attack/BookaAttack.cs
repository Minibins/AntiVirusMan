using UnityEngine;

public class BookaAttack : JoystickDependendAttack
{
    [SerializeField] float Force;
    protected override GameObject attack()
    {
        GameObject weapon = base.attack();
        weapon.GetComponent<Rigidbody2D>().velocity = joystickDirection*Force;
        playerAttack.Animator.Play("ThrowThing");
        return weapon;
    }
}