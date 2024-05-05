using UnityEngine;

public class JudjeExplosion : ExplodeAndDeath
{
    [SerializeField] private GameObject egg;
    protected override GameObject Explosion()
    {
        GameObject explosion = PC.Carma>6 ? Instantiate(egg, transform.position, Quaternion.identity) : base.Explosion();
        Destroy();
        ExpCollectible exp;
        if(explosion.TryGetComponent<ExpCollectible>(out exp)) exp.Exp = PC.Carma;
        return explosion;
    }
}
