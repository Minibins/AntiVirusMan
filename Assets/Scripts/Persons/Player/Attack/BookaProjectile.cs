using UnityEngine;

public class BookaProjectile : MonoBehaviour, IAttackProjectileModule
{
    public void Attack(IDamageble target, Collider2D collision)
    {
        DebuffBank debuffBank;
        if (collision.TryGetComponent<DebuffBank>(out debuffBank))
            debuffBank.AddDebuff(new BookaDebuff());
    }
}