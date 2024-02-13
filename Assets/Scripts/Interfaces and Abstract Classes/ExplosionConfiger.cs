using UnityEngine;
public class ExplosionConfiger : MonoBehaviour, IExplosion
{
    public float Radius
    {
        set
        {
            CapsuleCollider2D explosionCollider = GetComponent<CapsuleCollider2D>();
            explosionCollider.size =
                ((explosionCollider.direction == CapsuleDirection2D.Horizontal ? Vector2.right : Vector2.up) *
                (explosionCollider.size.y - explosionCollider.size.x))
                + (Vector2.one * value);
        }
    }
    public int Power
    {
        set
        {
            GetComponent<AttackProjectile>().Damage = value;
        }
    }


}