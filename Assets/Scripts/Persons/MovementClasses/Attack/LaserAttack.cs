using DustyStudios.MathAVM;

using UnityEngine;

public class LaserAttack : AbstractAttack
{
    [SerializeField] float spawnPosDistance;
    Animator _animator;
    
    override protected void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        if(_animator==null) _animator = GetComponentInParent<Animator>();
    }
    protected override Vector2 spawnPos => base.spawnPos+MathA.RotatedVector(spawnPosDistance*Vector2.right,UiElementsList.instance.Joysticks.Attack.Direction);
    protected override GameObject attack()
    {
        GameObject weapon = base.attack();
        weapon.transform.rotation = MathA.VectorsAngle(UiElementsList.instance.Joysticks.Attack.Direction);
        _animator.SetBool("IsChad",false);
        _animator.SetBool("Attack",false);
        return weapon;
    }
}
