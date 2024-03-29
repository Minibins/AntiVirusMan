using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class ExplodeAndDeath : MonoBehaviour, IScannable
{
    [SerializeField] float chargeTime = 2f;
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] int _explosionPower;
    [SerializeField] bool DestroyHimself = true, OnScan=true;
    public void Action()
    {
        Invoke(nameof(Explosion),chargeTime);
    }

    public void EndScan()
    {
        if(OnScan)
        Explosion();
    }

    public void StartScan()
    {
    }
    public void StopScan()
    {
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
