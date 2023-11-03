using System;

using Unity.VisualScripting;

using UnityEngine;

public class Health : MonoBehaviour,Draggable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthCell[] healthCells;
    [SerializeField] public GameManager gameManager;
    [SerializeField] private GameObject DeathSound;
    [SerializeField] private GameObject PunchSound;
    [SerializeField] private float needVelocityForInvisibility;
    private Animator animator;
    private Rigidbody2D rb;
    private bool IsInvisible;
    [field: SerializeField] public int CurrentHealth;
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
    public void ApplyDamage(int damage)
    {
        if(!IsInvisible)
        {
            Instantiate(PunchSound);
            CurrentHealth -= damage;
            OnApplyDamage?.Invoke();
            if(CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
        if(gameObject.name == "PC")
        {
            for(int i = 0; i < CurrentHealth; i++)
            {
                healthCells[i].Enable();
            }
            for(int i = _maxHealth - 1; i >= CurrentHealth; i--)
            {
                healthCells[i].Disable();
            }
        }
    }
    public void SetMaxHealth(int maxHealth)
    {
        CurrentHealth += maxHealth;
        _maxHealth += maxHealth;
    }
    public void HealHealth(int health)
    {
        CurrentHealth += health;
        CurrentHealth = CurrentHealth > _maxHealth ? _maxHealth : CurrentHealth;
        if (gameObject.name == "PC")
        {
            for (int i = 0; i < CurrentHealth; i++)
            {
                healthCells[i].Enable();
            }
        }
    }
    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
    private void DestroyHimself()
    {
        if (gameObject.name == "PC")
        {
            gameManager.LoseGame();
            Instantiate(DeathSound);
        }
        else
        {Destroy(gameObject);
            GetComponent<Enemy>()._PC.GetComponentInChildren<PC>().EnemyKilled();
        }
        
    }
    private void FixedUpdate()
    {
            if(rb.velocity.magnitude < needVelocityForInvisibility)
            {
                IsInvisible = false;
                animator.SetBool("IsInvisible",false);
            }
            else
            {
                IsInvisible = true;
                animator.SetBool("IsInvisible",true );
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
}