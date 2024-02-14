using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class ExplodeAndDeath : MonoBehaviour
{
    [SerializeField] float chargeTime = 2f;
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] int _explosionPower;
    [SerializeField] bool DestroyHimself = true;
    public void Action()
    {
        Invoke(nameof(Explosion),chargeTime);
    }
    protected virtual GameObject Explosion()
    {

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
        if(DestroyHimself) 
            Destroy(gameObject);
        return explosion;
    }
}
