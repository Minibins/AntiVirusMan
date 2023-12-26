using System;

using UnityEngine;
public enum EnemyTypes
    {
        [EnemyTypesAttributes(0,0,0)]
        Soplik,
        [EnemyTypesAttributes(5,0,0)]
        Stepa,
        [EnemyTypesAttributes(0,5,0)]
        Booka,
        [EnemyTypesAttributes(-5,0,0)]
        Toocha
    }
public class EnemyTypesAttributes:Attribute
{
    private Vector3Int position;
    public EnemyTypesAttributes(int x,int y,int z)
    {
        this.position = new Vector3Int(x,y,z);
    }
}
[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Animator)),
 RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    
    [SerializeField] private EnemyTypes WhoAmI;
    [SerializeField] private LayerMask _maskWhoKills;
    public float moveDirection;
    private protected Health _health;
    private protected MoveBase _move;
    private SpriteRenderer _spriteRenderer;
    private protected Animator _animator;
    private bool dead = false;
    public int ChangeMove;
    public GameObject MoveToPoint;
    public GameObject _PC;
    protected Action onComputerReach;
    virtual private protected void Awake()
    {
        _health = GetComponent<Health>();
        _move = GetComponent<MoveBase>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        if(LevelUP.isTaken[14])
        {
            gameObject.AddComponent<DRAG>();
        }
        if(LevelUP.isTaken[16])
        {
           AddBookaComponent();
        }
        if (LevelUP.isTaken[18])
        {
            _health.AddMaxHealth(-1);
            _animator.Play("Wire");
            
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
        if(ChangeMove == 1)
        {
            try
            {
                _animator.Play("Wire");
            }
            catch
            {

            }
        }
        _PC = GameObject.FindGameObjectWithTag("PC");
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }


    private protected void OnEnable()
    {
        _health.OnDeath += OnDeath;
        onComputerReach += OnDeath;
    }

    private protected void OnDisable()
    {
        _health.OnDeath -= OnDeath;
        onComputerReach -= OnDeath;
    }

    virtual private protected void EnemyMove()
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
            if ((_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0&&!dead)
            {
                onComputerReach();
                PC.Carma += 2;
            }
            else if ((collision.CompareTag("Portal") || collision.CompareTag("SecondPortal")))
            {
                _health.ApplyDamage(999);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PC")&& LevelUP.isTaken[18] && WhoAmI!=EnemyTypes.Toocha&&ChangeMove==0)
        {
            _animator.SetTrigger("Evolution");
            _health.AddMaxHealth(1);
        }
        if (other.CompareTag("WayPoint"))
        {
            MoveToPoint = other.gameObject.GetComponent<WayPoint>()
                .nextPoint[UnityEngine. Random.Range(0, other.gameObject.GetComponent<WayPoint>().nextPoint.Length)];
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
            Destroy(GetComponent<AttackProjectile>());
            dead = true;
        }
        
        Level.TakeEXP(0.5f);
        
    }
}