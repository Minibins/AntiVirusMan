using DustyStudios.MathAVM;
using UnityEngine;

public class MakeEnemyJump : MonoBehaviour, IAttackProjectileModule
{
    [SerializeField] float Power;
    [SerializeField] PhysicsMaterial2D bouncyMaterial;
    public void Attack(IDamageble target,Collider2D collision)
    {
        Rigidbody2D rb;
        if(collision.gameObject.TryGetComponent<Rigidbody2D>(out rb))
        {
            rb.velocity += Vector2.up * Power;
            rb.sharedMaterial = bouncyMaterial;
            rb.mass = 1f;
        }
    }
}