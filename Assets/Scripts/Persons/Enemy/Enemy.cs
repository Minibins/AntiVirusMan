using UnityEngine;
[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Move)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _maskWhoKills;
    public GameObject _PC;
    private Health _health;
    private Move _move;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public GameObject MoveToPoint;
    public int ChangeMove;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _move = GetComponent<Move>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _PC = GameObject.FindGameObjectWithTag("PC");

    }
    private void FixedUpdate()
    {
        EnemyMove();

    }

    private void OnEnable()
    {
        _health.OnDeath += OnDeath;
    }
    private void OnDisable()
    {
        _health.OnDeath -= OnDeath;
    }
    private void EnemyMove()
    {
        if (ChangeMove == 0)
        {
            if (_PC.transform.position.x < transform.position.x)
            {
                _move.MoveHorizontally(-1f);
            }
            else
            {
                _move.MoveHorizontally(1f);
            }
        }
        else if (ChangeMove == 1)
        {
            _move.MoveOnWire(MoveToPoint);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0)
        {
            _health.ApplyDamage(_health.CurrentHealth);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Portal") || collision.CompareTag("SecondPortal"))
        {
            _health.ApplyDamage(999);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WayPoint"))
        {
            MoveToPoint = other.gameObject.GetComponent<WayPoint>().nextPoint[Random.Range(0, other.gameObject.GetComponent<WayPoint>().nextPoint.Length)];
        }
        else if (other.CompareTag("EndWire"))
        {
            ChangeMove = 0;
        }
    }
    private void OnDeath()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        _animator.SetTrigger("Die");
        Level.TakeEXP(0.5f);
    }
}