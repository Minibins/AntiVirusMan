using UnityEngine;

public class JudjeExplosion : SpawnAndDie
{
    [SerializeField] private GameObject egg;
    protected override GameObject Spawn()
    {
        if(PC.Carma < 3)
        {
            Destroy();
            return null;
        }
        GameObject explosion = EasterEgg.isEaster ? Instantiate(egg, transform.position, Quaternion.identity) : base.Spawn();
        ExpCollectible exp;
        if(explosion.TryGetComponent<ExpCollectible>(out exp)) exp.Exp = PC.Carma;
        return explosion;
    }
}
