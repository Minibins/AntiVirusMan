using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class ExplodeOnDeath : AbstractOnDeathAction
{
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] int _explosionPower;
    protected override void Action()
    {
        Invoke(nameof(Explosion),2f);
    }
    private void Explosion()
    {
        Destroy(gameObject);

        GameObject explosion= Instantiate(_explosion, transform.position, Quaternion.identity);
        if(GetComponent<Enemy>().isElite)
        {
            explosion.GetComponent<ExpCollectible>().Exp = PC.Carma;
        }
        else
        {
            CapsuleCollider2D explosionCollider = explosion.GetComponent<CapsuleCollider2D>();
            explosionCollider.size = 
                (explosionCollider.direction==CapsuleDirection2D.Vertical ? Vector2.right : Vector2.up * 
                (explosionCollider.size.y - explosionCollider.size.x))
                +(Vector2.one*_explosionRadius);

            explosion.GetComponent<AttackProjectile>().Damage = _explosionPower;
        }
    }
}
