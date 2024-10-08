using System;
using DustyStudios.MathAVM;
using UnityEngine;

public class EnemyPusher : MonoBehaviour, IAttackProjectileModule
{
    [SerializeField] float Power;
    [SerializeField] LayerMask layer;

    public void Attack(IDamageble target, Collider2D collision)
    {
        MoveBase move;
        if (collision.gameObject.TryGetComponent<MoveBase>(out move))
        {
            move.Rigidbody.velocity = new Vector2( MathA.OneOrNegativeOne(GetComponent<SpriteRenderer>().flipX) * Power *
                (1 + Convert.ToInt16(collision.IsTouchingLayers(layer))+ move.Rigidbody.velocity.x),move.Rigidbody.velocity.y);
        }
    }
}