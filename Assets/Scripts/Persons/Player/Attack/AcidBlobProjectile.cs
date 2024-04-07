using UnityEngine;

public class AcidBlobProjectile : MonoBehaviour, IAttackProjectileModule
{
    public void Attack(IDamageble target,Collider2D collision)
    {
        DebuffBank debuffBank;
        if(collision.TryGetComponent<DebuffBank>(out debuffBank) && !debuffBank.HasDebuffOfType(typeof(AcidDebuff)))
        {
            debuffBank.AddDebuff(new AcidDebuff());
            target.OnDamageGet(1,IDamageble.DamageType.Default);
        }
    }
}
