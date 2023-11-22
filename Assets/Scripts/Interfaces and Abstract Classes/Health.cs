using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health: MonoBehaviour, Draggable, IDamageble
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] private GameObject DeathSound;
    [SerializeField] private GameObject PunchSound;
    [SerializeField] private float needVelocityForInvisibility = 9999;
    [field: SerializeField] public float CurrentHealth;
    private bool IsInvisible;
    private Rigidbody2D rb;
    private Animator animator;
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
        if (!IsInvisible)
        {
            Instantiate(PunchSound);
            CurrentHealth -= damage;
            OnApplyDamage?.Invoke();
            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        CurrentHealth += maxHealth;
        _maxHealth += maxHealth;
    }

    public virtual void HealHealth(int health)
    {
        CurrentHealth += health;
        CurrentHealth = CurrentHealth > _maxHealth ? _maxHealth : CurrentHealth;
        
    }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public virtual void DestroyHimself() {

    }


    protected virtual void FixedUpdate()
    {
        if (rb.velocity.magnitude < needVelocityForInvisibility)
        {
            IsInvisible = false;
            animator.SetBool("IsInvisible", false);
        }
        else
        {
            IsInvisible = true;
            animator.SetBool("IsInvisible", true);
        }
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
        IsInvisible = false;
    }

    public void OnDamageGet(int Damage)
    {
        ApplyDamage(Damage);
    }
}