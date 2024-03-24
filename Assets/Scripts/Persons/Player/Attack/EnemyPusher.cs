using DustyStudios.MathAVM;

using System;

using UnityEngine;

public class EnemyPusher : MonoBehaviour, IAttackProjectileModule
{
    [SerializeField] float Power;
    [SerializeField] LayerMask layer;
    public void Attack(IDamageble target,Collider2D collision)
    {
        MoveBase move;
        if(collision.gameObject.TryGetComponent<MoveBase>(out move))
        {
            print(collision.IsTouchingLayers(layer));
            move.Rigidbody.velocity = new( MathA.OneOrNegativeOne(GetComponent<SpriteRenderer>().flipX)*Power*(1+Convert.ToInt16(collision.IsTouchingLayers(layer))),move.Rigidbody.velocity.y);
        }
    }
}
