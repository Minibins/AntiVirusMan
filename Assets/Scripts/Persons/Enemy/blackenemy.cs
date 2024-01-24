using UnityEngine;

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Rigidbody2D))]
public class blackenemy : Enemy
{
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] private float otklonenieOtklonenia;
    [SerializeField] private float SkorostOtklonenia;
    [SerializeField] private float Speed;
    [SerializeField] int _explosionPower;
    private float otklonenie;
    [SerializeField] private bool isElite;

    new protected private void Awake()
    {
        if(isElite)
        {
            PC.Carma = 7.5f;
        }
        _PC = GameObject.FindGameObjectWithTag("PC");
        _move = GetComponent<MoveBase>();
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        if(LevelUP.Items[14].IsTaken)
        {
            gameObject.AddComponent<DRAG>();
        }
        if(LevelUP.Items[16].IsTaken)
        {
            AddBookaComponent();
        }
    }

    new private void OnEnable()
    {
        _health.OnDeath += ExplosionInvoke;
        onComputerReach += ExplosionInvoke;
        base.OnEnable();
        if(isElite)
        {
            _health.OnDeath += CarmaSetZero;
        }
    }

    new private void OnDisable()
    {
        _health.OnDeath -= ExplosionInvoke;
        onComputerReach -= ExplosionInvoke;
        base.OnDisable();
        if(isElite)
        {
            _health.OnDeath -= CarmaSetZero;
        }
    }
    private void ExplosionInvoke()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        Invoke(nameof(Explosion), 2f);
    }
    private void CarmaSetZero()
    {
        PC.Carma = 0;
    }

    override protected private void EnemyMove()
    {
        Vector3 FlyVector = transform.position - _PC.transform.position;
        FlyVector.Normalize();
        FlyVector.x += otklonenieOtklonenia * Mathf.Sin(SkorostOtklonenia * otklonenie);
        ;
        FlyVector *= Speed;
        transform.position += FlyVector;
        otklonenie++;
    }

    private void Explosion()
    {
        Destroy(gameObject);

        GameObject explosion= Instantiate(_explosion, transform.position, Quaternion.identity);
        if(isElite)
        {
            explosion.GetComponent<ExpCollectible>().Exp = PC.Carma;
        }
    }
}