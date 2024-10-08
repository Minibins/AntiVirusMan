using System;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour, iDraggable, IDamageble, IHealable
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] private GameObject DeathSound, PunchSound;
    [SerializeField] IDamageble.DamageType[] parrybleDamageTypes;
    [field: SerializeField] public float CurrentHealth;
    protected Animator animator;
    private Action _onDeath;
    public Stat multiplerDamage = new Stat(1);
    
    private bool hasParryAnim;
    private const string parryAnimName = "Parry";

    public Action OnDeath
    {
        get => _onDeath;
        set
        {
            if (value != null)
            {
                _onDeath -= DestroyHimself;
            }

            _onDeath = value;
        }
    }

    public Action OnApplyDamage;

    public virtual void ApplyDamage(float damage)
    {
        CurrentHealth -= damage * multiplerDamage;
        OnApplyDamage?.Invoke();
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        if (PunchSound != null) Instantiate(PunchSound);
    }

    public void AddMaxHealth(int maxHealth)
    {
        CurrentHealth += maxHealth;
        _maxHealth += maxHealth;
    }

    public void Heal(int hp) => HealHealth(hp);

    public virtual void HealHealth(int health)
    {
        CurrentHealth += health;
        CurrentHealth = CurrentHealth > _maxHealth ? _maxHealth : CurrentHealth;
    }

    protected virtual void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null && animator.isActiveAndEnabled && gameObject.activeInHierarchy)
        {
            if (animator.parameters.Any(a => a.name == parryAnimName))
                hasParryAnim = true;
        }

        if (OnDeath == null)
        {
            OnDeath = DestroyHimself;
        }
    }

    private void OnDestroy()
    {
        OnDeath = null;
        OnApplyDamage = null;
    }

    public virtual void DestroyHimself()
    {
    }

    public void SoundDead()
    {
        if (DeathSound != null) Instantiate(DeathSound);
    }

    public void OnDrag()
    {
    }

    public void OnDragEnd()
    {
    }
    
    public void OnDamageGet(float Damage, IDamageble.DamageType type)
    {
        if (parrybleDamageTypes != null && !parrybleDamageTypes.Contains(type))
            ApplyDamage(Damage);
        else if (hasParryAnim) animator.SetTrigger(parryAnimName);
    }
}