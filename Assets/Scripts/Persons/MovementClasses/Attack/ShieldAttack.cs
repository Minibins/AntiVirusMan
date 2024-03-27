using DustyStudios.MathAVM;

using UnityEngine;

public class ShieldAttack : AbstractAttack
{
    protected SpriteRenderer _spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }
    protected override GameObject attack()
    {
        GameObject _weapon = base.attack();
        _weapon.GetComponent<SpriteRenderer>().flipX = transform.lossyScale.x<0;
        _weapon.GetComponent<AttackProjectile>().Damage = playerAttack.Damage;
        print(playerAttack.Damage.Value);
        return _weapon;
    }
}
