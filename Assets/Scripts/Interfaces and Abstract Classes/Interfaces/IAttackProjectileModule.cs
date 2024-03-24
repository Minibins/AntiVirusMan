using UnityEngine;

public interface IAttackProjectileModule
{
    public void Attack(IDamageble target,Collider2D collision);
}
