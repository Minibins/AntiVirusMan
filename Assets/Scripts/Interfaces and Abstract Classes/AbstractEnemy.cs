using DustyStudios;

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemy : MonoBehaviour
{
    [SerializeField] private string playerPrefsName, playerPrefsLittleName;
    [SerializeField] public bool isElite;
    [SerializeField] public LayerMask _maskWhoKills;
    public GameObject _PC;
    public Action onComputerReach;
    public static List<AbstractEnemy> Enemies = new List<AbstractEnemy>();
    protected MoveBase _move;
    protected Animator _animator;
    protected Health _health;
    protected virtual bool canEvolute => true;

    private bool isLittle = false;
    protected bool IsLittle
    { 
        get => isLittle;
        set
        {
            if(value ^ isLittle)
            {
                if(value)
                    BecameChild();
                else
                    Evolution();
            }
            isLittle = value; 
        }
    }
    protected virtual void Awake()
    {
        _move = GetComponent<MoveBase>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        Enemies.Add(this);
        if(LevelUP.Items[14].IsTaken)
            gameObject.AddComponent<DRAG>();

        if(LevelUP.Items[16].IsTaken)
            AddBookaComponent();

        if(LevelUP.Items[18].IsTaken)
            isLittle = true;

        ResetPC();
    }
    private void FixedUpdate()
    {
        if(_move != null) EnemyMove();
    }
    protected virtual void EnemyMove() 
    {}
    public void ResetPC()=>
        _PC = GameObject.FindGameObjectWithTag("PC");
    public virtual AboveDeath AddBookaComponent()
    {
        AboveDeath MyDeath = gameObject.AddComponent<AboveDeath>();
        MyDeath.IsPlatform = true;
        return MyDeath;
    }
    public virtual void BecameChild() => _health.AddMaxHealth(-1);
    public virtual void Evolution() => _health.AddMaxHealth(1);
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PC") && LevelUP.Items[18].IsTaken)
            IsLittle = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_health.CurrentHealth > 0 && (_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0 && !dead)
        {
            onComputerReach();
            PC.Carma += 2;
        }
    }
    protected virtual void OnEnable()
    {
        _health.OnDeath += OnDeath;
        onComputerReach += OnDeath;
    }

    protected virtual void OnDisable()
    {
        _health.OnDeath -= OnDeath;
        onComputerReach -= OnDeath;
    }
    protected float deathTime;
    protected bool dead = false;
    private void OnDeath()
    {
        if(dead)
            AfterDeathPunch();
        else
        {
            string ppname = IsLittle ? playerPrefsLittleName : playerPrefsName;
            if(!DustyConsoleInGame.UsedConsoleInSession)
                PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
            Enemies.Remove(this);
            if(_move != null)
                _move.SetSpeedMultiplierForever(0);
            const string DeathAnimationName = "Die";
            if(_animator.parameters.Any(a => a.name == DeathAnimationName))
                _animator.SetTrigger(DeathAnimationName);
            else
                Destroy(gameObject,Time.fixedDeltaTime * 3);
            Destroy(GetComponent<AttackProjectile>());
            dead = true;
            deathTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        }
        _health.SoundDead();
        Level.EXP += 0.4f;
    }
    protected virtual void AfterDeathPunch()
    {
    }
}