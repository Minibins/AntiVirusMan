using UnityEngine;

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(Move))]
public class blackenemy : Enemy
{
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] private float otklonenieOtklonenia;
    [SerializeField] private float SkorostOtklonenia;
    [SerializeField] private float Speed;
    [SerializeField] int _explosionPower;
    private float otklonenie;



    new protected private void Awake()
    {
        _PC = GameObject.FindGameObjectWithTag("PC");
        _move = GetComponent<Move>();
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        if(isDraggable)
        {
            gameObject.AddComponent<DRAG>();
        }
        if(AntivirusHaveBoots)
        {
            AddBookaComponent();
        }
    }

    new private void OnEnable()
    {
        _health.OnDeath += ExplosionInvoke;
        base.OnEnable();
    }

    new private void OnDisable()
    {
        _health.OnDeath -= ExplosionInvoke;
        base.OnDisable();
    }

    private void ExplosionInvoke()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        Invoke(nameof(Explosion), 2f);
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

        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}