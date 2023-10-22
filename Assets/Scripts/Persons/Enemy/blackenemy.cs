using UnityEngine;
[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Move))]
public class blackenemy : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    private GameObject _PC;
    [SerializeField] private float otklonenieOtklonenia;
    [SerializeField] private float SkorostOtklonenia;
    [SerializeField] private float Speed;
    private Move _move;
    private float otklonenie;
   

    [SerializeField] int _explosionPower;
    [SerializeField] private LayerMask _maskWhoKills;
    private Health _health;
    private void Awake()
    {
        _PC = GameObject.FindGameObjectWithTag("PC");
        _move = GetComponent<Move>();
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _health.OnDeath += OnDeath;
    }
    private void OnDisable()
    {
        _health.OnDeath -= OnDeath;
    }
    private void OnDeath()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        Move move = GetComponent<Move>();
        move.CanJump = true;
        move.MoveHorizontally(1f);
        Invoke(nameof(Explosion), 2f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0)
        {
            _health.ApplyDamage(_health.CurrentHealth);
        }
    }
    private void Update()
    {
        Fly();
        otklonenie++;
    }
    private void Fly()
    {
        Vector3 FlyVector = transform.position-_PC.transform.position;
        FlyVector.Normalize();
        FlyVector.x += otklonenieOtklonenia * Mathf.Sin(SkorostOtklonenia * otklonenie); ;
        FlyVector *= Speed;
        transform.position += FlyVector;
    }
    private void Explosion()
    {
        Destroy(gameObject);
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
        Health health;
        foreach (var target in targets)
        {
            if (target.TryGetComponent<Health>(out health))
            {
                health.ApplyDamage(_explosionPower);
            }
        }
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}