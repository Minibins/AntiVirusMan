using System;
using System.Linq;
using System.Reflection;
using DustyStudios.MathAVM;
using UnityEngine;

public enum EnemyTypes
{
    [EnemyTypesAttributes(500, 0,  0,  0)] Soplik,
    [EnemyTypesAttributes(0,   5,  0,  0)] Stepa,
    [EnemyTypesAttributes(-1,  0,  5,  0)] Booka,
    [EnemyTypesAttributes(1000,-5, 0,  0)] Toocha,
    [EnemyTypesAttributes(100, 0,  -5, 0)] Yasha
}

public class EnemyTypesAttributes : Attribute
{
    public readonly Vector3Int Position;
    public readonly float ForcerePulsive;

    public EnemyTypesAttributes(float forcerePulsive, int x, int y, int z)
    {
        Position = new Vector3Int(x, y, z);
        ForcerePulsive = forcerePulsive;
    }
}

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Animator)),
 RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(SpriteRenderer))]
public class Enemy : AbstractEnemy
{
    [SerializeField] public EnemyTypes WhoAmI;
    public float moveDirection;
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        base.Awake();
    }
    public override AboveDeath AddBookaComponent()
    {
        EnemyTypesAttributes myAttributes =
            typeof(EnemyTypes).GetField(WhoAmI.ToString()).GetCustomAttribute<EnemyTypesAttributes>();
        if (myAttributes.ForcerePulsive < 0)
            return null;

        AboveDeath MyDeath = base.AddBookaComponent();
        MyDeath.ForcerePulsive = myAttributes.ForcerePulsive;
        return MyDeath;
    }
    #region Эволюция
    protected override bool canEvolute => WhoAmI != EnemyTypes.Toocha;
    public override void BecameChild()
    {
        base.BecameChild();
        _animator.Play("Wire");
    }
    public override void Evolution()
    {
        base.Evolution();
        _animator.SetTrigger("Evolution");
    }
    #endregion
    protected override void EnemyMove()=>
        _move.MoveHorizontally(MathA.OneOrNegativeOne(_PC.transform.position.x < transform.position.x));

    [SerializeField] private bool CanCombo;
    protected override void AfterDeathPunch()
    {
        if(CanCombo)
        base.AfterDeathPunch();
        Destroy(gameObject,deathTime);
        const string ComboAnimationName = "Up";
        if(_animator.parameters.Any(a => a.name == ComboAnimationName))
            _animator.SetTrigger(ComboAnimationName);
    }
}