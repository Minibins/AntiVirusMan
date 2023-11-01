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
    [SerializeField] private bool CanBeFinishedOff;
    
    private bool dead = false;
    public GameObject MoveToPoint;
    public int ChangeMove;
    [SerializeField] private Sprite[] Finishings;

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
        if (_health.CurrentHealth > 0)
        {
            if ((_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0)
        {
            _health.ApplyDamage(_health.CurrentHealth);
        }
        else if ((collision.CompareTag("Portal") || collision.CompareTag("SecondPortal")))
        {
            _health.ApplyDamage(999);
        }
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
            if(gameObject.GetComponent<blackenemy>()==null)
            {
gameObject.GetComponent<Rigidbody2D>().gravityScale=1;
            }
            
        }
    }
    private void OnDeath()
    {
        
        if (dead) { 
            if (!CanBeFinishedOff) {
            return;
            }
            else
            {
                Destroy(gameObject, 1.10f);
                _animator.SetTrigger("Up");
            }
        }
        else
        {
_move.SetSpeedMultiplierForOllTime(0);
        _animator.SetTrigger("Die");
        
        dead = true;
        }
        Level.TakeEXP(0.5f);
        GetComponent<AttackProjectile>().Damage = 0;
    }
}