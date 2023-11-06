using UnityEngine;

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Move)),
 RequireComponent(typeof(Animator)),
 RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    private enum EnemyTypes
    {
        Soplik,
        Stepa,
        Booka,
        Toocha
    }
    [SerializeField] private EnemyTypes WhoAmI;
    [SerializeField] private LayerMask _maskWhoKills;
    public float moveDirection;
    private Health _health;
    private Move _move;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool dead = false;
    public int ChangeMove;
    public GameObject MoveToPoint;
    public GameObject _PC;
    public static bool isDraggable;
    public static bool AntivirusHaveBoots;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _move = GetComponent<Move>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        if (isDraggable)
        {
            gameObject.AddComponent<DRAG>();
        }
        if(AntivirusHaveBoots)
        {
           AddBookaComponent();
        }
    }
    public void AddBookaComponent()
    {   if(WhoAmI == EnemyTypes.Booka) { return; }
       AboveDeath MyDeath= gameObject.AddComponent<AboveDeath>();
        if(WhoAmI==EnemyTypes.Soplik)
        {
            MyDeath.ForcerePulsive = 500;
        }
        else if(WhoAmI == EnemyTypes.Toocha)
        {
            MyDeath.ForcerePulsive = 1000;
        }
        MyDeath.IsPlatform = true;
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
                moveDirection = -1f;
                _move.MoveHorizontally(moveDirection);
            }
            else
            {
                moveDirection = 1f;
                _move.MoveHorizontally(moveDirection);
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
            MoveToPoint = other.gameObject.GetComponent<WayPoint>()
                .nextPoint[Random.Range(0, other.gameObject.GetComponent<WayPoint>().nextPoint.Length)];
        }
        else if (other.CompareTag("EndWire"))
        {
            ChangeMove = 0;
            if (gameObject.GetComponent<blackenemy>() == null)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }

    private void OnDeath()
    {
        if (dead)
        {
            if (WhoAmI!=EnemyTypes.Soplik)
            {
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