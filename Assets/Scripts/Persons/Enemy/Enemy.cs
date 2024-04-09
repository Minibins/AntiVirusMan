using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using UnityEngine;
public enum EnemyTypes
    {
        [EnemyTypesAttributes(500,0,0,0)]
        Soplik,
        [EnemyTypesAttributes(0,5,0,0)]
        Stepa,
        [EnemyTypesAttributes(-1,0,5,0)]
        Booka,
        [EnemyTypesAttributes(1000,-5,0,0)]
        Toocha,
        [EnemyTypesAttributes(20,0,-5,0)]
        Yasha
    }
public class EnemyTypesAttributes:Attribute
{
    public readonly Vector3Int Position;
    public readonly float ForcerePulsive;
    public EnemyTypesAttributes(float forcerePulsive, int x,int y,int z)
    {
        Position = new Vector3Int(x,y,z);
        ForcerePulsive = forcerePulsive;
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
    {   
        EnemyTypesAttributes myAttributes = typeof(EnemyTypes).GetField(WhoAmI.ToString()).GetCustomAttribute<EnemyTypesAttributes>();
        if(myAttributes.ForcerePulsive<0) { return; }
        AboveDeath MyDeath= gameObject.AddComponent<AboveDeath>();
        MyDeath.ForcerePulsive = myAttributes.ForcerePulsive;
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
        if(_move != null)
        {
            if(ChangeMove == 0)
            {
                moveDirection = DustyStudios.MathAVM.MathA.OneOrNegativeOne(_PC.transform.position.x < transform.position.x);

                _move.MoveHorizontally(moveDirection);
            }
            else if(ChangeMove == 1)
            {
                _move.MoveOnWire(MoveToPoint);
            }
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
            ExitWire();
        }
    }

    private void ExitWire()
    {
        ChangeMove = 0;
        if(gameObject.GetComponent<blackenemy>() == null)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(isFallFromWire)
        {
            _animator.SetTrigger("Corpse");
            isFallFromWire = false;
        }
    }
    bool isFallFromWire;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(ChangeMove==1)
        {
            if(collision.CompareTag("Way"))
            {
                ExitWire();
                _animator.SetTrigger("Fall");
                isFallFromWire = true;
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
            if(!Save.console)
                PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
            Enemies.Remove(this);
            if(_move!=null)
                _move.SetSpeedMultiplierForever(0);
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