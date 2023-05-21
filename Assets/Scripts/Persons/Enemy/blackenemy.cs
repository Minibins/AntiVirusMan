using UnityEngine;
[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Move))]
public class blackenemy : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionRadius;
    [SerializeField] int _explosionPower;
    [SerializeField] private LayerMask _maskWhoKills;
    private Health _health;
    private void Awake()
    {
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
        move.MoveHorizontally(0f);
        Invoke(nameof(Explosion), 2f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0)
        {
            _health.ApplyDamage(_health.CurrentHealth);
        }
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