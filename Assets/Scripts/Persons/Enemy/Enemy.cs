using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
    public static List<Enemy> Enemies = new();
    [SerializeField] public bool isElite,isLittle;
    [SerializeField] public EnemyTypes WhoAmI;
    [SerializeField] public LayerMask _maskWhoKills;
    public float moveDirection;
    protected Health _health;
    protected MoveBase _move;
    [SerializeField] string playerPrefsName,playerPrefsLittleName;
    public MoveBase Move
    {
        get => _move;
    }
    SpriteRenderer _spriteRenderer;
    protected Animator _animator;
    private bool dead = false;
    public int ChangeMove;
    public GameObject MoveToPoint;
    public GameObject _PC;
    public Action onComputerReach;
    virtual private protected void Awake()
    {
        _health = GetComponent<Health>();
        _move = GetComponent<MoveBase>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        if(LevelUP.Items[14].IsTaken)
        {
            gameObject.AddComponent<DRAG>();
        }
        if(LevelUP.Items[16].IsTaken)
        {
            AddBookaComponent();
        }
        if(LevelUP.Items[18].IsTaken)
        {
            isLittle=true;
            _health.AddMaxHealth(-1);
            _animator.Play("Wire");
        }
        ResetPC();
        Enemies.Add(this);
    }

    public void ResetPC()
    {
        _PC = GameObject.FindGameObjectWithTag("PC");
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
            moveDirection = DustyStudios.MathAVM.MathA.OneOrNegativeOne(_PC.transform.position.x < transform.position.x);
            if(_move!=null)
            {
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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PC")&& LevelUP.Items[18].IsTaken && WhoAmI!=EnemyTypes.Toocha&&ChangeMove==0)
        {
            isLittle = false;
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
    float deathTime;
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
                Destroy(gameObject, deathTime);
                const string ComboAnimationName = "Up";
                if(_animator.parameters.Any(a => a.name== ComboAnimationName))
                    _animator.SetTrigger(ComboAnimationName);
            }
        }
        else
        {
            string ppname = isLittle ? playerPrefsLittleName : playerPrefsName;
            PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
            Enemies.Remove(this);
            _move.SetSpeedMultiplierForOllTime(0);
            const string DeathAnimationName = "Die";
            if(_animator.parameters.Any(a => a.name == DeathAnimationName))
                _animator.SetTrigger(DeathAnimationName);
            else
                Destroy(gameObject,Time.fixedDeltaTime*3);
            Destroy(GetComponent<AttackProjectile>());
            dead = true;
            deathTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        }
        _health.SoundDead();
        Level.EXP += 0.4f;
        
    }
}