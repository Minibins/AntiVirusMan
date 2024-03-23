using DustyStudios.MathAVM;

using UnityEngine;

public class EnemyPusher : MonoBehaviour, IAttackProjectileModule
{
    [SerializeField] float Power;
    public void Attack(IDamageble target,Collider2D collision)
    {
        MoveBase move;
        if(collision.gameObject.TryGetComponent<MoveBase>(out move))
        {
            move.Rigidbody.velocity = new( MathA.OneOrNegativeOne(GetComponent<SpriteRenderer>().flipX)*Power,move.Rigidbody.velocity.y);
        }
    }
}
