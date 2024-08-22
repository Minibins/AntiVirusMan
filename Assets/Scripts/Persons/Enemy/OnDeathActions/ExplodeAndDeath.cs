using UnityEngine;
public class ExplodeAndDeath : SpawnAndDie
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private int _explosionPower;

    protected override GameObject Spawn()
    {
        GameObject explosion = base.Spawn();
        IExplosion Explosion;
        if(explosion.TryGetComponent<IExplosion>(out Explosion))
        {
            Explosion.Radius = _explosionRadius;
            Explosion.Power = _explosionPower;
        }
        return explosion;
    }
}