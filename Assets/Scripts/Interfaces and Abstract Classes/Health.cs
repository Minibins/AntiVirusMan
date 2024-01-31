using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, iDraggable, IDamageble
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] private GameObject DeathSound;
    [SerializeField] private GameObject PunchSound;
    [field: SerializeField] public float CurrentHealth;
    protected Animator animator;
    private Action _onDeath;

    public Action OnDeath
    {
        get => _onDeath;
        set
        {
            if (_onDeath != null)
            {
                _onDeath -= DestroyHimself;
            }

            _onDeath = value;
        }
    }

    public Action OnApplyDamage { get; set; }

    public virtual void ApplyDamage(float damage)
    {
        
            Instantiate(PunchSound);
            CurrentHealth -= damage;
            OnApplyDamage?.Invoke();
            if(CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
    }

    public void AddMaxHealth(int maxHealth)
    {
        CurrentHealth += maxHealth;
        _maxHealth += maxHealth;
    }

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
        Instantiate(DeathSound);
    }

    public void OnDrag()
    {
    }

    public void OnDragEnd()
    {
    }

    public void OnDamageGet(int Damage)
    {
        ApplyDamage(Damage);
    }
}