using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class ExplodeAndDeath : MonoBehaviour
{
    [SerializeField] float chargeTime = 2f;
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] int _explosionPower;
    public void Action()
    {
        Invoke(nameof(Explosion),chargeTime);
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
            IExplosion Explosion = explosion.GetComponent<IExplosion>();
            Explosion.Radius = _explosionRadius;
            Explosion.Power = _explosionPower;
        }
    }
}
